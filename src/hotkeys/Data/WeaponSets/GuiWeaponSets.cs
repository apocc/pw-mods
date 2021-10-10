// Copyright (c) apocc.
// Licensed under MIT License.

using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.WeaponSets
{
    public sealed class GuiWeaponSets
    {
        internal static void Draw()
        {
            var settings = Main.Settings;
            var cd = settings.GetCultureData();

            GUILayout.Space(Globals.GroupSpace);

            settings.EnableTws =
               GuiBuilder.BuildToggle(settings.EnableTws, cd.LabelTwsEnable, null, true);

            GuiBuilder.BuildBinding(settings.TwsKey00, cd.LabelTwsKey00);
            GuiBuilder.BuildBinding(settings.TwsKey01, cd.LabelTwsKey01);
            GuiBuilder.BuildBinding(settings.TwsKey02, cd.LabelTwsKey02);
            GuiBuilder.BuildBinding(settings.TwsKey03, cd.LabelTwsKey03);
            GuiBuilder.BuildBinding(settings.TwsToggle, cd.LabelTwsCycle);

            settings.TwsEnableAllSelectedCharacters =
                GuiBuilder.BuildToggle(settings.TwsEnableAllSelectedCharacters, cd.LabelTwsEnableForAll, cd.DescTwsEnableForAll);
            settings.TwsEnableInInventory =
                GuiBuilder.BuildToggle(settings.TwsEnableInInventory, cd.LabelTwsEnableFullScreen, cd.DescTwsEnableFullScreen);
            settings.TwsForceChangeForAllWhenInInventory =
                GuiBuilder.BuildToggle(settings.TwsForceChangeForAllWhenInInventory,
                cd.LabelTwsEnableForceChangeInFullScreen, cd.DescTwsEnableForceChangeInFullScreen);

            GUILayout.Space(Globals.GroupSpace);
        }
    }
}
