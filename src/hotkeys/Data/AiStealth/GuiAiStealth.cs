// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.AiStealth
{
    internal sealed class GuiAiStealth
    {
        internal static List<SettingsOption> Options = Options ?? Update();

        internal static List<SettingsOption> Update()
        {
            Options = new List<SettingsOption>
            {
                new SettingsOption($"<b>{Main.Settings.GetCultureData().LabelTaisEnable}</b>",
                    nameof(Settings.EnableTAiS), SettingsOptionType.CheckBox),
                new SettingsOption(Main.Settings.GetCultureData().LabelTaisKeyAi,
                    nameof(Settings.TaisKeyAi), SettingsOptionType.Text, "__apocc__tais__ai"),
                new SettingsOption(Main.Settings.GetCultureData().LabelTaisKeyStealth,
                    nameof(Settings.TaisKeyStealth), SettingsOptionType.Text, "__apocc__tais__stealth"),
            };

            return Options;
        }
    }
}
