// Copyright (c) apocc.
// Licensed under MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using Apocc.Pw.Hotkeys.Data;
using Kingmaker.Localization;
using Kingmaker.Localization.Shared;
using UnityEngine;
using UnityModManagerNet;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public sealed class Settings
    {
        #region fields

        private static readonly string KeyCodeNone = KeyCode.None.ToString();
        private string _culture;
        private SettingsLocaleData _currentCultureData;
        private SettingsLocaleData _defaultCultureData;
        private string _keyActionBarToggleAbility = KeyCodeNone;
        private string _keyActionBarToggleQuick = KeyCodeNone;
        private string _keyActionBarToggleSpells = KeyCodeNone;
        private string _keyAi = KeyCodeNone;
        private string _keyCsNext = KeyCodeNone;
        private string _keyCsPrev = KeyCodeNone;
        private string _keyForm00 = KeyCodeNone;
        private string _keyForm01 = KeyCodeNone;
        private string _keyForm02 = KeyCodeNone;
        private string _keyForm03 = KeyCodeNone;
        private string _keyForm04 = KeyCodeNone;
        private string _keyForm05 = KeyCodeNone;
        private string _keyFormCircle = KeyCodeNone;
        private string _keySet00 = KeyCodeNone;
        private string _keySet01 = KeyCodeNone;
        private string _keySet02 = KeyCodeNone;
        private string _keySet03 = KeyCodeNone;
        private string _keySetToggle = KeyCodeNone;
        private string _keyStealth = KeyCodeNone;
        private string _keyUsitSlot00 = KeyCodeNone;
        private string _keyUsitSlot01 = KeyCodeNone;
        private string _keyUsitSlot02 = KeyCodeNone;
        private string _keyUsitSlot03 = KeyCodeNone;
        private string _keyUsitSlot04 = KeyCodeNone;
        internal const string Filename = "settings.xml";

        #endregion fields

        #region properties

        #region attributes

        [XmlAttribute("enableActionBar")]
        public bool EnableActionBar { get; set; } = true;
        [XmlAttribute("enableCharSel")]
        public bool EnableCharSel { get; set; } = true;
        [XmlAttribute("enableForm")]
        public bool EnableForm { get; set; } = true;
        [XmlAttribute("enableTAiS")]
        public bool EnableTAiS { get; set; } = true;
        [XmlAttribute("enableTws")]
        public bool EnableTws { get; set; } = true;
        [XmlAttribute("enableUsit")]
        public bool EnableUsit { get; set; } = true;
        [XmlAttribute("verbose")]
        public bool EnableVerboseLogging { get; set; }

        #endregion attributes

        #region serializable

        public string ActionBarToggleAbility { get => _keyActionBarToggleAbility; set => SetProperty(ref _keyActionBarToggleAbility, value); }
        public string ActionBarToggleQuick { get => _keyActionBarToggleQuick; set => SetProperty(ref _keyActionBarToggleQuick, value); }
        public string ActionBarToggleSpells { get => _keyActionBarToggleSpells; set => SetProperty(ref _keyActionBarToggleSpells, value); }
        public string CsNext { get => _keyCsNext; set => SetProperty(ref _keyCsNext, value); }
        public string CsPrev { get => _keyCsPrev; set => SetProperty(ref _keyCsPrev, value); }
        public bool EnableAllSelectedCharacters { get; set; }
        public string FormKey00 { get => _keyForm00; set => SetProperty(ref _keyForm00, value); }
        public string FormKey01 { get => _keyForm01; set => SetProperty(ref _keyForm01, value); }
        public string FormKey02 { get => _keyForm02; set => SetProperty(ref _keyForm02, value); }
        public string FormKey03 { get => _keyForm03; set => SetProperty(ref _keyForm03, value); }
        public string FormKey04 { get => _keyForm04; set => SetProperty(ref _keyForm04, value); }
        public string FormKey05 { get => _keyForm05; set => SetProperty(ref _keyForm05, value); }
        public string FormKeyCircle { get => _keyFormCircle; set => SetProperty(ref _keyFormCircle, value); }
        public string TaisKeyAi { get => _keyAi; set => SetProperty(ref _keyAi, value); }
        public string TaisKeyStealth { get => _keyStealth; set => SetProperty(ref _keyStealth, value); }
        public bool TwsEnableInInventory { get; set; }
        public bool TwsForceChangeForAllWhenInInventory { get; set; }
        public string TwsKey00 { get => _keySet00; set => SetProperty(ref _keySet00, value); }
        public string TwsKey01 { get => _keySet01; set => SetProperty(ref _keySet01, value); }
        public string TwsKey02 { get => _keySet02; set => SetProperty(ref _keySet02, value); }
        public string TwsKey03 { get => _keySet03; set => SetProperty(ref _keySet03, value); }
        public string TwsToggle { get => _keySetToggle; set => SetProperty(ref _keySetToggle, value); }
        public bool UsitEnableAllSelectedCharacters { get; set; }
        public string UsitSlot00 { get => _keyUsitSlot00; set => SetProperty(ref _keyUsitSlot00, value); }
        public string UsitSlot01 { get => _keyUsitSlot01; set => SetProperty(ref _keyUsitSlot01, value); }
        public string UsitSlot02 { get => _keyUsitSlot02; set => SetProperty(ref _keyUsitSlot02, value); }
        public string UsitSlot03 { get => _keyUsitSlot03; set => SetProperty(ref _keyUsitSlot03, value); }
        public string UsitSlot04 { get => _keyUsitSlot04; set => SetProperty(ref _keyUsitSlot04, value); }
        public bool UsitUseActionBarPlacement { get; set; }

        #endregion serializable

        #region internal

        [XmlIgnore]
        public KeyCode ActionBarKeyCodeToggleAbility { get; set; }
        [XmlIgnore]
        public KeyCode ActionBarKeyCodeToggleQuick { get; set; }
        [XmlIgnore]
        public KeyCode ActionBarKeyCodeToggleSpells { get; set; }
        [XmlIgnore]
        public List<string> AvailableCultures { get; set; }
        [XmlIgnore]
        public KeyCode CsKeyCodeNext { get; set; }
        [XmlIgnore]
        public KeyCode CsKeyCodePrev { get; set; }
        [XmlIgnore]
        public KeyCode FormKeyCode00 { get; set; }
        [XmlIgnore]
        public KeyCode FormKeyCode01 { get; set; }
        [XmlIgnore]
        public KeyCode FormKeyCode02 { get; set; }
        [XmlIgnore]
        public KeyCode FormKeyCode03 { get; set; }
        [XmlIgnore]
        public KeyCode FormKeyCode04 { get; set; }
        [XmlIgnore]
        public KeyCode FormKeyCode05 { get; set; }
        [XmlIgnore]
        public KeyCode FormKeyCodeCircle { get; set; }
        [XmlIgnore]
        public KeyCode TaisKeyCodeAi { get; set; }
        [XmlIgnore]
        public KeyCode TaisKeyCodeStealth { get; set; }
        [XmlIgnore]
        public KeyCode TwsKeyCode00 { get; set; }
        [XmlIgnore]
        public KeyCode TwsKeyCode01 { get; set; }
        [XmlIgnore]
        public KeyCode TwsKeyCode02 { get; set; }
        [XmlIgnore]
        public KeyCode TwsKeyCode03 { get; set; }
        [XmlIgnore]
        public KeyCode TwsKeyCodeToggle { get; set; }
        [XmlIgnore]
        public KeyCode UsitKeyCodeSlot00 { get; set; }
        [XmlIgnore]
        public KeyCode UsitKeyCodeSlot01 { get; set; }
        [XmlIgnore]
        public KeyCode UsitKeyCodeSlot02 { get; set; }
        [XmlIgnore]
        public KeyCode UsitKeyCodeSlot03 { get; set; }
        [XmlIgnore]
        public KeyCode UsitKeyCodeSlot04 { get; set; }

        #endregion internal

        #endregion properties

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

        private void SetProperty(ref string prop, string value, [CallerMemberName] string propertyName = null)
        {
            if (ReferenceEquals(prop, value)) return;
            prop = value;

            if (propertyName == null)
            {
                if (EnableVerboseLogging) Log.Error("Settings.SetProperty => propertyName is null!", Globals.LogPrefix);
                return;
            }

            if (!Enum.TryParse(prop, out KeyCode kc))
            {
                if (EnableVerboseLogging)
                    Log.Error($"Settings.SetProperty => Could not parse key code '{prop}' for property {propertyName}!", Globals.LogPrefix);

                return;
            }

            if (EnableVerboseLogging) Log.Log($"Successfully parsed key code '{prop}' for property '{propertyName}'", Globals.LogPrefix);

            switch (propertyName)
            {
                case "ActionBarToggleAbility": ActionBarKeyCodeToggleAbility = kc; break;
                case "ActionBarToggleSpells": ActionBarKeyCodeToggleSpells = kc; break;
                case "ActionBarToggleQuick": ActionBarKeyCodeToggleQuick = kc; break;
                case "FormKey00": FormKeyCode00 = kc; break;
                case "FormKey01": FormKeyCode01 = kc; break;
                case "FormKey02": FormKeyCode02 = kc; break;
                case "FormKey03": FormKeyCode03 = kc; break;
                case "FormKey04": FormKeyCode04 = kc; break;
                case "FormKey05": FormKeyCode05 = kc; break;
                case "FormKeyCircle": FormKeyCodeCircle = kc; break;
                case "TwsKey00": TwsKeyCode00 = kc; break;
                case "TwsKey01": TwsKeyCode01 = kc; break;
                case "TwsKey02": TwsKeyCode02 = kc; break;
                case "TwsKey03": TwsKeyCode03 = kc; break;
                case "TwsToggle": TwsKeyCodeToggle = kc; break;
                case "TaisKeyAi": TaisKeyCodeAi = kc; break;
                case "TaisKeyStealth": TaisKeyCodeStealth = kc; break;
                case "UsitSlot00": UsitKeyCodeSlot00 = kc; break;
                case "UsitSlot01": UsitKeyCodeSlot01 = kc; break;
                case "UsitSlot02": UsitKeyCodeSlot02 = kc; break;
                case "UsitSlot03": UsitKeyCodeSlot03 = kc; break;
                case "UsitSlot04": UsitKeyCodeSlot04 = kc; break;
                case "CsNext": CsKeyCodeNext = kc; break;
                case "CsPrev": CsKeyCodePrev = kc; break;
            }
        }

        public static Settings Load(UnityModManager.ModEntry modEntry)
        {
            Settings settings = null;

            try
            {
                Log.Log("Trying to load settings", Globals.LogPrefix);

                var path = Path.Combine(modEntry.Path, Filename);
                if (!File.Exists(path))
                {
                    Log.Log("No settings file found, loading default settings.", Globals.LogPrefix);
                    settings = new Settings();
                }
                else
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                        settings = new XmlSerializer(typeof(Settings)).Deserialize(fs) as Settings;
                }
            }
            catch (Exception e)
            {
                Log.Error("Could not parse settings object!", Globals.LogPrefix);
                Globals.LogException(e);

                settings = new Settings();
            }

            Log.Log("Loading additional settings data", Globals.LogPrefix); ;
            settings.AvailableCultures = LoadAvailableCultures(modEntry);
            settings._defaultCultureData = SettingsLocaleData.Default;

            return settings;
        }

        public SettingsLocaleData GetCultureData() => _currentCultureData ?? _defaultCultureData;

        public T GetPropertyValue<T>(string propertyName) => (T)GetType().GetProperty(propertyName)?.GetValue(this, null);

        public void Save(UnityModManager.ModEntry modEntry)
        {
            if (EnableVerboseLogging)
                Log.Log("Settings.Save: Trying to save settings", Globals.LogPrefix);

            var path = Path.Combine(modEntry.Path, Filename);
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                new XmlSerializer(typeof(Settings)).Serialize(fs, this);

            if (EnableVerboseLogging)
                Log.Log("Settings.Save: Finished saving settings", Globals.LogPrefix);
        }

        public void SetPropertyValue(string propertyName, object value) => GetType().GetProperty(propertyName)?.SetValue(this, value);

        public void UpdateCulture(UnityModManager.ModEntry modEntry)
        {
            try
            {
                if (_currentCultureData != null) return;

                Log.Log("Processing game culture information", Globals.LogPrefix);

                var ingameCulture = LocaleExtensions.GetCulture(LocalizationManager.CurrentLocale).Name.ToLowerInvariant();
                Log.Log("Ingame culture: " + ingameCulture, Globals.LogPrefix);

                _culture = AvailableCultures.FirstOrDefault(ac => ac == ingameCulture) ?? Globals.DefaultCulture;
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
