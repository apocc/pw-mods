using Kingmaker.UI.ActionBar;
using UnityEngine;

namespace Apocc.Pw.Hotkeys
{
    public static class ToggleAiStealth
    {
        public static void Run()
        {
            if (Input.GetKeyUp(Settings.Instance.TaisKeyCodeStealth))
                new StealthSwitchButton().OnClick();

            if (Input.GetKeyUp(Settings.Instance.TaisKeyCodeAi))
                new AiSwitchButton().OnClick();
        }
    }
}
