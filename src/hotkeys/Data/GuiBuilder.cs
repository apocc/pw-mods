// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data
{
    internal static class GuiBuilder
    {
        internal static void BuildControl(SettingsOption option)
        {
            Settings settings = Main.Settings;

            GUILayout.BeginHorizontal();

            GUILayout.Label(option.Label, GUILayout.Width(Globals.LabelWidth));

            switch (option.Type)
            {
                case SettingsOptionType.Text:
                    GUI.SetNextControlName(option.Id);

                    var valueText = GUILayout.TextField(settings.GetPropertyValue<string>(option.Property), GUILayout.Width(Globals.TextFieldWidth));
                    settings.SetPropertyValue(option.Property, valueText);

                    GUILayout.Space(Globals.ControlSpace);

                    if (GUILayout.Button(settings.GetCultureData().LabelGuiButtonClear, GUILayout.Width(Globals.ButtonWidth)))
                        settings.SetPropertyValue(option.Property, KeyCode.None.ToString());

                    break;
                case SettingsOptionType.CheckBox:
                    var valueCheck = GUILayout.Toggle(settings.GetPropertyValue<bool>(option.Property), "",
                        GUILayout.Width(Globals.ButtonWidth + Globals.TextFieldWidth + 20));
                    settings.SetPropertyValue(option.Property, valueCheck);
                    break;
                default:
                    break;
            }

            if (option.Description != null)
            {
                GUILayout.Space(Globals.ControlSpace);
                GUILayout.Label(option.Description);
            }

            GUILayout.EndHorizontal();
        }

        internal static void BuildControls(List<SettingsOption> options)
        {
            foreach (SettingsOption option in options)
                BuildControl(option);
        }

        internal static bool UpdateControl(List<SettingsOption> options, string controlName, object value)
        {
            SettingsOption option = options.FirstOrDefault(o => o.Type == SettingsOptionType.Text && o.Id == controlName);
            if (option == null)
                return false;

            Main.Settings.SetPropertyValue(option.Property, value);

            return true;
        }
    }
}
