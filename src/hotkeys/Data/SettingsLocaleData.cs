// Copyright (c) apocc.
// Licensed under MIT License.

using System.Xml.Serialization;

namespace Apocc.Pw.Hotkeys.Data
{
    public sealed class ModLocEntry
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText]
        public string Value { get; set; }

        public ModLocEntry() { }

        public ModLocEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    internal static class SettingsLocaleData
    {
        internal static readonly ModLocEntry[] EntriesEnGB = new ModLocEntry[]
        {
            new ModLocEntry(Utilities.KeyBtnEnable, "Enable"),

            new ModLocEntry(AiStealth.KeyHeader, "Ai and Stealth"),
            new ModLocEntry(AiStealth.KeyBtnEnableDesc, "Enable Ai and Stealth hotkeys"),
            new ModLocEntry(AiStealth.KeyKbAiDesc, "AI hotkey"),
            new ModLocEntry(AiStealth.KeyKbStealthDesc, "Stealth hotkey"),

            new ModLocEntry(WeaponSets.KeyHeader, "Weapon Sets"),
            new ModLocEntry(WeaponSets.KeyBtnEnableDesc, "Enable Weapon Set hotkeys"),
            new ModLocEntry(WeaponSets.KeyKb00Desc, "Weapon set 1"),
            new ModLocEntry(WeaponSets.KeyKb01Desc, "Weapon set 2"),
            new ModLocEntry(WeaponSets.KeyKb02Desc, "Weapon set 3"),
            new ModLocEntry(WeaponSets.KeyKb03Desc, "Weapon set 4"),
            new ModLocEntry(WeaponSets.KeyKbCycleDesc, "Weapon set cycle"),
            new ModLocEntry(WeaponSets.KeyToggleForAllDesc, "For all selected chararacters"),
            new ModLocEntry(WeaponSets.KeyToggleInventoryDesc, "Enable weapon set hotkeys when in inventory"),
            new ModLocEntry(WeaponSets.KeyToggleInventoryForAllDesc, "For all selected chararacters when in inventory"),

            new ModLocEntry(ActionBar.KeyHeader, "Action Bar"),
            new ModLocEntry(ActionBar.KeyBtnEnableDesc, "Enable Action Bar hotkeys"),
            new ModLocEntry(ActionBar.KeyKbAbilityDesc, "Abilities"),
            new ModLocEntry(ActionBar.KeyKbSpellsDesc, "Spells"),
            new ModLocEntry(ActionBar.KeyKbQuickDesc, "Quick Slots"),

            new ModLocEntry(QuickSlot.KeyHeader, "Quick Slot"),
            new ModLocEntry(QuickSlot.KeyBtnEnableDesc, "Enable Quick Slot hotkeys"),
            new ModLocEntry(QuickSlot.KeyKb00Desc, "Slot 1"),
            new ModLocEntry(QuickSlot.KeyKb01Desc, "Slot 2"),
            new ModLocEntry(QuickSlot.KeyKb02Desc, "Slot 3"),
            new ModLocEntry(QuickSlot.KeyKb03Desc, "Slot 4"),
            new ModLocEntry(QuickSlot.KeyKb04Desc, "Slot 5"),
            new ModLocEntry(QuickSlot.KeyToggleForAllDesc, "For all selected chararacters"),
            new ModLocEntry(QuickSlot.KeyToggleQuickSlotPlacementDesc, "Use quick slot placement"),

            new ModLocEntry(CharacterSelect.KeyHeader, "Character Select"),
            new ModLocEntry(CharacterSelect.KeyBtnEnableDesc, "Enable Character Select hotkeys"),
            new ModLocEntry(CharacterSelect.KeyKbNextDesc, "Next character"),
            new ModLocEntry(CharacterSelect.KeyKbPrevDesc, "Previous character"),

            new ModLocEntry(Formation.KeyHeader, "Formations"),
            new ModLocEntry(Formation.KeyBtnEnableDesc, "Enable Formations hotkeys"),
            new ModLocEntry(Formation.KeyKb00Desc, "Formation: auto"),
            new ModLocEntry(Formation.KeyKb01Desc, "Formation: triangle"),
            new ModLocEntry(Formation.KeyKb02Desc, "Formation: star"),
            new ModLocEntry(Formation.KeyKb03Desc, "Formation: waves"),
            new ModLocEntry(Formation.KeyKb04Desc, "Formation: circle"),
            new ModLocEntry(Formation.KeyKb05Desc, "Formation: hammer"),
            new ModLocEntry(Formation.KeyKbCycleDesc, "Formation cycle"),
        };
    }
}
