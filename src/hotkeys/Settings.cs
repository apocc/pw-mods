// Copyright (c) apocc.
// Licensed under MIT License.

using UnityModManagerNet;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public sealed class Settings : UnityModManager.ModSettings
    {
        public bool EnableVerboseLogging = false;

        public static Settings LoadSettings(UnityModManager.ModEntry modEntry)
        {
            Log.Log("Trying to load settings", Globals.LogPrefix);

            var settings = Load<Settings>(modEntry);

            return settings;
        }

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            if (EnableVerboseLogging)
                Log.Log("Settings.Save: Trying to save settings", Globals.LogPrefix);

            Save(this, modEntry);

            if (EnableVerboseLogging)
                Log.Log("Settings.Save: Finished saving settings", Globals.LogPrefix);
        }
    }
}
