using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._VM.ServiceWindows.Inventory;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public static class ToggleWeaponSet
    {
        private static int CalcNewIndex(UnitEntityData unit, int index)
            => index == -1 ? (unit.Body.CurrentHandEquipmentSetIndex + 1) % 4 : index;

        private static void IncrementSetIndex(Settings s, int index = -1)
        {
            GameModeType mode = Game.Instance.CurrentMode;
            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            var isInventory = mode == GameModeType.FullScreenUi;
            var forceForAll = s.TwsEnableInInventory && s.TwsForceChangeForAllWhenInInventory;

            if (isInventory && !s.TwsEnableInInventory)
                return;

            string updatedUnitId = null;
            if (isInventory && s.TwsEnableInInventory)
                updatedUnitId = UpdateInventory(s, index);

            if (s.EnableAllSelectedCharacters && (!isInventory || isInventory && forceForAll))
            {
                foreach (UnitEntityData unit in sm.SelectedUnits)
                {
                    if (updatedUnitId == unit.UniqueId) continue;

                    unit.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(unit, index);
                }
            }

            if (!s.EnableAllSelectedCharacters && sm.SelectedUnits.Count == 1)
            {
                UnitEntityData unit = sm.SelectedUnits[0];

                if (updatedUnitId != unit.UniqueId)
                    unit.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(unit, index);
            }
        }

        private static string UpdateInventory(Settings s, int index = -1)
        {
            UnitEntityData current = UIUtility.GetCurrentCharacter();

            if (s.EnableVerboseLogging)
                Log.Log($"Raising inventory changed event for {current.CharacterName}", Globals.LogPrefix);

            current.Body.CurrentHandEquipmentSetIndex = CalcNewIndex(current, index);
            EventBus.RaiseEvent<IInventoryHandler>((h) => h.Refresh());

            return current.UniqueId;
        }

        public static void Run(Settings s)
        {
            if (Input.GetKeyUp(s.TwsKeyCode00))
                IncrementSetIndex(s, 0);
            if (Input.GetKeyUp(s.TwsKeyCode01))
                IncrementSetIndex(s, 1);
            if (Input.GetKeyUp(s.TwsKeyCode02))
                IncrementSetIndex(s, 2);
            if (Input.GetKeyUp(s.TwsKeyCode03))
                IncrementSetIndex(s, 3);

            if (Input.GetKeyUp(s.TwsKeyCodeToggle))
                IncrementSetIndex(s);
        }
    }
}
