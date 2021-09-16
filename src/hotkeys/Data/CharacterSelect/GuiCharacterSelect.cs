using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.CharacterSelect
{
    internal sealed class GuiCharacterSelect
    {
        internal static readonly List<SettingsOption> Options = new List<SettingsOption>
        {
            new SettingsOption("<b>Enable character selection:</b>", "EnableCharSel", SettingsOptionType.CheckBox),
            new SettingsOption("Key to select next character:", "CsNext", SettingsOptionType.Text, "__apocc__cs__next"),
            new SettingsOption("Key to select previous character:", "CsPrev", SettingsOptionType.Text, "__apocc__cs__prev"),
        };
    }
}
