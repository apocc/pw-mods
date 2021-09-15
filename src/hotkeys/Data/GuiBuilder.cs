using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data
{
    internal static class GuiBuilder
    {
        internal static void BuildControl(SettingsOption option, Settings settings)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(option.Label, GUILayout.Width(Globals.LabelWidth));

            switch (option.Type)
            {
                case SettingsOptionType.Text:
                    GUI.SetNextControlName(option.Id);

                    var valueText = GUILayout.TextField(settings.GetPropertyValue<string>(option.Property), GUILayout.Width(Globals.TextFieldWidth));
                    settings.SetPropertyValue(option.Property, valueText);

                    GUILayout.Space(20);

                    if (GUILayout.Button(Globals.ClearButtonText, GUILayout.Width(Globals.ButtonWidth)))
                        settings.SetPropertyValue(option.Property, KeyCode.None.ToString());

                    break;
                case SettingsOptionType.CheckBox:
                    var valueCheck = GUILayout.Toggle(settings.GetPropertyValue<bool>(option.Property), "");
                    settings.SetPropertyValue(option.Property, valueCheck);
                    break;
                default:
                    break;
            }

            GUILayout.EndHorizontal();
        }

        internal static void BuildControls(List<SettingsOption> options, Settings settings)
        {
            foreach (SettingsOption option in options)
                BuildControl(option, settings);
        }

        internal static void UpdateControl(List<SettingsOption> options, string controlName, object value, Settings settings)
        {
            SettingsOption option = options.FirstOrDefault(o => o.Type == SettingsOptionType.Text && o.Id == controlName);
            if (option != null)
                settings.SetPropertyValue(option.Property, value);
        }
    }
}
