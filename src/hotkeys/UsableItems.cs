using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands;
using System.Linq;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public static class UsableItems
    {
        private static void TryActivate(int slotIndex, UnitEntityData unit)
        {
            UnitBody body = unit.Body;

            if (Settings.Instance.EnableVerboseLogging)
            {
                Log.Log("Character: " + unit.CharacterName, Globals.LogPrefix);
                Log.Log($"Slots: {string.Join(", ", body.QuickSlots.Select(qs => qs.HasItem))}", Globals.LogPrefix);
            }

            UsableSlot[] usableSlots = Settings.Instance.UsitUseActionBarPlacement
                ? body.QuickSlots.Where(qs => qs.HasItem).ToArray()
                : body.QuickSlots;

            if (usableSlots == null || usableSlots.Length <= slotIndex)
                return;

            UsableSlot slot = usableSlots[slotIndex];
            if (slot.Item.Ability == null)
                return;

            Ability ability = slot.Item.Ability;

            if (Settings.Instance.EnableVerboseLogging)
                Log.Log($"Ability: {ability.Blueprint.Name}", Globals.LogPrefix);

            if (ability.Data.TargetAnchor != AbilityTargetAnchor.Owner)
            {
                if (Settings.Instance.EnableVerboseLogging)
                    Log.Log("!Owner", Globals.LogPrefix);

                Game.Instance.SelectedAbilityHandler.SetAbility(ability.Data);
            }
            else
            {
                if (Settings.Instance.EnableVerboseLogging)
                    Log.Log("Owner", Globals.LogPrefix);

                unit.Commands.Run(new UnitUseAbility(ability.Data, unit));
            }
        }

        private static void UseQuickSlot(int index)
        {
            if (Settings.Instance.EnableVerboseLogging)
                Log.Log($"Use slot: {index} for all: {Settings.Instance.UsitEnableAllSelectedCharacters}", Globals.LogPrefix);

            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            if (Settings.Instance.UsitEnableAllSelectedCharacters)
            {
                foreach (UnitEntityData unitEntityData in sm.SelectedUnits)
                {
                    TryActivate(index, unitEntityData);
                }

                return;
            }

            if (sm.SelectedUnits.Count != 1)
                return;

            TryActivate(index, sm.SelectedUnits[0]);
        }

        public static void Run()
        {
            if (Input.GetKeyUp(Settings.Instance.UsitKeyCodeSlot00))
                UseQuickSlot(0);
            if (Input.GetKeyUp(Settings.Instance.UsitKeyCodeSlot01))
                UseQuickSlot(1);
            if (Input.GetKeyUp(Settings.Instance.UsitKeyCodeSlot02))
                UseQuickSlot(2);
            if (Input.GetKeyUp(Settings.Instance.UsitKeyCodeSlot03))
                UseQuickSlot(3);
            if (Input.GetKeyUp(Settings.Instance.UsitKeyCodeSlot04))
                UseQuickSlot(4);
        }
    }
}
