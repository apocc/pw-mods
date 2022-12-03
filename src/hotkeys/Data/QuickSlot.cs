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
using ModMenu.Settings;

using static Kingmaker.UI.KeyboardAccess;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data
{
    internal static class QuickSlot
    {
        internal static readonly string KeyBtnEnable = Utilities.GetKey("qs.btn.enable");
        internal static readonly string KeyKb00 = Utilities.GetKey("qs.kb.00");
        internal static readonly string KeyKb01 = Utilities.GetKey("qs.kb.01");
        internal static readonly string KeyKb02 = Utilities.GetKey("qs.kb.02");
        internal static readonly string KeyKb03 = Utilities.GetKey("qs.kb.03");
        internal static readonly string KeyKb04 = Utilities.GetKey("qs.kb.04");
        internal static readonly string KeyToggleForAll = Utilities.GetKey("qs.toggle.for.all");
        internal static readonly string KeyToggleQuickSlotPlacement = Utilities.GetKey("qs.toggle.quick.slot.placement");

        internal static readonly string KeyBtnEnableTitle = Utilities.GetKey("qs.btn.enable.title");
        internal static readonly string KeyHeader = Utilities.GetKey("qs.header");
        internal static readonly string KeyKb00Title = Utilities.GetKey("qs.kb.00.title");
        internal static readonly string KeyKb01Title = Utilities.GetKey("qs.kb.01.title");
        internal static readonly string KeyKb02Title = Utilities.GetKey("qs.kb.02.title");
        internal static readonly string KeyKb03Title = Utilities.GetKey("qs.kb.03.title");
        internal static readonly string KeyKb04Title = Utilities.GetKey("qs.kb.04.title");
        internal static readonly string KeyToggleForAllTitle = Utilities.GetKey("qs.toggle.for.all.title");
        internal static readonly string KeyToggleQuickSlotPlacementDesc = Utilities.GetKey("qs.toggle.quick.slot.placement.desc");
        internal static readonly string KeyToggleQuickSlotPlacementTitle = Utilities.GetKey("qs.toggle.quick.slot.placement.title");

        internal static void AddModMenuSettings(SettingsBuilder sb)
        {
            Log.Log("Initializing quick slot settings", Globals.LogPrefix);

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
                        .New(KeyKb04, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb04Title)), () => OnPress(4))
                .AddToggle(
                    Toggle
                        .New(KeyToggleForAll, false, Utilities.GetString(KeyToggleForAllTitle)))
                .AddToggle(
                    Toggle
                        .New(KeyToggleQuickSlotPlacement, false, Utilities.GetString(KeyToggleQuickSlotPlacementTitle))
                            .WithLongDescription(Utilities.GetString(KeyToggleQuickSlotPlacementDesc)));
        }

        private static void OnPress(int slotIndex)
        {
            var enabled = ModMenu.ModMenu.GetSettingValue<bool>(KeyBtnEnable);
            if (enabled && Utilities.TypesForQs.Contains(Main.Reporter.CurrentFullScreenUIType))
                UseQuickSlot(slotIndex);
        }

        private static void TryActivate(int slotIndex, UnitEntityData unit)
        {
            UnitBody body = unit.Body;

            if (Main.Settings.EnableVerboseLogging)
            {
                Log.Log("Character: " + unit.CharacterName, Globals.LogPrefix);
                Log.Log($"Slots: {string.Join(", ", body.QuickSlots.Select(qs => qs.HasItem))}", Globals.LogPrefix);
            }

            UsableSlot[] usableSlots = ModMenu.ModMenu.GetSettingValue<bool>(KeyToggleQuickSlotPlacement)
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
            var forAll = ModMenu.ModMenu.GetSettingValue<bool>(KeyToggleForAll);

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"Use slot: {index} for all: {forAll}", Globals.LogPrefix);

            var scc = Game.Instance.SelectionCharacter;

            if (forAll)
            {
                foreach (UnitEntityData unitEntityData in scc.SelectedUnits)
                {
                    TryActivate(index, unitEntityData);
                }

                return;
            }

            if (scc.SelectedUnits.Count != 1)
                return;

            TryActivate(index, scc.SelectedUnits[0]);
        }
    }
}
