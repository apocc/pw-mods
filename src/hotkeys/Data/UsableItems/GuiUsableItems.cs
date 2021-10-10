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
                new SettingsOption($"<b>{Main.Settings.GetCultureData().LabelUsitEnable}</b>",nameof(Settings.EnableUsit), SettingsOptionType.CheckBox, null,
                    Main.Settings.GetCultureData().DescUsitMain),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey00, nameof(Settings.UsitSlot00), SettingsOptionType.Text, "__apocc__usit__slot00"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey01, nameof(Settings.UsitSlot01), SettingsOptionType.Text, "__apocc__usit__slot01"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey02, nameof(Settings.UsitSlot02), SettingsOptionType.Text, "__apocc__usit__slot02"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey03, nameof(Settings.UsitSlot03), SettingsOptionType.Text, "__apocc__usit__slot03"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitKey04, nameof(Settings.UsitSlot04), SettingsOptionType.Text, "__apocc__usit__slot04"),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitEnableForAll, nameof(Settings.UsitEnableAllSelectedCharacters),
                    SettingsOptionType.CheckBox),
                new SettingsOption(Main.Settings.GetCultureData().LabelUsitUseActonBarPlacement, nameof(Settings.UsitUseActionBarPlacement),
                    SettingsOptionType.CheckBox, null, Main.Settings.GetCultureData().DescUsitUseActonBarPlacement),
            };

            return Options;
        }
    }
}
