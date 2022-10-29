// Copyright (c) apocc.
// Licensed under MIT License.

using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Kingmaker.Localization;
using Kingmaker.Localization.Shared;
using ModMenu.Settings;
using Apocc.Pw.Hotkeys.Data;

using Log = UnityModManagerNet.UnityModManager.Logger;
using HarmonyLib;

namespace Apocc.Pw.Hotkeys
{
    public sealed class ModLockEntry
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string Value { get; set; }

        public ModLockEntry() { }

        public ModLockEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    [HarmonyPatch(typeof(LocalizationManager))]
    public static class LocalizationManager_OnLocaleChanged_Prefix_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnLocaleChanged")]
        public static bool Prefix()
        {
            Log.Log($"LocalizationManager_OnLocaleChanged_Prefix_Patch: New locale -> {LocalizationManager.CurrentLocale}", Globals.LogPrefix);

            ModMenuSettings.CheckLocale();

            return true;
        }
    }

    internal sealed class ModMenuSettings
    {
        private static readonly string _keyTitle = Utilities.GetKey("title");

        private static CultureInfo _ci;
        private bool _initialized = false;
        private SettingsBuilder _settings;
        private static readonly ModLockEntry[] _entriesEnGB = new ModLockEntry[]
        {
            new ModLockEntry(AiStealth.KeyHeader, "Ai and Stealth"),
            new ModLockEntry(AiStealth.KeyBtnEnableDesc, "Enable Ai and Stealth hotkeys"),
            new ModLockEntry(AiStealth.KeyKbAiDesc, "AI hotkey"),
            new ModLockEntry(AiStealth.KeyKbStealthDesc, "Stealth hotkey"),

            new ModLockEntry(WeaponSets.KeyHeader, "Weapon Sets"),
            new ModLockEntry(WeaponSets.KeyBtnEnableDesc, "Enable Weapon Set hotkeys"),
            new ModLockEntry(WeaponSets.KeyKb00Desc, "Weapon set 1"),
            new ModLockEntry(WeaponSets.KeyKb01Desc, "Weapon set 2"),
            new ModLockEntry(WeaponSets.KeyKb02Desc, "Weapon set 3"),
            new ModLockEntry(WeaponSets.KeyKb03Desc, "Weapon set 4"),
            new ModLockEntry(WeaponSets.KeyKbCycleDesc, "Weapon set cycle"),
            new ModLockEntry(WeaponSets.KeyToggleForAllDesc, "For all selected chararacters"),
            new ModLockEntry(WeaponSets.KeyToggleInventoryDesc, "Enable weapon set hotkeys when in inventory"),
            new ModLockEntry(WeaponSets.KeyToggleInventoryForAllDesc, "For all selected chararacters when in inventory"),

            new ModLockEntry(ActionBar.KeyHeader, "Action Bar"),
            new ModLockEntry(ActionBar.KeyBtnEnableDesc, "Enable Action Bar hotkeys"),
            new ModLockEntry(ActionBar.KeyKbAbilityDesc, "Abilities"),
            new ModLockEntry(ActionBar.KeyKbSpellsDesc, "Spells"),
            new ModLockEntry(ActionBar.KeyKbQuickDesc, "Quick Slots"),

            new ModLockEntry(QuickSlot.KeyHeader, "Quick Slot"),
            new ModLockEntry(QuickSlot.KeyBtnEnableDesc, "Enable Quick Slot hotkeys"),
            new ModLockEntry(QuickSlot.KeyKb00Desc, "Slot 1"),
            new ModLockEntry(QuickSlot.KeyKb01Desc, "Slot 2"),
            new ModLockEntry(QuickSlot.KeyKb02Desc, "Slot 3"),
            new ModLockEntry(QuickSlot.KeyKb03Desc, "Slot 4"),
            new ModLockEntry(QuickSlot.KeyKb04Desc, "Slot 5"),
            new ModLockEntry(QuickSlot.KeyToggleForAllDesc, "For all selected chararacters"),
            new ModLockEntry(QuickSlot.KeyToggleQuickSlotPlacementDesc, "Use quick slot placement"),

            new ModLockEntry(CharacterSelect.KeyHeader, "Character Select"),
            new ModLockEntry(CharacterSelect.KeyBtnEnableDesc, "Enable Character Select hotkeys"),
            new ModLockEntry(CharacterSelect.KeyKbNextDesc, "Next character"),
            new ModLockEntry(CharacterSelect.KeyKbPrevDesc, "Previous character"),

            new ModLockEntry(Formation.KeyHeader, "Formations"),
            new ModLockEntry(Formation.KeyBtnEnableDesc, "Enable Formations hotkeys"),
            new ModLockEntry(Formation.KeyKb00Desc, "Formation: auto"),
            new ModLockEntry(Formation.KeyKb01Desc, "Formation: triangle"),
            new ModLockEntry(Formation.KeyKb02Desc, "Formation: star"),
            new ModLockEntry(Formation.KeyKb03Desc, "Formation: waves"),
            new ModLockEntry(Formation.KeyKb04Desc, "Formation: circle"),
            new ModLockEntry(Formation.KeyKb05Desc, "Formation: hammer"),
            new ModLockEntry(Formation.KeyKbCycleDesc, "Formation cycle"),
        };

        internal static void CheckLocale()
        {
            Log.Log($"Checking if locale has changed: {_ci} -> {LocalizationManager.CurrentLocale}", Globals.LogPrefix);

            var localeHasChanged = false;
            var currentCulture = LocalizationManager.CurrentLocale.GetCulture();
            if (_ci != currentCulture)
            {
                _ci = currentCulture;
                localeHasChanged = true;
            }

            if (!localeHasChanged) return;

            try
            {
                Log.Log("Trying to update locale data", Globals.LogPrefix);

                if (LocalizationManager.CurrentLocale == Locale.enGB)
                {
                    UpdateLocalisationPackWith(_entriesEnGB);
                    return;
                }

                var serializer = new XmlSerializer(typeof(ModLockEntry[]));
                var filename = $"{LocalizationManager.CurrentLocale}.xml";
                using (var fs = new FileStream(Path.Combine("Mods", Globals.ModId, Globals.LocalisationFolder, filename), FileMode.Open, FileAccess.Read))
                {
                    var entries = serializer.Deserialize(fs) as ModLockEntry[];

                    UpdateLocalisationPackWith(entries, true);
                }
            }
            catch (System.Exception e)
            {
                Log.Error("Could not update localisation pack, falling back to default!", Globals.LogPrefix);
                Globals.LogException(e);

                UpdateLocalisationPackWith(_entriesEnGB);
            }
        }

        private static void UpdateLocalisationPackWith(ModLockEntry[] entries, bool isPrefixOnly = false)
        {
            Log.Log("Updating locale pack", Globals.LogPrefix);

            var currentPack = LocalizationManager.CurrentPack;
            foreach (var mle in entries)
            {
                var key = isPrefixOnly ? Utilities.GetKey(mle.Key) : mle.Key;
                currentPack.PutString(key.ToLowerInvariant(), mle.Value);
            }

            currentPack.PutString(_keyTitle, Globals.ModTitle);
        }

        internal void Init()
        {
            if (_initialized)
            {
                Log.Log("ModMenu settings already initialized", Globals.LogPrefix);
                return;
            }

            _settings = SettingsBuilder.New(Globals.ModMenuSettingsKey, Utilities.GetString(_keyTitle));

            Log.Log("Initializing ModMenu settings", Globals.LogPrefix);

            AiStealth.AddModMenuSettings(_settings);
            WeaponSets.AddModMenuSettings(_settings);
            ActionBar.AddModMenuSettings(_settings);
            QuickSlot.AddModMenuSettings(_settings);
            CharacterSelect.AddModMenuSettings(_settings);
            Formation.AddModMenuSettings(_settings);

            ModMenu.ModMenu.AddSettings(_settings);

            _initialized = true;
            Log.Log("ModMenu settings initialized", Globals.LogPrefix);
        }

        internal void OnVerbose()
        {
            Log.Log("Verbose logging toggle");
        }
    }
}
