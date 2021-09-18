// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.AiStealth
{
    internal sealed class GuiAiStealth
    {
        internal static readonly List<SettingsOption> Options = new List<SettingsOption>
        {
            new SettingsOption("<b>Enable Toggle AI and Stealth:</b>", "EnableTAiS", SettingsOptionType.CheckBox),
            new SettingsOption("Key to toggle AI:", "TaisKeyAi", SettingsOptionType.Text, "__apocc__tais__ai"),
            new SettingsOption("Key to toggle Stealth:", "TaisKeyStealth", SettingsOptionType.Text, "__apocc__tais__stealth"),
        };
    }
}
