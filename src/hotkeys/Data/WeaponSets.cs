// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Inventory;
using Kingmaker;
using ModMenu.Settings;

using GameModesGroup = Kingmaker.UI.KeyboardAccess.GameModesGroup;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data
{
    public sealed class WeaponSets
    {
        internal static readonly string KeyBtnEnable = Utilities.GetKey("ws.btn.enable");
        internal static readonly string KeyKb00 = Utilities.GetKey("ws.kb.00");
        internal static readonly string KeyKb01 = Utilities.GetKey("ws.kb.01");
        internal static readonly string KeyKb02 = Utilities.GetKey("ws.kb.02");
        internal static readonly string KeyKb03 = Utilities.GetKey("ws.kb.03");
        internal static readonly string KeyKbCycle = Utilities.GetKey("ws.kb.cycle");
        internal static readonly string KeyToggleForAll = Utilities.GetKey("ws.toggle.for.all");
        internal static readonly string KeyToggleInventory = Utilities.GetKey("ws.toggle.inventory");
        internal static readonly string KeyToggleInventoryForAll = Utilities.GetKey("ws.toggle.inventory.for.all");

        internal static readonly string KeyBtnEnableTitle = Utilities.GetKey("ws.btn.enable.title");
        internal static readonly string KeyHeader = Utilities.GetKey("ws.header");
        internal static readonly string KeyKb00Title = Utilities.GetKey("ws.kb.00.title");
        internal static readonly string KeyKb01Title = Utilities.GetKey("ws.kb.01.title");
        internal static readonly string KeyKb02Title = Utilities.GetKey("ws.kb.02.title");
        internal static readonly string KeyKb03Title = Utilities.GetKey("ws.kb.03.title");
        internal static readonly string KeyKbCycleTitle = Utilities.GetKey("ws.kb.cycle.title");
        internal static readonly string KeyToggleForAllTitle = Utilities.GetKey("ws.toggle.for.all.title");
        internal static readonly string KeyToggleInventoryDesc = Utilities.GetKey("ws.toggle.inventory.desc");
        internal static readonly string KeyToggleInventoryTitle = Utilities.GetKey("ws.toggle.inventory.title");
        internal static readonly string KeyToggleInventoryForAllDesc = Utilities.GetKey("ws.toggle.inventory.for.all.desc");
        internal static readonly string KeyToggleInventoryForAllTitle = Utilities.GetKey("ws.toggle.inventory.for.all.title");

        private static int CalcNewIndex(UnitEntityData unit, int index)
           => index == -1 ? (unit.Body.CurrentHandEquipmentSetIndex + 1) % 4 : index;

        private static void IncrementSetIndex(int index)
        {
            var mode = Game.Instance.CurrentMode;
            var isInInventory = mode == GameModeType.FullScreenUi;
            var forAll = ModMenu.ModMenu.GetSettingValue<bool>(KeyToggleForAll);
            var enableInInventory = ModMenu.ModMenu.GetSettingValue<bool>(KeyToggleInventory);
            var enableInInventoryForAll = ModMenu.ModMenu.GetSettingValue<bool>(KeyToggleInventoryForAll);

            if (isInInventory && !enableInInventory)
                return;

            var scc = Game.Instance.SelectionCharacter;
            string updatedUnitId = null;

            if (isInInventory && enableInInventory)
                updatedUnitId = UpdateInventory(index);

            if (forAll && (!isInInventory || isInInventory && enableInInventoryForAll))
            {
                foreach (var u in scc.SelectedUnits)
                {
                    if (updatedUnitId == u.UniqueId) continue;

                    u.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(u, index);
                }

                return;
            }

            if (!forAll && scc.SelectedUnits.Count == 1)
            {
                var unit = scc.SelectedUnits[0];

                if (updatedUnitId != unit.UniqueId)
                    unit.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(unit, index);
            }
        }

        private static void OnPress(int index = -1)
        {
            var enabled = ModMenu.ModMenu.GetSettingValue<bool>(KeyBtnEnable);
            if (enabled && Utilities.TypesForTws.Contains(Main.Reporter.CurrentFullScreenUIType))
                IncrementSetIndex(index);
        }

        private static string UpdateInventory(int index)
        {
            var current = Game.Instance.SelectionCharacter.CurrentSelectedCharacter;

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"Raising inventory changed event for {current.CharacterName}", Globals.LogPrefix);

            current.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(current, index);
            EventBus.RaiseEvent<IInventoryHandler>((h) => h.Refresh());

            return current.UniqueId;
        }

        internal static void AddModMenuSettings(SettingsBuilder sb)
        {
            Log.Log("Initializing weapon set settings", Globals.LogPrefix);

            sb.AddSubHeader(Utilities.GetString(KeyHeader))
                .AddToggle(
                    Toggle.New(KeyBtnEnable, false, Utilities.KeyBtnEnableString).WithLongDescription(Utilities.GetString(KeyBtnEnableTitle)))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb00, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb00Title)), () => OnPress(0))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb01, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb01Title)), () => OnPress(1))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb02, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb02Title)), () => OnPress(2))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb03, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb03Title)), () => OnPress(3))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbCycle, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbCycleTitle)), () => OnPress())
                 .AddToggle(
                    Toggle
                        .New(KeyToggleForAll, false, Utilities.GetString(KeyToggleForAllTitle)))
                 .AddToggle(
                    Toggle
                        .New(KeyToggleInventory, false, Utilities.GetString(KeyToggleInventoryTitle))
                            .WithLongDescription(Utilities.GetString(KeyToggleInventoryDesc)))
                 .AddToggle(
                    Toggle
                        .New(KeyToggleInventoryForAll, false, Utilities.GetString(KeyToggleInventoryForAllTitle))
                            .WithLongDescription(Utilities.GetString(KeyToggleInventoryForAllDesc)));
        }
    }
}
