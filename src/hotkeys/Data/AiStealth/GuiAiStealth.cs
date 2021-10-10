// Copyright (c) apocc.
// Licensed under MIT License.

using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.AiStealth
{
    internal sealed class GuiAiStealth
    {
        internal static void Draw()
        {
            var settings = Main.Settings;
            var cd = settings.GetCultureData();

            GUILayout.Space(Globals.GroupSpace);

            settings.EnableTAiS =
               GuiBuilder.BuildToggle(settings.EnableTAiS, cd.LabelTaisEnable, null, true);

            GuiBuilder.BuildBinding(settings.TaisAi, cd.LabelTaisKeyAi);
            GuiBuilder.BuildBinding(settings.TaisStealth, cd.LabelTaisKeyStealth);

            GUILayout.Space(Globals.GroupSpace);
        }
    }
}
