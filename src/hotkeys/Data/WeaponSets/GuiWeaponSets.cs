// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.WeaponSets
{
    public sealed class GuiWeaponSets
    {
        internal static List<SettingsOption> Options = Options ?? Update();

        internal static List<SettingsOption> Update()
        {
            Options = new List<SettingsOption>
            {
                new SettingsOption($"<b>{Main.Settings.GetCultureData().LabelTwsEnable}</b>", "EnableTws", SettingsOptionType.CheckBox),
                new SettingsOption(Main.Settings.GetCultureData().LabelTwsKey00, "TwsKey00", SettingsOptionType.Text, "__apocc__tws__set00"),
                new SettingsOption(Main.Settings.GetCultureData().LabelTwsKey01, "TwsKey01", SettingsOptionType.Text, "__apocc__tws__set02"),
                new SettingsOption(Main.Settings.GetCultureData().LabelTwsKey02, "TwsKey02", SettingsOptionType.Text, "__apocc__tws__set03"),
                new SettingsOption(Main.Settings.GetCultureData().LabelTwsKey03, "TwsKey03", SettingsOptionType.Text, "__apocc__tws__set04"),
                new SettingsOption(Main.Settings.GetCultureData().LabelTwsCycle, "TwsToggle", SettingsOptionType.Text, "__apocc__tws__toggle",
                    Main.Settings.GetCultureData().DescTwsCircle),
                new SettingsOption(Main.Settings.GetCultureData().LabelTwsEnableForAll, "EnableAllSelectedCharacters", SettingsOptionType.CheckBox,
                    Main.Settings.GetCultureData().DescTwsEnableForAll),
                new SettingsOption(Main.Settings.GetCultureData().LabelTwsEnableFullScreen, "TwsEnableInInventory", SettingsOptionType.CheckBox, null,
                    Main.Settings.GetCultureData().DescTwsEnableFullScreen),
                new SettingsOption(Main.Settings.GetCultureData().LabelTwsEnableForceChangeInFullScreen,
                    "TwsForceChangeForAllWhenInInventory", SettingsOptionType.CheckBox, null,
                    Main.Settings.GetCultureData().DescTwsEnableForceChangeInFullScreen),
            };

            return Options;
        }
    }
}
