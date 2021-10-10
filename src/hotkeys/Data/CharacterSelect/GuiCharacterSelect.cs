// Copyright (c) apocc.
// Licensed under MIT License.

using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.CharacterSelect
{
    internal sealed class GuiCharacterSelect
    {
        internal static void Draw()
        {
            var settings = Main.Settings;
            var cd = settings.GetCultureData();

            GUILayout.Space(Globals.GroupSpace);

            settings.EnableCharSel =
               GuiBuilder.BuildToggle(settings.EnableCharSel, cd.LabelCsEnable, null, true);

            GuiBuilder.BuildBinding(settings.CsPrev, cd.LabelCsPrev);
            GuiBuilder.BuildBinding(settings.CsNext, cd.LabelCsNext);

            GUILayout.Space(Globals.GroupSpace);
        }
    }
}
