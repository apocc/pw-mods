using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.UsableItems
{
    internal sealed class GuiUsableItems
    {
        internal static readonly List<SettingsOption> Options = new List<SettingsOption>
        {
            new SettingsOption("<b>Enable Usable Items hotkeys:</b>", "EnableUsit", SettingsOptionType.CheckBox),
            new SettingsOption("Key Slot 1", "UsitSlot00", SettingsOptionType.Text, "__apocc__usit__slot00"),
            new SettingsOption("Key Slot 2", "UsitSlot01", SettingsOptionType.Text, "__apocc__usit__slot01"),
            new SettingsOption("Key Slot 3", "UsitSlot02", SettingsOptionType.Text, "__apocc__usit__slot02"),
            new SettingsOption("Key Slot 4", "UsitSlot03", SettingsOptionType.Text, "__apocc__usit__slot03"),
            new SettingsOption("Key Slot 5", "UsitSlot04", SettingsOptionType.Text, "__apocc__usit__slot04"),
            new SettingsOption("For all selected chararacters:", "UsitEnableAllSelectedCharacters", SettingsOptionType.CheckBox),
            new SettingsOption("Use action bar placement:", "UsitUseActionBarPlacement", SettingsOptionType.CheckBox),
        };
    }
}
