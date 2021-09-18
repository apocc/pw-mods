// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Inventory;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data.WeaponSets
{
    internal static class Runner
    {
        private static int CalcNewIndex(UnitEntityData unit, int index)
           => index == -1 ? (unit.Body.CurrentHandEquipmentSetIndex + 1) % 4 : index;

        private static void IncrementSetIndex(int index = -1)
        {
            GameModeType mode = Game.Instance.CurrentMode;
            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            var isInventory = mode == GameModeType.FullScreenUi;
            var forceForAll = Main.Settings.TwsEnableInInventory && Main.Settings.TwsForceChangeForAllWhenInInventory;

            if (isInventory && !Main.Settings.TwsEnableInInventory)
                return;

            string updatedUnitId = null;
            if (isInventory && Main.Settings.TwsEnableInInventory)
                updatedUnitId = UpdateInventory(index);

            if (Main.Settings.EnableAllSelectedCharacters && (!isInventory || isInventory && forceForAll))
            {
                foreach (UnitEntityData unit in sm.SelectedUnits)
                {
                    if (updatedUnitId == unit.UniqueId) continue;

                    unit.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(unit, index);
                }
            }

            if (!Main.Settings.EnableAllSelectedCharacters && sm.SelectedUnits.Count == 1)
            {
                UnitEntityData unit = sm.SelectedUnits[0];

                if (updatedUnitId != unit.UniqueId)
                    unit.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(unit, index);
            }
        }

        private static string UpdateInventory(int index = -1)
        {
            UnitEntityData current = UIUtility.GetCurrentCharacter();

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"Raising inventory changed event for {current.CharacterName}", Globals.LogPrefix);

            current.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(current, index);
            EventBus.RaiseEvent<IInventoryHandler>((h) => h.Refresh());

            return current.UniqueId;
        }

        internal static void Run()
        {
            if (Input.GetKeyUp(Main.Settings.TwsKeyCode00))
                IncrementSetIndex(0);
            if (Input.GetKeyUp(Main.Settings.TwsKeyCode01))
                IncrementSetIndex(1);
            if (Input.GetKeyUp(Main.Settings.TwsKeyCode02))
                IncrementSetIndex(2);
            if (Input.GetKeyUp(Main.Settings.TwsKeyCode03))
                IncrementSetIndex(3);

            if (Input.GetKeyUp(Main.Settings.TwsKeyCodeToggle))
                IncrementSetIndex();
        }
    }
}
