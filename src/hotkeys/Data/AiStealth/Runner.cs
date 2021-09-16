using Kingmaker.UI.ActionBar;
using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.AiStealth
{
    internal static class Runner
    {
        internal static void Run(Settings s)
        {
            if (Input.GetKeyUp(s.TaisKeyCodeStealth))
                new StealthSwitchButton().OnClick();

            if (Input.GetKeyUp(s.TaisKeyCodeAi))
                new AiSwitchButton().OnClick();
        }
    }
}
