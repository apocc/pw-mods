// Copyright (c) apocc.
// Licensed under MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

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

                    var valueText = GUILayout.TextField(
                        settings.GetFieldValue<KeyCode>(option.Property).ToString(),
                        GUILayout.Width(Globals.TextFieldWidth));

                    if (!Enum.TryParse<KeyCode>(valueText, out var kc) && settings.EnableVerboseLogging)
                        Log.Error($"Couldn't parse keycode '{valueText}' for '{option.Label}'.", Globals.LogPrefix);
                    else
                        settings.SetFieldValue(option.Property, kc);

                    GUILayout.Space(Globals.ControlSpace);

                    if (GUILayout.Button(settings.GetCultureData().LabelGuiButtonClear, GUILayout.Width(Globals.ButtonWidth)))
                        settings.SetFieldValue(option.Property, KeyCode.None);

                    break;
                case SettingsOptionType.CheckBox:
                    var valueCheck = GUILayout.Toggle(settings.GetFieldValue<bool>(option.Property), "",
                        GUILayout.Width(Globals.ButtonWidth + Globals.TextFieldWidth + 20));
                    settings.SetFieldValue(option.Property, valueCheck);
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

            switch (value)
            {
                case string st:
                    if (!Enum.TryParse<KeyCode>(st, out var kc))
                        return false;

                    Main.Settings.SetFieldValue(option.Property, kc);

                    return true; ;
            }

            Main.Settings.SetFieldValue(option.Property, value);

            return true;
        }
    }
}
