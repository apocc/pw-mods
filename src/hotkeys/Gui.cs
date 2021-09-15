using Apocc.Pw.Hotkeys.Data;
using Apocc.Pw.Hotkeys.Data.AiStealth;
using Apocc.Pw.Hotkeys.Data.UsableItems;
using Apocc.Pw.Hotkeys.Data.WeaponSets;
using UnityEngine;
using UnityModManagerNet;

namespace Apocc.Pw.Hotkeys
{
    internal static class Gui
    {
        internal static void OnGUI(bool enabled, UnityModManager.ModEntry modEntry, Settings settings)
        {
            if (!enabled) return;

            GUILayout.BeginVertical();

            GUILayout.Label("<size=15><b>There is no validation. Make sure other installed mods don't use the same key!</b></size>");
            GUILayout.Space(20);

            GuiBuilder.BuildControls(GuiWeaponSets.Options, settings);

            GUILayout.Space(20);

            GuiBuilder.BuildControls(GuiAiStealth.Options, settings);

            GUILayout.Space(20);

            GuiBuilder.BuildControls(GuiUsableItems.Options, settings);

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label("<b>Enable verbose logging:</b>", GUILayout.Width(Globals.LabelWidth));
            settings.EnableVerboseLogging = GUILayout.Toggle(settings.EnableVerboseLogging, "");
            GUILayout.EndHorizontal();

            if (Event.current.keyCode != KeyCode.None && Event.current.keyCode != KeyCode.Tab)
            {
                var controlName = GUI.GetNameOfFocusedControl();
                var value = Event.current.keyCode.ToString();

                GuiBuilder.UpdateControl(GuiWeaponSets.Options, controlName, value, settings);
                GuiBuilder.UpdateControl(GuiAiStealth.Options, controlName, value, settings);
                GuiBuilder.UpdateControl(GuiUsableItems.Options, controlName, value, settings);
            }

            GUILayout.EndVertical();
        }
    }
}
