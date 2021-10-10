// Copyright (c) apocc.
// Licensed under MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Apocc.Pw.Hotkeys.Data;
using Kingmaker.Localization;
using Kingmaker.Localization.Shared;
using UnityEngine;
using UnityModManagerNet;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public sealed class Settings : UnityModManager.ModSettings
    {
        private List<string> _availableCultures = new List<string>();
        private string _culture;
        private SettingsLocaleData _currentCultureData;
        private SettingsLocaleData _defaultCultureData;
        public KeyCode ActionBarToggleAbility = KeyCode.None;
        public KeyCode ActionBarToggleQuick = KeyCode.None;
        public KeyCode ActionBarToggleSpells = KeyCode.None;
        public KeyCode CsNext = KeyCode.None;
        public KeyCode CsPrev = KeyCode.None;
        public bool EnableActionBar = false;
        public bool EnableCharSel = false;
        public bool EnableForm = false;
        public bool EnableTAiS = false;
        public bool EnableTws = false;
        public bool EnableUsit = false;
        public bool EnableVerboseLogging = false;
        public KeyCode FormKey00 = KeyCode.None;
        public KeyCode FormKey01 = KeyCode.None;
        public KeyCode FormKey02 = KeyCode.None;
        public KeyCode FormKey03 = KeyCode.None;
        public KeyCode FormKey04 = KeyCode.None;
        public KeyCode FormKey05 = KeyCode.None;
        public KeyCode FormKeyCircle = KeyCode.None;
        public KeyCode TaisKeyAi = KeyCode.None;
        public KeyCode TaisKeyStealth = KeyCode.None;
        public bool TwsEnableAllSelectedCharacters = false;
        public bool TwsEnableInInventory = false;
        public bool TwsForceChangeForAllWhenInInventory = false;
        public KeyCode TwsKey00 = KeyCode.None;
        public KeyCode TwsKey01 = KeyCode.None;
        public KeyCode TwsKey02 = KeyCode.None;
        public KeyCode TwsKey03 = KeyCode.None;
        public KeyCode TwsToggle = KeyCode.None;
        public bool UsitEnableAllSelectedCharacters = false;
        public KeyCode UsitSlot00 = KeyCode.None;
        public KeyCode UsitSlot01 = KeyCode.None;
        public KeyCode UsitSlot02 = KeyCode.None;
        public KeyCode UsitSlot03 = KeyCode.None;
        public KeyCode UsitSlot04 = KeyCode.None;
        public bool UsitUseActionBarPlacement = false;

        private static List<string> LoadAvailableCultures(UnityModManager.ModEntry modEntry)
        {
            var cultures = new List<string> { Globals.DefaultCulture };

            try
            {
                var path = Path.Combine(modEntry.Path, Globals.LocalisationFolder);
                if (!Directory.Exists(path))
                    Log.Log("No localisation folder found", Globals.LogPrefix);
                else
                    cultures.AddRange(Directory.EnumerateFiles(path).Select(p => Path.GetFileNameWithoutExtension(p).ToLowerInvariant()));
            }
            catch (Exception e)
            {
                Log.Error("Could not load available cultures!", Globals.LogPrefix);
                Globals.LogException(e);
            }

            Log.Log("Available cultures: " + string.Join(",", cultures), Globals.LogPrefix);
            return cultures;
        }

        public static Settings LoadSettings(UnityModManager.ModEntry modEntry)
        {
            Log.Log("Trying to load settings", Globals.LogPrefix);

            var settings = Load<Settings>(modEntry);

            Log.Log("Loading additional settings data", Globals.LogPrefix); ;
            settings._availableCultures = LoadAvailableCultures(modEntry);
            settings._defaultCultureData = SettingsLocaleData.Default;

            return settings;
        }

        public SettingsLocaleData GetCultureData() => _currentCultureData ?? _defaultCultureData;

        public T GetFieldValue<T>(string propertyName) => (T)GetType().GetField(propertyName)?.GetValue(this);

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            if (EnableVerboseLogging)
                Log.Log("Settings.Save: Trying to save settings", Globals.LogPrefix);

            Save(this, modEntry);

            if (EnableVerboseLogging)
                Log.Log("Settings.Save: Finished saving settings", Globals.LogPrefix);
        }

        public void SetFieldValue(string propertyName, object value) => GetType().GetField(propertyName)?.SetValue(this, value);

        public void UpdateCulture(UnityModManager.ModEntry modEntry)
        {
            try
            {
                if (EnableVerboseLogging)
                    Log.Log("Processing game culture information", Globals.LogPrefix);

                if (_currentCultureData != null) return;

                var ingameCulture = LocaleExtensions.GetCulture(LocalizationManager.CurrentLocale).Name.ToLowerInvariant();

                if (EnableVerboseLogging)
                    Log.Log("Ingame culture: " + ingameCulture, Globals.LogPrefix);

                _culture = _availableCultures.FirstOrDefault(ac => ac == ingameCulture) ?? Globals.DefaultCulture;
                if (_culture == Globals.DefaultCulture)
                {
                    _currentCultureData = SettingsLocaleData.Default;
                    return;
                }

                var path = Path.Combine(modEntry.Path, Globals.LocalisationFolder);
                var filepath = $"{path}\\{_culture}.xml";
                using (var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    _currentCultureData = new XmlSerializer(typeof(SettingsLocaleData)).Deserialize(fs) as SettingsLocaleData;
            }
            catch (Exception e)
            {
                Log.Error("Could not update culture, falling back to default!", Globals.LogPrefix);
                Globals.LogException(e);

                _currentCultureData = SettingsLocaleData.Default;
                _culture = Globals.DefaultCulture;
            }
        }
    }
}
