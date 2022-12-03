// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using HarmonyLib;
using Kingmaker;
using Kingmaker.UI.MVVM._PCView.ActionBar;
using ModMenu.Settings;

using static Kingmaker.UI.KeyboardAccess;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data
{
    internal enum SubmenuType
    {
        None, Ability, Spells, Quick
    }

    internal static class ActionBar
    {
        internal static readonly Dictionary<string, bool> VisibilityStates = new Dictionary<string, bool>();

        internal static readonly string KeyBtnEnable = Utilities.GetKey("ab.btn.enable");
        internal static readonly string KeyKbAbility = Utilities.GetKey("ab.kb.ablility");
        internal static readonly string KeyKbSpells = Utilities.GetKey("ab.kb.spells");
        internal static readonly string KeyKbQuick = Utilities.GetKey("ab.kb.quick");

        internal static readonly string KeyBtnEnableTitle = Utilities.GetKey("ab.btn.enable.title");
        internal static readonly string KeyHeader = Utilities.GetKey("ab.header");
        internal static readonly string KeyKbAbilityTitle = Utilities.GetKey("ab.kb.ablility.title");
        internal static readonly string KeyKbSpellsTitle = Utilities.GetKey("ab.kb.spells.title");
        internal static readonly string KeyKbQuickTitle = Utilities.GetKey("ab.kb.quick.title");

        internal static void AddModMenuSettings(SettingsBuilder sb)
        {
            Log.Log("Initializing action bar settings", Globals.LogPrefix);

            sb.AddSubHeader(Utilities.GetString(KeyHeader))
                .AddToggle(
                    Toggle
                        .New(KeyBtnEnable, false, Utilities.KeyBtnEnableString).WithLongDescription(Utilities.GetString(KeyBtnEnableTitle)))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbAbility, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbAbilityTitle)), () => OnPress(SubmenuType.Ability))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbSpells, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbSpellsTitle)), () => OnPress(SubmenuType.Spells))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbQuick, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbQuickTitle)), () => OnPress(SubmenuType.Quick));
        }

        private static void OnPress(SubmenuType type)
        {
            var enabled = ModMenu.ModMenu.GetSettingValue<bool>(KeyBtnEnable);
            if (enabled && Utilities.TypesForActionBar.Contains(Main.Reporter.CurrentFullScreenUIType))
                ToggleActionBarSubmenu(type);
        }

        private static void ToggleActionBarSubmenu(SubmenuType submenuType)
        {
            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ActionBar: Toggle sub menu {submenuType}", Globals.LogPrefix);

            string path = null;
            switch (submenuType)
            {
                case SubmenuType.Ability:
                    path = "NestedCanvas1/ActionBarPcView/ActionBarBottomView/ActionBarMainContainer/AbilitiesGroupsContainer/Groups/AbilityGroupView";
                    break;
                case SubmenuType.Spells:
                    path = "NestedCanvas1/ActionBarPcView/ActionBarBottomView/ActionBarMainContainer/AbilitiesGroupsContainer/Groups/SpellGroupView";
                    break;
                case SubmenuType.Quick:
                    path = "NestedCanvas1/ActionBarPcView/ActionBarBottomView/ActionBarMainContainer/AbilitiesGroupsContainer/Groups/ItemGroupView";
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

            ActionBar.VisibilityStates[__instance.name] = ___VisibleState;
        }
    }
}
