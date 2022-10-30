// Copyright (c) apocc.
// Licensed under MIT License.

using System.Xml.Serialization;

namespace Apocc.Pw.Hotkeys.Data
{
    public sealed class ModLockEntry
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string Value { get; set; }

        public ModLockEntry() { }

        public ModLockEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    internal static class SettingsLocaleData
    {
        internal static readonly ModLockEntry[] EntriesEnGB = new ModLockEntry[]
        {
            new ModLockEntry(AiStealth.KeyHeader, "Ai and Stealth"),
            new ModLockEntry(AiStealth.KeyBtnEnableDesc, "Enable Ai and Stealth hotkeys"),
            new ModLockEntry(AiStealth.KeyKbAiDesc, "AI hotkey"),
            new ModLockEntry(AiStealth.KeyKbStealthDesc, "Stealth hotkey"),

            new ModLockEntry(WeaponSets.KeyHeader, "Weapon Sets"),
            new ModLockEntry(WeaponSets.KeyBtnEnableDesc, "Enable Weapon Set hotkeys"),
            new ModLockEntry(WeaponSets.KeyKb00Desc, "Weapon set 1"),
            new ModLockEntry(WeaponSets.KeyKb01Desc, "Weapon set 2"),
            new ModLockEntry(WeaponSets.KeyKb02Desc, "Weapon set 3"),
            new ModLockEntry(WeaponSets.KeyKb03Desc, "Weapon set 4"),
            new ModLockEntry(WeaponSets.KeyKbCycleDesc, "Weapon set cycle"),
            new ModLockEntry(WeaponSets.KeyToggleForAllDesc, "For all selected chararacters"),
            new ModLockEntry(WeaponSets.KeyToggleInventoryDesc, "Enable weapon set hotkeys when in inventory"),
            new ModLockEntry(WeaponSets.KeyToggleInventoryForAllDesc, "For all selected chararacters when in inventory"),

            new ModLockEntry(ActionBar.KeyHeader, "Action Bar"),
            new ModLockEntry(ActionBar.KeyBtnEnableDesc, "Enable Action Bar hotkeys"),
            new ModLockEntry(ActionBar.KeyKbAbilityDesc, "Abilities"),
            new ModLockEntry(ActionBar.KeyKbSpellsDesc, "Spells"),
            new ModLockEntry(ActionBar.KeyKbQuickDesc, "Quick Slots"),

            new ModLockEntry(QuickSlot.KeyHeader, "Quick Slot"),
            new ModLockEntry(QuickSlot.KeyBtnEnableDesc, "Enable Quick Slot hotkeys"),
            new ModLockEntry(QuickSlot.KeyKb00Desc, "Slot 1"),
            new ModLockEntry(QuickSlot.KeyKb01Desc, "Slot 2"),
            new ModLockEntry(QuickSlot.KeyKb02Desc, "Slot 3"),
            new ModLockEntry(QuickSlot.KeyKb03Desc, "Slot 4"),
            new ModLockEntry(QuickSlot.KeyKb04Desc, "Slot 5"),
            new ModLockEntry(QuickSlot.KeyToggleForAllDesc, "For all selected chararacters"),
            new ModLockEntry(QuickSlot.KeyToggleQuickSlotPlacementDesc, "Use quick slot placement"),

            new ModLockEntry(CharacterSelect.KeyHeader, "Character Select"),
            new ModLockEntry(CharacterSelect.KeyBtnEnableDesc, "Enable Character Select hotkeys"),
            new ModLockEntry(CharacterSelect.KeyKbNextDesc, "Next character"),
            new ModLockEntry(CharacterSelect.KeyKbPrevDesc, "Previous character"),

            new ModLockEntry(Formation.KeyHeader, "Formations"),
            new ModLockEntry(Formation.KeyBtnEnableDesc, "Enable Formations hotkeys"),
            new ModLockEntry(Formation.KeyKb00Desc, "Formation: auto"),
            new ModLockEntry(Formation.KeyKb01Desc, "Formation: triangle"),
            new ModLockEntry(Formation.KeyKb02Desc, "Formation: star"),
            new ModLockEntry(Formation.KeyKb03Desc, "Formation: waves"),
            new ModLockEntry(Formation.KeyKb04Desc, "Formation: circle"),
            new ModLockEntry(Formation.KeyKb05Desc, "Formation: hammer"),
            new ModLockEntry(Formation.KeyKbCycleDesc, "Formation cycle"),
        };
    }
}
