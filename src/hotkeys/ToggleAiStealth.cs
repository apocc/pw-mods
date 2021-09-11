using Kingmaker.UI.ActionBar;
using UnityEngine;

namespace Apocc.Pw.Hotkeys
{
    public static class ToggleAiStealth
    {
        public static void Run(Settings settings)
        {
            if (Input.GetKeyUp(settings.TaisKeyCodeStealth))
                new StealthSwitchButton().OnClick();

            if (Input.GetKeyUp(settings.TaisKeyCodeAi))
                new AiSwitchButton().OnClick();
        }
    }
}
