// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker;
using Kingmaker.GameModes;
using Kingmaker.UI.ActionBar;
using ModMenu.Settings;

using GameModesGroup = Kingmaker.UI.KeyboardAccess.GameModesGroup;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data
{
    internal sealed class AiStealth
    {
        internal static readonly string KeyBtnEnable = Utilities.GetKey("ais.btn.enable");
        internal static readonly string KeyKbAi = Utilities.GetKey("ais.kb.ai");
        internal static readonly string KeyKbStealth = Utilities.GetKey("ais.kb.stealth");

        internal static readonly string KeyBtnEnableTitle = Utilities.GetKey("ais.btn.enable.title");
        internal static readonly string KeyHeader = Utilities.GetKey("ais.header");
        internal static readonly string KeyKbAiTitle = Utilities.GetKey("ais.kb.ai.title");
        internal static readonly string KeyKbStealthTitle = Utilities.GetKey("ais.kb.stealth.title");

        internal static void AddModMenuSettings(SettingsBuilder sb)
        {
            Log.Log("Initializing ai settings", Globals.LogPrefix);

            sb.AddSubHeader(Utilities.GetString(KeyHeader))
                .AddToggle(
                    Toggle
                        .New(KeyBtnEnable, false, Utilities.KeyBtnEnableString).WithLongDescription(Utilities.GetString(KeyBtnEnableTitle)))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbAi, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbAiTitle)), OnAiKeyPress)
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbStealth, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbStealthTitle)), OnStealthKeyPress);
        }

        private static void OnAiKeyPress()
        {
            if (Game.Instance.CurrentMode != GameModeType.FullScreenUi && ModMenu.ModMenu.GetSettingValue<bool>(KeyBtnEnable))
                new AiSwitchButton().OnClick();
        }

        private static void OnStealthKeyPress()
        {
            if (Game.Instance.CurrentMode != GameModeType.FullScreenUi && ModMenu.ModMenu.GetSettingValue<bool>(KeyBtnEnable))
                new StealthSwitchButton().OnClick();
        }
    }
}
