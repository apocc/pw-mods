using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityModManagerNet;

namespace Apocc.Pw.Hotkeys
{
    public sealed class Settings
    {
        private static readonly string KeyCodeNone = KeyCode.None.ToString();

        private string _keyAi = KeyCodeNone;
        private string _keySet00 = KeyCodeNone;
        private string _keySet01 = KeyCodeNone;
        private string _keySet02 = KeyCodeNone;
        private string _keySet03 = KeyCodeNone;
        private string _keySetToggle = KeyCodeNone;
        private string _keyStealth = KeyCodeNone;
        private UnityModManager.ModEntry.ModLogger _log;
        internal const string Filename = "settings.xml";
        public bool EnableAllSelectedCharacters { get; set; }
        [XmlAttribute("enableTAiS")]
        public bool EnableTAiS { get; set; } = true;
        [XmlAttribute("enableTws")]
        public bool EnableTws { get; set; } = true;
        [XmlAttribute("verbose")]
        public bool EnableVerboseLogging { get; set; }
        public string TaisKeyAi { get => _keyAi; set => SetProperty(ref _keyAi, value); }
        [XmlIgnore]
        public KeyCode TaisKeyCodeAi { get; set; }
        [XmlIgnore]
        public KeyCode TaisKeyCodeStealth { get; set; }
        public string TaisKeyStealth { get => _keyStealth; set => SetProperty(ref _keyStealth, value); }
        public string TwsKey00 { get => _keySet00; set => SetProperty(ref _keySet00, value); }
        public string TwsKey01 { get => _keySet01; set => SetProperty(ref _keySet01, value); }
        public string TwsKey02 { get => _keySet02; set => SetProperty(ref _keySet02, value); }
        public string TwsKey03 { get => _keySet03; set => SetProperty(ref _keySet03, value); }
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
        public string TwsToggle { get => _keySetToggle; set => SetProperty(ref _keySetToggle, value); }

        private void SetProperty(ref string prop, string value, [CallerMemberName] string propertyName = null)
        {
            if (ReferenceEquals(prop, value)) return;
            prop = value;

            if (propertyName == null)
            {
                if (EnableVerboseLogging) _log.Warning("Settings.SetProperty => propertyName is null!");
                return;
            }

            if (!Enum.TryParse(prop, out KeyCode kc))
            {
                if (EnableVerboseLogging) _log.Warning($"Settings.SetProperty => Could not parse key code '{prop}' for property {propertyName}!");
                return;
            }

            if (EnableVerboseLogging) _log.Log($"Successfully parsed key code '{prop}' for property '{propertyName}'");

            switch (propertyName)
            {
                case "TwsKey00": TwsKeyCode00 = kc; break;
                case "TwsKey01": TwsKeyCode01 = kc; break;
                case "TwsKey02": TwsKeyCode02 = kc; break;
                case "TwsKey03": TwsKeyCode03 = kc; break;
                case "TwsToggle": TwsKeyCodeToggle = kc; break;
                case "TaisKeyAi": TaisKeyCodeAi = kc; break;
                case "TaisKeyStealth": TaisKeyCodeStealth = kc; break;
            }
        }

        internal void SetLog(UnityModManager.ModEntry.ModLogger logger) => _log = logger;

        public static Settings Load(UnityModManager.ModEntry modEntry)
        {
            var path = Path.Combine(modEntry.Path, Filename);
            if (!File.Exists(path))
            {
                modEntry.Logger.Log("No settings file found, loading default settings.");
                return new Settings();
            }

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                return new XmlSerializer(typeof(Settings)).Deserialize(fs) as Settings;
        }

        public void Save(UnityModManager.ModEntry modEntry)
        {
            if (EnableVerboseLogging)
                _log.Log("Settings.Save: Trying to save settings");

            var path = Path.Combine(modEntry.Path, Filename);
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                new XmlSerializer(typeof(Settings)).Serialize(fs, this);

            if (EnableVerboseLogging)
                _log.Log("Settings.Save: Finished saving settings");
        }
    }
}
