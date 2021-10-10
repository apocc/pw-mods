﻿// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using Apocc.Pw.Hotkeys.Data;
using Apocc.Pw.Hotkeys.Data.ActionBar;
using Apocc.Pw.Hotkeys.Data.AiStealth;
using Apocc.Pw.Hotkeys.Data.CharacterSelect;
using Apocc.Pw.Hotkeys.Data.Formation;
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

            GUILayout.Label($"<size=15><b>{settings.GetCultureData().LabelGenNoValidation}</b></size>");
            GUILayout.Space(Globals.ControlSpace);

            var allOptions = new object[] {
                GuiWeaponSets.Options, GuiAiStealth.Options, GuiUsableItems.Options,
                GuiCharacterSelect.Options, GuiFormation.Options, GuiActionBar.Options
            };

            foreach (List<SettingsOption> optionsList in allOptions)
            {
                GuiBuilder.BuildControls(optionsList);
                GUILayout.Space(Globals.ControlSpace);
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label($"<b>{settings.GetCultureData().LabelGenVerboseLogging}</b>", GUILayout.Width(Globals.LabelWidth));
            settings.EnableVerboseLogging = GUILayout.Toggle(settings.EnableVerboseLogging, "");
            GUILayout.EndHorizontal();

            if (Event.current.keyCode != KeyCode.None && Event.current.keyCode != KeyCode.Tab)
            {
                var controlName = GUI.GetNameOfFocusedControl();
                var value = Event.current.keyCode.ToString();

                foreach (List<SettingsOption> optionsList in allOptions)
                {
                    if (GuiBuilder.UpdateControl(optionsList, controlName, value))
                        break;
                }
            }

            GUILayout.EndVertical();
        }

        internal static void OnShowGUI(bool enabled, UnityModManager.ModEntry modEntry)
        {
            Main.Settings.UpdateCulture(modEntry);

            GuiUsableItems.Update();
            GuiCharacterSelect.Update();
            GuiAiStealth.Update();
            GuiWeaponSets.Update();
            GuiFormation.Update();
            GuiActionBar.Update();
        }
    }
}
