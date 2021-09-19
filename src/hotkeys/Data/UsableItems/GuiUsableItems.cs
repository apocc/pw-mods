// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.UsableItems
{
    internal sealed class GuiUsableItems
    {
        internal static List<SettingsOption> Options = Options ?? Update();

        internal static List<SettingsOption> Update()
        {
            Options = new List<SettingsOption>
            {
                new SettingsOption($"<b>{Main.Settings.GetCultureData().LabelUsitEnable}</b>", "EnableUsit", SettingsOptionType.CheckBox, null,
                    Main.Settings.GetCultureData().DescUsitMain),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey00, "UsitSlot00", SettingsOptionType.Text, "__apocc__usit__slot00"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey01, "UsitSlot01", SettingsOptionType.Text, "__apocc__usit__slot01"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey02, "UsitSlot02", SettingsOptionType.Text, "__apocc__usit__slot02"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey03, "UsitSlot03", SettingsOptionType.Text, "__apocc__usit__slot03"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey04, "UsitSlot04", SettingsOptionType.Text, "__apocc__usit__slot04"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitEnableForAll, "UsitEnableAllSelectedCharacters", SettingsOptionType.CheckBox),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitUseActonBarPlacement, "UsitUseActionBarPlacement", SettingsOptionType.CheckBox, null,
                    Main.Settings.GetCultureData().DescUsitUseActonBarPlacement),
            };

            return Options;
        }
    }
}
