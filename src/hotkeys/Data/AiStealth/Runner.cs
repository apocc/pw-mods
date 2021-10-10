// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker.UI.ActionBar;
using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.AiStealth
{
    internal static class Runner
    {
        internal static void Run()
        {
            if (Input.GetKeyUp(Main.Settings.TaisKeyStealth))
                new StealthSwitchButton().OnClick();

            if (Input.GetKeyUp(Main.Settings.TaisKeyAi))
                new AiSwitchButton().OnClick();
        }
    }
}
