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
        private static string _keyTitle = Utilities.GetKey("title");
        private static string _keyBtnVerboseDesc = Utilities.GetKey("btn.verbose.desc");
        private static string _keyBtnVerboseText = Utilities.GetKey("btn.verbose.text");

        private CultureInfo _ci;
        private bool _initialized = false;
        private SettingsBuilder _settings;
        private static readonly ModLockEntry[] _entriesEnGB = new ModLockEntry[]
        {
            new ModLockEntry("apocchotkeys.settings.btn.verbose.desc",""),
            new ModLockEntry("apocchotkeys.settings.btn.verbose.text",""),

            new ModLockEntry(AiStealth.PKeyHeader, "Ai and Stealth"),
            new ModLockEntry(AiStealth.PKeyBtnEnableDesc, "Enable Ai and Stealth hotkeys"),
            new ModLockEntry(AiStealth.PKeyKbAiDesc, "AI hotkey"),
            new ModLockEntry(AiStealth.PKeyKbStealthDesc, "Stealth hotkey"),
            new ModLockEntry("btn",""),
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
