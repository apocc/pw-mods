// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.ActionBar
{
    internal sealed class GuiActionBar
    {
        internal static List<SettingsOption> Options = Options ?? Update();

        internal static List<SettingsOption> Update()
        {
            Options = new List<SettingsOption>
            {
                new SettingsOption($"<b>{Main.Settings.GetCultureData().LabelActionBarEnable}</b>", "EnableActionBar", SettingsOptionType.CheckBox),
                new SettingsOption(Main.Settings.GetCultureData().LabelActionBarToggleAbility, "ActionBarToggleAbility", SettingsOptionType.Text, "__apocc__actionBar__toggleAbility"),
                new SettingsOption(Main.Settings.GetCultureData().LabelActionBarToggleSpells, "ActionBarToggleSpells", SettingsOptionType.Text, "__apocc__actionBar__toggleSpells"),
                new SettingsOption(Main.Settings.GetCultureData().LabelActionBarToggleQuick, "ActionBarToggleQuick", SettingsOptionType.Text, "__apocc__actionBar__toggleQuick"),
            };

            return Options;
        }
    }
}
