// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using Apocc.Pw.Hotkeys.Data;
using Apocc.Pw.Hotkeys.Data.ActionBar;
using Apocc.Pw.Hotkeys.Data.CharacterSelect;
using Apocc.Pw.Hotkeys.Data.Formation;
using Apocc.Pw.Hotkeys.Data.UsableItems;
using UnityEngine;
using UnityModManagerNet;

namespace Apocc.Pw.Hotkeys
{
    internal static class Gui
    {
        private static readonly Dictionary<string, bool> GroupingVisibility = new Dictionary<string, bool>();

        private static bool Grouping(string id, bool defaultState = true)
        {
            if (!GroupingVisibility.ContainsKey(id))
                GroupingVisibility[id] = defaultState;

            var current = GroupingVisibility[id];
            var mod = current ? "-" : "+";

            return GroupingVisibility[id] = GUILayout.Button($"<b>{mod} {id} {mod}</b>")
                ? !current
                : current;
        }

        internal static void OnGUI(bool enabled, UnityModManager.ModEntry modEntry)
        {
            if (!enabled) return;

            Settings settings = Main.Settings;
            var cd = settings.GetCultureData();

            GUILayout.BeginVertical();

            GUILayout.Label($"<size=15><b>{settings.GetCultureData().LabelGenNoValidation}</b></size>");

            settings.EnableVerboseLogging = GuiBuilder.BuildToggle(settings.EnableVerboseLogging, cd.LabelGenVerboseLogging, null, true);

            GUILayout.Space(Globals.ControlSpace);

            GUILayout.EndVertical();

            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();

            var visibleQs = Grouping(cd.GroupQs);

            GUILayout.EndHorizontal();

            if (visibleQs)
                GuiUsableItems.Draw();

            GUILayout.BeginHorizontal();

            var visibleCs = Grouping(cd.GroupCs);

            GUILayout.EndHorizontal();

            if (visibleCs)
                GuiCharacterSelect.Draw();

            GUILayout.BeginHorizontal();

            var visibleForm = Grouping(cd.GroupForm);

            GUILayout.EndHorizontal();

            if (visibleForm)
                GuiFormation.Draw();

            GUILayout.BeginHorizontal();

            var visibleAc = Grouping(cd.GroupAc);

            GUILayout.EndHorizontal();

            if (visibleAc)
                GuiActionBar.Draw();

            GUILayout.EndVertical();
        }

        internal static void OnShowGUI(bool enabled, UnityModManager.ModEntry modEntry)
        {
            if (!enabled) return;

            Main.Settings.UpdateCulture(modEntry);
        }
    }
}
