// Copyright (c) apocc.
// Licensed under MIT License.

using UnityEngine;

namespace Apocc.Pw.Hotkeys.Data.Formation
{
    internal static class GuiFormation
    {
        internal static void Draw()
        {
            var settings = Main.Settings;
            var cd = settings.GetCultureData();

            GUILayout.Space(Globals.GroupSpace);

            settings.EnableForm =
               GuiBuilder.BuildToggle(settings.EnableForm, cd.LabelFormEnable, null, true);

            GuiBuilder.BuildBinding(settings.FormKey00, cd.LabelFormKey00);
            GuiBuilder.BuildBinding(settings.FormKey01, cd.LabelFormKey01);
            GuiBuilder.BuildBinding(settings.FormKey02, cd.LabelFormKey02);
            GuiBuilder.BuildBinding(settings.FormKey03, cd.LabelFormKey03);
            GuiBuilder.BuildBinding(settings.FormKey04, cd.LabelFormKey04);
            GuiBuilder.BuildBinding(settings.FormKey05, cd.LabelFormKey05);
            GuiBuilder.BuildBinding(settings.FormKeyCircle, cd.LabelFormKeyCircle);

            GUILayout.Space(Globals.GroupSpace);
        }
    }
}
