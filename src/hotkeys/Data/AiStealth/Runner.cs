// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker.UI.ActionBar;

namespace Apocc.Pw.Hotkeys.Data.AiStealth
{
    internal static class Runner
    {
        internal static void Run()
        {
            if (Main.Settings.TaisStealth.Up())
                new StealthSwitchButton().OnClick();

            if (Main.Settings.TaisAi.Up())
                new AiSwitchButton().OnClick();
        }
    }
}
