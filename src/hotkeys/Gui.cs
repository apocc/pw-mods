// Copyright (c) apocc.
// Licensed under MIT License.

using Apocc.Pw.Hotkeys.Data;
using UnityEngine;
using UnityModManagerNet;

namespace Apocc.Pw.Hotkeys
{
    internal static class Gui
    {
        internal static void OnGUI(bool enabled, UnityModManager.ModEntry modEntry)
        {
            if (!enabled) return;

            Settings settings = Main.Settings;

            GUILayout.BeginVertical();

            settings.EnableVerboseLogging = GuiBuilder.BuildToggle(settings.EnableVerboseLogging, "Enable verbose logging", null, true);

            GUILayout.EndVertical();
        }
    }
}
