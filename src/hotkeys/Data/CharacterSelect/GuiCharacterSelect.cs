// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.CharacterSelect
{
    internal sealed class GuiCharacterSelect
    {
        internal static List<SettingsOption> Options = Options ?? Update();

        internal static List<SettingsOption> Update()
        {
            Options = new List<SettingsOption>
            {
                new SettingsOption($"<b>{Main.Settings.GetCultureData().LabelCsEnable}</b>",
                    nameof(Settings.EnableCharSel), SettingsOptionType.CheckBox),
                new SettingsOption(Main.Settings.GetCultureData().LabelCsNext,
                    nameof(Settings.CsNext), SettingsOptionType.Text, "__apocc__cs__next"),
                new SettingsOption(Main.Settings.GetCultureData().LabelCsPrev,
                    nameof(Settings.CsPrev), SettingsOptionType.Text, "__apocc__cs__prev"),
            };

            return Options;
        }
    }
}
