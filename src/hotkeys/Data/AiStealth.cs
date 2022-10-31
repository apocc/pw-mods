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

        internal static readonly string KeyBtnEnableDesc = Utilities.GetKey("ais.btn.enable.desc");
        internal static readonly string KeyHeader = Utilities.GetKey("ais.header");
        internal static readonly string KeyKbAiDesc = Utilities.GetKey("ais.kb.ai.desc");
        internal static readonly string KeyKbStealthDesc = Utilities.GetKey("ais.kb.stealth.desc");

        internal static void AddModMenuSettings(SettingsBuilder sb)
        {
            Log.Log("Initializing ai settings", Globals.LogPrefix);

            sb.AddSubHeader(Utilities.GetString(KeyHeader))
                .AddToggle(
                    Toggle
                        .New(KeyBtnEnable, false, Utilities.GetString(KeyBtnEnableDesc)))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbAi, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbAiDesc)), OnAiKeyPress)
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbStealth, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbStealthDesc)), OnStealthKeyPress);
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
