using Kingmaker.UI.ActionBar;
using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.AiStealth
{
    internal static class Runner
    {
        internal static void Run()
        {
            if (Input.GetKeyUp(Main.Settings.TaisKeyCodeStealth))
                new StealthSwitchButton().OnClick();

            if (Input.GetKeyUp(Main.Settings.TaisKeyCodeAi))
                new AiSwitchButton().OnClick();
        }
    }
}
