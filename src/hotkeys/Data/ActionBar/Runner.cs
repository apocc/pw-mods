// Copyright (c) apocc.
// Licensed under MIT License.

using System;
using System.Collections.Generic;
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
        private static List<IDisposable> Disposables = new List<IDisposable>();
        private static SubmenuType SubmenuVisible = SubmenuType.None;

        private static void ResetSubmenu(ActionBarGroupPCView bar)
        {
            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ActionBar: Resetting submenu", Globals.LogPrefix);

            SubmenuVisible = SubmenuType.None;
            bar.SetVisible(false);
        }

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

            var visible = submenuType != SubmenuVisible;
            SubmenuVisible = submenuType == SubmenuVisible ? SubmenuType.None : submenuType;

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ActionBar: Setting visibility to {visible}", Globals.LogPrefix);

            bar.SetVisible(visible);

            if (visible)
            {
                if (Main.Settings.EnableVerboseLogging)
                    Log.Log($"ActionBar: Adding ESC disposable", Globals.LogPrefix);

                Disposables.Add(Game.Instance.UI.EscManager.Subscribe(() => ResetSubmenu(bar)));
            }
            else
            {
                if (Main.Settings.EnableVerboseLogging)
                    Log.Log($"ActionBar: Removing ESC disposable", Globals.LogPrefix);

                Game.Instance.UI.EscManager.Unsubscribe(() => ResetSubmenu(bar));
                Disposables.ForEach(d => d?.Dispose());
                Disposables.Clear();
            }
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
}
