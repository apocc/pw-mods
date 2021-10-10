// Copyright (c) apocc.
// Licensed under MIT License.

using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.UsableItems
{
    internal sealed class GuiUsableItems
    {
        internal static void Draw()
        {
            var settings = Main.Settings;
            var cd = settings.GetCultureData();

            GUILayout.Space(Globals.GroupSpace);

            settings.EnableUsit =
               GuiBuilder.BuildToggle(settings.EnableUsit, cd.LabelUsitEnable, cd.DescUsitMain, true);

            GuiBuilder.BuildBinding(settings.UsitSlot00, cd.LabelUsitKey00);
            GuiBuilder.BuildBinding(settings.UsitSlot01, cd.LabelUsitKey01);
            GuiBuilder.BuildBinding(settings.UsitSlot02, cd.LabelUsitKey02);
            GuiBuilder.BuildBinding(settings.UsitSlot03, cd.LabelUsitKey03);
            GuiBuilder.BuildBinding(settings.UsitSlot04, cd.LabelUsitKey04);

            settings.UsitEnableAllSelectedCharacters =
               GuiBuilder.BuildToggle(settings.UsitEnableAllSelectedCharacters, cd.LabelUsitEnableForAll);

            settings.UsitUseActionBarPlacement =
               GuiBuilder.BuildToggle(settings.UsitUseActionBarPlacement, cd.LabelUsitUseActonBarPlacement, cd.DescUsitUseActonBarPlacement);

            GUILayout.Space(Globals.GroupSpace);
        }
    }
}
