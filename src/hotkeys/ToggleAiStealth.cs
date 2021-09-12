using Kingmaker.UI.ActionBar;
using UnityEngine;

namespace Apocc.Pw.Hotkeys
{
    public static class ToggleAiStealth
    {
        public static void Run(Settings s)
        {
            if (Input.GetKeyUp(s.TaisKeyCodeStealth))
                new StealthSwitchButton().OnClick();

            if (Input.GetKeyUp(s.TaisKeyCodeAi))
                new AiSwitchButton().OnClick();
        }
    }
}
