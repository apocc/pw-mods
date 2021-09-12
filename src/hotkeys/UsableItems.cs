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
        private static void TryActivate(int slotIndex, UnitEntityData unit, Settings s)
        {
            UnitBody body = unit.Body;

            if (s.EnableVerboseLogging)
            {
                Log.Log("Character: " + unit.CharacterName, Globals.LogPrefix);
                Log.Log($"Slots: {string.Join(", ", body.QuickSlots.Select(qs => qs.HasItem))}", Globals.LogPrefix);
            }

            UsableSlot[] usableSlots = s.UsitUseActionBarPlacement
                ? body.QuickSlots.Where(qs => qs.HasItem).ToArray()
                : body.QuickSlots;

            if (usableSlots == null || usableSlots.Length <= slotIndex)
                return;

            UsableSlot slot = usableSlots[slotIndex];
            if (slot.Item.Ability == null)
                return;

            Ability ability = slot.Item.Ability;

            if (s.EnableVerboseLogging)
                Log.Log($"Ability: {ability.Blueprint.Name}", Globals.LogPrefix);

            if (ability.Data.TargetAnchor != AbilityTargetAnchor.Owner)
            {
                if (s.EnableVerboseLogging)
                    Log.Log("!Owner", Globals.LogPrefix);

                Game.Instance.SelectedAbilityHandler.SetAbility(ability.Data);
            }
            else
            {
                if (s.EnableVerboseLogging)
                    Log.Log("Owner", Globals.LogPrefix);

                unit.Commands.Run(new UnitUseAbility(ability.Data, unit));
            }
        }

        private static void UseQuickSlot(int index, Settings s)
        {
            if (s.EnableVerboseLogging)
                Log.Log($"Use slot: {index} for all: {s.UsitEnableAllSelectedCharacters}", Globals.LogPrefix);

            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            if (s.UsitEnableAllSelectedCharacters)
            {
                foreach (UnitEntityData unitEntityData in sm.SelectedUnits)
                {
                    TryActivate(index, unitEntityData, s);
                }

                return;
            }

            if (sm.SelectedUnits.Count != 1)
                return;

            TryActivate(index, sm.SelectedUnits[0], s);
        }

        public static void Run(Settings s)
        {
            if (Input.GetKeyUp(s.UsitKeyCodeSlot00))
                UseQuickSlot(0, s);
            if (Input.GetKeyUp(s.UsitKeyCodeSlot01))
                UseQuickSlot(1, s);
            if (Input.GetKeyUp(s.UsitKeyCodeSlot02))
                UseQuickSlot(2, s);
            if (Input.GetKeyUp(s.UsitKeyCodeSlot03))
                UseQuickSlot(3, s);
            if (Input.GetKeyUp(s.UsitKeyCodeSlot04))
                UseQuickSlot(4, s);
        }
    }
}
