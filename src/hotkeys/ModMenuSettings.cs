// Copyright (c) apocc.
// Licensed under MIT License.

using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Kingmaker.Localization;
using Kingmaker.Localization.Shared;
using ModMenu.Settings;
using Apocc.Pw.Hotkeys.Data;
using HarmonyLib;

using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
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

        internal static void CheckLocale()
        {
            if (Main.Settings.EnableVerboseLogging)
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
                if (Main.Settings.EnableVerboseLogging)
                    Log.Log("Trying to update locale data", Globals.LogPrefix);

                if (LocalizationManager.CurrentLocale == Locale.enGB)
                {
                    UpdateLocalisationPackWith(SettingsLocaleData.EntriesEnGB);
                    return;
                }

                var serializer = new XmlSerializer(typeof(ModLockEntry[]));
                var filename = $"{LocalizationManager.CurrentLocale}.xml";
                var filepath = Path.Combine("Mods", Globals.ModId, Globals.LocalisationFolder, filename);

                if (!File.Exists(filepath))
                {
                    if (Main.Settings.EnableVerboseLogging)
                        Log.Error("No localisation file found, falling back to default!", Globals.LogPrefix);
                    UpdateLocalisationPackWith(SettingsLocaleData.EntriesEnGB);
                    return;
                }

                using (var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    var entries = serializer.Deserialize(fs) as ModLockEntry[];

                    UpdateLocalisationPackWith(entries, true);
                }
            }
            catch (System.Exception e)
            {
                Log.Error("Could not update localisation pack, falling back to default!", Globals.LogPrefix);
                Globals.LogException(e);

                UpdateLocalisationPackWith(SettingsLocaleData.EntriesEnGB);
            }
        }

        private static void UpdateLocalisationPackWith(ModLockEntry[] entries, bool isPrefixOnly = false)
        {
            if (Main.Settings.EnableVerboseLogging)
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
    }
}
