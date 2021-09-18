// Copyright (c) apocc.
// Licensed under MIT License.

using Apocc.Pw.Hotkeys.Data;
using Apocc.Pw.Hotkeys.Data.AiStealth;
using Apocc.Pw.Hotkeys.Data.CharacterSelect;
using Apocc.Pw.Hotkeys.Data.UsableItems;
using Apocc.Pw.Hotkeys.Data.WeaponSets;
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

            GUILayout.Label("<size=15><b>There is no validation. Make sure other installed mods don't use the same key!</b></size>");
            GUILayout.Space(20);

            GuiBuilder.BuildControls(GuiWeaponSets.Options);

            GUILayout.Space(20);

            GuiBuilder.BuildControls(GuiAiStealth.Options);

            GUILayout.Space(20);

            GuiBuilder.BuildControls(GuiUsableItems.Options);

            GUILayout.Space(20);

            GuiBuilder.BuildControls(GuiCharacterSelect.Options);

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label("<b>Enable verbose logging:</b>", GUILayout.Width(Globals.LabelWidth));
            settings.EnableVerboseLogging = GUILayout.Toggle(settings.EnableVerboseLogging, "");
            GUILayout.EndHorizontal();

            if (Event.current.keyCode != KeyCode.None && Event.current.keyCode != KeyCode.Tab)
            {
                var controlName = GUI.GetNameOfFocusedControl();
                var value = Event.current.keyCode.ToString();

                GuiBuilder.UpdateControl(GuiWeaponSets.Options, controlName, value);
                GuiBuilder.UpdateControl(GuiAiStealth.Options, controlName, value);
                GuiBuilder.UpdateControl(GuiUsableItems.Options, controlName, value);
                GuiBuilder.UpdateControl(GuiCharacterSelect.Options, controlName, value);
            }

            GUILayout.EndVertical();
        }
    }
}
