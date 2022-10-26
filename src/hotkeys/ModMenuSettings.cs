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

namespace Apocc.Pw.Hotkeys
{
    public sealed class ModLockEntry
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string Value { get; set; }

        public ModLockEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    internal sealed class ModMenuSettings
    {
        private static readonly string _keyTitle = Utilities.GetKey("title");
        private static readonly string _keyBtnVerboseDesc = Utilities.GetKey("btn.verbose.desc");
        private static readonly string _keyBtnVerboseText = Utilities.GetKey("btn.verbose.text");

        private CultureInfo _ci;
        private bool _initialized = false;
        private SettingsBuilder _settings;
        private static readonly ModLockEntry[] _entriesEnGB = new ModLockEntry[]
        {
            new ModLockEntry(_keyBtnVerboseDesc,"Only for debugging purposes, please deactivate during normal gameplay."),
            new ModLockEntry(_keyBtnVerboseText,"Enable verbose logging"),

            new ModLockEntry(AiStealth.KeyHeader, "Ai and Stealth"),
            new ModLockEntry(AiStealth.KeyBtnEnableDesc, "Enable Ai and Stealth hotkeys"),
            new ModLockEntry(AiStealth.KeyKbAiDesc, "AI hotkey"),
            new ModLockEntry(AiStealth.KeyKbStealthDesc, "Stealth hotkey"),

            new ModLockEntry(WeaponSets.KeyHeader, "Weapon Sets"),
            new ModLockEntry(WeaponSets.KeyBtnEnableDesc, "Enable Weapon Set hotkeys"),
            new ModLockEntry(WeaponSets.KeyKb00Desc, "Hotkey for weapon set 1"),
            new ModLockEntry(WeaponSets.KeyKb01Desc, "Hotkey for weapon set 2"),
            new ModLockEntry(WeaponSets.KeyKb02Desc, "Hotkey for weapon set 3"),
            new ModLockEntry(WeaponSets.KeyKb03Desc, "Hotkey for weapon set 4"),
            new ModLockEntry(WeaponSets.KeyKbCycleDesc, "Hotkey for weapon set cycle"),
            new ModLockEntry(WeaponSets.KeyToggleForAllDesc, "For all selected chararacters"),
            new ModLockEntry(WeaponSets.KeyToggleInventoryDesc, "Enable weapon set hotkeys when in inventory"),
            new ModLockEntry(WeaponSets.KeyToggleInventoryForAllDesc, "For all selected chararacters when in inventory"),

            new ModLockEntry(ActionBar.KeyHeader, "Action Bar"),
            new ModLockEntry(ActionBar.KeyBtnEnableDesc, "Enable Action Bar hotkeys"),
            new ModLockEntry(ActionBar.KeyKbAbilityDesc, "Toggle ability panel"),
            new ModLockEntry(ActionBar.KeyKbSpellsDesc, "Toggle spells panel"),
            new ModLockEntry(ActionBar.KeyKbQuickDesc, "Toggle quick slot panel"),

            new ModLockEntry(QuickSlot.KeyHeader, "Quick Slot"),
            new ModLockEntry(QuickSlot.KeyBtnEnableDesc, "Enable Quick Slot hotkeys"),
            new ModLockEntry(QuickSlot.KeyKb00Desc, "Hotkey for slot 1"),
            new ModLockEntry(QuickSlot.KeyKb01Desc, "Hotkey for slot 2"),
            new ModLockEntry(QuickSlot.KeyKb02Desc, "Hotkey for slot 3"),
            new ModLockEntry(QuickSlot.KeyKb03Desc, "Hotkey for slot 4"),
            new ModLockEntry(QuickSlot.KeyKb04Desc, "Hotkey for slot 5"),
            new ModLockEntry(QuickSlot.KeyToggleForAllDesc, "For all selected chararacters"),
            new ModLockEntry(QuickSlot.KeyToggleQuickSlotPlacementDesc, "Use quick slot placement"),

            new ModLockEntry(CharacterSelect.KeyHeader, "Character Select"),
            new ModLockEntry(CharacterSelect.KeyBtnEnableDesc, "Enable Character Select hotkeys"),
            new ModLockEntry(CharacterSelect.KeyKbNextDesc, "Hotkey to select next character"),
            new ModLockEntry(CharacterSelect.KeyKbPrevDesc, "Hotkey to select previous character"),

            new ModLockEntry(Formation.KeyHeader, "Formations"),
            new ModLockEntry(Formation.KeyBtnEnableDesc, "Enable Formations hotkeys"),
            new ModLockEntry(Formation.KeyKb00Desc, "Hotkey for formation: auto"),
            new ModLockEntry(Formation.KeyKb01Desc, "Hotkey for formation: triangle"),
            new ModLockEntry(Formation.KeyKb02Desc, "Hotkey for formation: star"),
            new ModLockEntry(Formation.KeyKb03Desc, "Hotkey for formation: waves"),
            new ModLockEntry(Formation.KeyKb04Desc, "Hotkey for formation: circle"),
            new ModLockEntry(Formation.KeyKb05Desc, "Hotkey for formation: hammer"),
            new ModLockEntry(Formation.KeyKbCycleDesc, "Hotkey for formation cycle"),
        };

        internal void CheckLocale()
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
                using (var fs = new FileStream("", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var entries = serializer.Deserialize(fs) as ModLockEntry[];

                    UpdateLocalisationPackWith(entries);
                }
            }
            catch (System.Exception e)
            {
                Log.Error("Could not update localisation pack, falling back to default!", Globals.LogPrefix);
                Globals.LogException(e);

                UpdateLocalisationPackWith(_entriesEnGB);
            }
        }

        private void UpdateLocalisationPackWith(ModLockEntry[] entries)
        {
            Log.Log("Updating locale pack", Globals.LogPrefix);

            var currentPack = LocalizationManager.CurrentPack;
            foreach (var mle in entries)
            {
                Log.Log("Putting: " + mle.Key, Globals.LogPrefix);
                currentPack.PutString(mle.Key.ToLowerInvariant(), mle.Value);
            }

            currentPack.PutString(_keyTitle, Globals.ModTitle);
        }

        internal void Init()
        {
            CheckLocale();

            if (_initialized)
            {
                Log.Log("ModMenu settings already initialized", Globals.LogPrefix);
                return;
            }

            _settings = SettingsBuilder.New(Globals.ModMenuSettingsKey, Utilities.GetString(_keyTitle));

            Log.Log("Initializing ModMenu settings", Globals.LogPrefix);
            _settings.AddButton(Button.New(Utilities.GetString(_keyBtnVerboseDesc), Utilities.GetString(_keyBtnVerboseText), OnVerbose));

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
