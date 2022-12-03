// Copyright (c) apocc.
// Licensed under MIT License.

using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data
{
    internal static class GuiBuilder
    {
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
