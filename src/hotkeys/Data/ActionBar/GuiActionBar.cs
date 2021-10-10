// Copyright (c) apocc.
// Licensed under MIT License.

using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.ActionBar
{
    internal sealed class GuiActionBar
    {
        internal static void Draw()
        {
            var settings = Main.Settings;
            var cd = settings.GetCultureData();

            GUILayout.Space(Globals.GroupSpace);

            settings.EnableActionBar =
               GuiBuilder.BuildToggle(settings.EnableActionBar, cd.LabelActionBarEnable, null, true);

            GuiBuilder.BuildBinding(settings.ActionBarToggleSpells, cd.LabelActionBarToggleSpells);
            GuiBuilder.BuildBinding(settings.ActionBarToggleAbility, cd.LabelActionBarToggleAbility);
            GuiBuilder.BuildBinding(settings.ActionBarToggleQuick, cd.LabelActionBarToggleQuick);

            GUILayout.Space(Globals.GroupSpace);
        }
    }
}
