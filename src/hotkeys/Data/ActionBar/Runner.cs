// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using HarmonyLib;
using Kingmaker;
using Kingmaker.UI.MVVM._PCView.ActionBar;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data.ActionBar
{
    internal enum SubmenuType
    {
        None, Ability, Spells, Quick
    }

    internal static class Runner
    {
        internal static readonly Dictionary<string, bool> VisibilityStates = new Dictionary<string, bool>();

        private static void ToggleActionBarSubmenu(SubmenuType submenuType)
        {
            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ActionBar: Toggle sub menu {submenuType}", Globals.LogPrefix);

            string path = null;
            switch (submenuType)
            {
                case SubmenuType.Ability:
                    path = "ActionBarPcView/ActionBarBottomView/ActionBarMainContainer/AbilitiesGroupsContainer/Groups/AbilityGroupView";
                    break;
                case SubmenuType.Spells:
                    path = "ActionBarPcView/ActionBarBottomView/ActionBarMainContainer/AbilitiesGroupsContainer/Groups/SpellGroupView";
                    break;
                case SubmenuType.Quick:
                    path = "ActionBarPcView/ActionBarBottomView/ActionBarMainContainer/AbilitiesGroupsContainer/Groups/ItemGroupView";
                    break;
            }

            if (path == null)
            {
                Log.Error($"ActionBar: SubmenuType is not supported '{submenuType}'!", Globals.LogPrefix);
                return;
            }

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ActionBar: Path is {path}", Globals.LogPrefix);

            var view = Game.Instance.UI.MainCanvas.transform.Find(path);
            if (view == null)
            {
                Log.Error($"ActionBar: Couldn't find prefab for view!", Globals.LogPrefix);
                return;
            }

            var bar = view.gameObject.GetComponent<ActionBarGroupPCView>();
            if (bar == null)
            {
                Log.Error($"ActionBar: Couldn't find prefab for action bar'{submenuType}'!", Globals.LogPrefix);
                return;
            }

            if (!VisibilityStates.TryGetValue(bar.name, out var visible))
                visible = false;

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ActionBar: Setting visibility to {!visible}", Globals.LogPrefix);

            bar.SetVisible(!visible);
        }

        internal static void Run()
        {
            if (Input.GetKeyUp(Main.Settings.ActionBarKeyCodeToggleAbility))
            {
                ToggleActionBarSubmenu(SubmenuType.Ability);
            }
            if (Input.GetKeyUp(Main.Settings.ActionBarKeyCodeToggleSpells))
            {
                ToggleActionBarSubmenu(SubmenuType.Spells);
            }
            if (Input.GetKeyUp(Main.Settings.ActionBarKeyCodeToggleQuick))
            {
                ToggleActionBarSubmenu(SubmenuType.Quick);
            }
        }
    }

    [HarmonyPatch(typeof(ActionBarGroupPCView))]
    public static class ActionBarGroupPCView_
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ActionBarGroupPCView.SetVisible))]
        public static void SetVisible(ActionBarGroupPCView __instance, bool ___VisibleState)
        {
            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ActionBarGroupPCView_SetVisible_Postfix_Patch: {__instance.name} visibility is {___VisibleState}", Globals.LogPrefix);

            Runner.VisibilityStates[__instance.name] = ___VisibleState;
        }
    }
}
