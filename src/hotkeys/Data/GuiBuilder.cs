// Copyright (c) apocc.
// Licensed under MIT License.

using UnityEngine;
using UnityModManagerNet;

namespace Apocc.Pw.Hotkeys.Data
{
    internal static class GuiBuilder
    {
        internal static void BuildBinding(KeyBinding binding, string label, string desc = null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, GUILayout.Width(Globals.LabelWidth));
            UnityModManager.UI.DrawKeybinding(ref binding, label, null, GUILayout.Width(Globals.TextFieldWidth));

            if (desc != null)
            {
                GUILayout.Space(Globals.ControlSpace);
                GUILayout.Label(desc);
            }

            GUILayout.EndHorizontal();
        }

        internal static bool BuildToggle(bool value, string label, string desc = null, bool bold = false)
        {
            GUILayout.BeginHorizontal();

            var lbl = bold ? $"<b>{label}</b>" : label;
            GUILayout.Label(lbl, GUILayout.Width(Globals.LabelWidth));
            var check = GUILayout.Toggle(value, "", GUILayout.Width(270));

            if (desc != null)
            {
                GUILayout.Space(Globals.ControlSpace);
                GUILayout.Label(desc);
            }

            GUILayout.EndHorizontal();

            return check;
        }
    }
}
