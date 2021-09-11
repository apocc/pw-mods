using UnityEngine;
using UnityModManagerNet;

namespace Apocc.Pw.Hotkeys
{
    internal static class Gui
    {
        private const int ButtonWidth = 75;
        private const string ClearButtonText = "Clear";
        private const int LabelWidth = 225;
        private const int TextFieldWidth = 125;

        internal static void OnGUI(bool enabled, UnityModManager.ModEntry modEntry, Settings settings)
        {
            if (!enabled) return;

            GUILayout.BeginVertical();

            GUILayout.Label("<size=15><b>There is no validation. Make sure other installed mods don't use the same key!</b></size>");
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label("<b>Enable Toggle Weapon Sets:</b>", GUILayout.Width(LabelWidth));
            settings.EnableTws = GUILayout.Toggle(settings.EnableTws, "");
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key for weapon set 1:", GUILayout.Width(LabelWidth));
            GUI.SetNextControlName("__apocc__tws__set00");
            settings.TwsKey00 = GUILayout.TextField(settings.TwsKey00, GUILayout.Width(TextFieldWidth));

            GUILayout.Space(20);
            if (GUILayout.Button(ClearButtonText, GUILayout.Width(ButtonWidth)))
                settings.TwsKey00 = KeyCode.None.ToString();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key for weapon set 2:", GUILayout.Width(LabelWidth));
            GUI.SetNextControlName("__apocc__tws__set01");
            settings.TwsKey01 = GUILayout.TextField(settings.TwsKey01, GUILayout.Width(TextFieldWidth));

            GUILayout.Space(20);
            if (GUILayout.Button(ClearButtonText, GUILayout.Width(ButtonWidth)))
                settings.TwsKey01 = KeyCode.None.ToString();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key for weapon set 3:", GUILayout.Width(LabelWidth));
            GUI.SetNextControlName("__apocc__tws__set02");
            settings.TwsKey02 = GUILayout.TextField(settings.TwsKey02, GUILayout.Width(TextFieldWidth));

            GUILayout.Space(20);
            if (GUILayout.Button(ClearButtonText, GUILayout.Width(ButtonWidth)))
                settings.TwsKey02 = KeyCode.None.ToString();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key for weapon set 4:", GUILayout.Width(LabelWidth));
            GUI.SetNextControlName("__apocc__tws__set03");
            settings.TwsKey03 = GUILayout.TextField(settings.TwsKey03, GUILayout.Width(TextFieldWidth));

            GUILayout.Space(20);
            if (GUILayout.Button(ClearButtonText, GUILayout.Width(ButtonWidth)))
                settings.TwsKey03 = KeyCode.None.ToString();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key weapon cycle:", GUILayout.Width(LabelWidth));
            GUI.SetNextControlName("__apocc__tws__toggle");
            settings.TwsToggle = GUILayout.TextField(settings.TwsToggle, GUILayout.Width(TextFieldWidth));

            GUILayout.Space(20);
            if (GUILayout.Button(ClearButtonText, GUILayout.Width(ButtonWidth)))
                settings.TwsToggle = KeyCode.None.ToString();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("For all selected chararacters:", GUILayout.Width(LabelWidth));
            settings.EnableAllSelectedCharacters = GUILayout.Toggle(settings.EnableAllSelectedCharacters, "");
            GUILayout.EndHorizontal();

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label("<b>Enable Toggle AI and Stealth:</b>", GUILayout.Width(LabelWidth));
            settings.EnableTAiS = GUILayout.Toggle(settings.EnableTAiS, "");
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key to toggle AI:", GUILayout.Width(LabelWidth));
            GUI.SetNextControlName("__apocc__tais__ai");
            settings.TaisKeyAi = GUILayout.TextField(settings.TaisKeyAi, GUILayout.Width(TextFieldWidth));

            GUILayout.Space(20);
            if (GUILayout.Button(ClearButtonText, GUILayout.Width(ButtonWidth)))
                settings.TaisKeyAi = KeyCode.None.ToString();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key to toggle Stealth:", GUILayout.Width(LabelWidth));
            GUI.SetNextControlName("__apocc__tais__stealth");
            settings.TaisKeyStealth = GUILayout.TextField(settings.TaisKeyStealth, GUILayout.Width(TextFieldWidth));

            GUILayout.Space(20);
            if (GUILayout.Button(ClearButtonText, GUILayout.Width(ButtonWidth)))
                settings.TaisKeyStealth = KeyCode.None.ToString();

            GUILayout.EndHorizontal();

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label("<b>Enable verbose logging:</b>", GUILayout.Width(LabelWidth));
            settings.EnableVerboseLogging = GUILayout.Toggle(settings.EnableVerboseLogging, "");
            GUILayout.EndHorizontal();

            if (Event.current.keyCode != KeyCode.None && Event.current.keyCode != KeyCode.Tab)
            {
                switch (GUI.GetNameOfFocusedControl())
                {
                    case "__apocc__tws__set00":
                        settings.TwsKey00 = Event.current.keyCode.ToString();
                        break;
                    case "__apocc__tws__set01":
                        settings.TwsKey01 = Event.current.keyCode.ToString();
                        break;
                    case "__apocc__tws__set02":
                        settings.TwsKey02 = Event.current.keyCode.ToString();
                        break;
                    case "__apocc__tws__set03":
                        settings.TwsKey03 = Event.current.keyCode.ToString();
                        break;
                    case "__apocc__tws__toggle":
                        settings.TwsToggle = Event.current.keyCode.ToString();
                        break;
                    case "__apocc__tais__ai":
                        settings.TaisKeyAi = Event.current.keyCode.ToString();
                        break;
                    case "__apocc__tais__stealth":
                        settings.TaisKeyStealth = Event.current.keyCode.ToString();
                        break;
                }
            }

            GUILayout.EndVertical();
        }
    }
}
