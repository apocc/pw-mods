// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;

namespace Apocc.Pw.Hotkeys.Data.Formation
{
    internal static class GuiFormation
    {
        internal static List<SettingsOption> Options = Options ?? Update();

        internal static List<SettingsOption> Update()
        {
            Options = new List<SettingsOption>
            {
                new SettingsOption($"<b>{Main.Settings.GetCultureData().LabelFormEnable}</b>", "EnableForm", SettingsOptionType.CheckBox),
                 new SettingsOption(Main.Settings.GetCultureData().LabelFormKey00, "FormKey00", SettingsOptionType.Text, "__apocc__form__00"),
                 new SettingsOption(Main.Settings.GetCultureData().LabelFormKey01, "FormKey01", SettingsOptionType.Text, "__apocc__form__01"),
                 new SettingsOption(Main.Settings.GetCultureData().LabelFormKey02, "FormKey02", SettingsOptionType.Text, "__apocc__form__02"),
                 new SettingsOption(Main.Settings.GetCultureData().LabelFormKey03, "FormKey03", SettingsOptionType.Text, "__apocc__form__03"),
                 new SettingsOption(Main.Settings.GetCultureData().LabelFormKey04, "FormKey04", SettingsOptionType.Text, "__apocc__form__04"),
                 new SettingsOption(Main.Settings.GetCultureData().LabelFormKey05, "FormKey05", SettingsOptionType.Text, "__apocc__form__05"),
                 new SettingsOption(Main.Settings.GetCultureData().LabelFormKeyCircle, "FormKeyCircle", SettingsOptionType.Text, "__apocc__form__circle"),
            };

            return Options;
        }
    }
}
