// Copyright (c) apocc.
// Licensed under MIT License.

using System.Linq;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data.UsableItems
{
    internal static class Runner
    {
        private static void TryActivate(int slotIndex, UnitEntityData unit)
        {
            UnitBody body = unit.Body;

            if (Main.Settings.EnableVerboseLogging)
            {
                Log.Log("Character: " + unit.CharacterName, Globals.LogPrefix);
                Log.Log($"Slots: {string.Join(", ", body.QuickSlots.Select(qs => qs.HasItem))}", Globals.LogPrefix);
            }

            UsableSlot[] usableSlots = Main.Settings.UsitUseActionBarPlacement
                ? body.QuickSlots.Where(qs => qs.HasItem).ToArray()
                : body.QuickSlots;

            if (usableSlots == null || usableSlots.Length <= slotIndex)
                return;

            UsableSlot slot = usableSlots[slotIndex];
            if (!slot.HasItem || slot.Item.Ability == null)
                return;

            Ability ability = slot.Item.Ability;

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"Ability: {ability.Blueprint.Name}", Globals.LogPrefix);

            if (ability.Data.TargetAnchor != AbilityTargetAnchor.Owner)
            {
                if (Main.Settings.EnableVerboseLogging)
                    Log.Log("!Owner", Globals.LogPrefix);

                Game.Instance.SelectedAbilityHandler.SetAbility(ability.Data);
            }
            else
            {
                if (Main.Settings.EnableVerboseLogging)
                    Log.Log("Owner", Globals.LogPrefix);

                unit.Commands.Run(new UnitUseAbility(ability.Data, unit));
            }
        }

        private static void UseQuickSlot(int index)
        {
            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"Use slot: {index} for all: {Main.Settings.UsitEnableAllSelectedCharacters}", Globals.LogPrefix);

            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            if (Main.Settings.UsitEnableAllSelectedCharacters)
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
            if (Main.Settings.UsitSlot00.Up())
                UseQuickSlot(0);
            if (Main.Settings.UsitSlot01.Up())
                UseQuickSlot(1);
            if (Main.Settings.UsitSlot02.Up())
                UseQuickSlot(2);
            if (Main.Settings.UsitSlot03.Up())
                UseQuickSlot(3);
            if (Main.Settings.UsitSlot04.Up())
                UseQuickSlot(4);
        }
    }
}
