// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.WeaponSets
{
    public sealed class GuiWeaponSets
    {
        internal static readonly List<SettingsOption> Options = new List<SettingsOption>
        {
            new SettingsOption("<b>Enable Toggle Weapon Sets:</b>", "EnableTws", SettingsOptionType.CheckBox),
            new SettingsOption("Key for weapon set 1", "TwsKey00", SettingsOptionType.Text, "__apocc__tws__set00"),
            new SettingsOption("Key for weapon set 2", "TwsKey01", SettingsOptionType.Text, "__apocc__tws__set02"),
            new SettingsOption("Key for weapon set 3", "TwsKey02", SettingsOptionType.Text, "__apocc__tws__set03"),
            new SettingsOption("Key for weapon set 4", "TwsKey03", SettingsOptionType.Text, "__apocc__tws__set04"),
            new SettingsOption("Key weapon cycle:", "TwsToggle", SettingsOptionType.Text, "__apocc__tws__toggle"),
            new SettingsOption("For all selected chararacters:", "EnableAllSelectedCharacters", SettingsOptionType.CheckBox),
            new SettingsOption("Enable in inventory:", "TwsEnableInInventory", SettingsOptionType.CheckBox),
            new SettingsOption("Force weapon set change for all selected characters even when in inventory:",
                "TwsForceChangeForAllWhenInInventory", SettingsOptionType.CheckBox),
        };
    }
}
