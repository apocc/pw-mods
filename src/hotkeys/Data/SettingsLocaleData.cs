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
            new ModLocEntry(AiStealth.KeyBtnEnableTitle, "Enable Ai and Stealth hotkeys"),
            new ModLocEntry(AiStealth.KeyKbAiTitle, "AI hotkey"),
            new ModLocEntry(AiStealth.KeyKbStealthTitle, "Stealth hotkey"),

            new ModLocEntry(WeaponSets.KeyHeader, "Weapon Sets"),
            new ModLocEntry(WeaponSets.KeyBtnEnableTitle, "Enable Weapon Set hotkeys"),
            new ModLocEntry(WeaponSets.KeyKb00Title, "Weapon set 1"),
            new ModLocEntry(WeaponSets.KeyKb01Title, "Weapon set 2"),
            new ModLocEntry(WeaponSets.KeyKb02Title, "Weapon set 3"),
            new ModLocEntry(WeaponSets.KeyKb03Title, "Weapon set 4"),
            new ModLocEntry(WeaponSets.KeyKbCycleTitle, "Weapon set cycle"),
            new ModLocEntry(WeaponSets.KeyToggleForAllTitle, "For all selected chararacters"),
            new ModLocEntry(WeaponSets.KeyToggleInventoryDesc, "If enabled the weapon set functions will be applied when on the inventory screen."),
            new ModLocEntry(WeaponSets.KeyToggleInventoryTitle, "Enable weapon set hotkeys when in inventory"),
            new ModLocEntry(WeaponSets.KeyToggleInventoryForAllDesc, "If enabled the weapon set functions will be applied to all selected characters 'on the field' when on the inventory screen."),
            new ModLocEntry(WeaponSets.KeyToggleInventoryForAllTitle, "For all selected chararacters when in inventory"),

            new ModLocEntry(ActionBar.KeyHeader, "Action Bar"),
            new ModLocEntry(ActionBar.KeyBtnEnableTitle, "Enable Action Bar hotkeys"),
            new ModLocEntry(ActionBar.KeyKbAbilityTitle, "Abilities"),
            new ModLocEntry(ActionBar.KeyKbSpellsTitle, "Spells"),
            new ModLocEntry(ActionBar.KeyKbQuickTitle, "Quick Slots"),

            new ModLocEntry(QuickSlot.KeyHeader, "Quick Slot"),
            new ModLocEntry(QuickSlot.KeyBtnEnableTitle, "Enable Quick Slot hotkeys"),
            new ModLocEntry(QuickSlot.KeyKb00Title, "Slot 1"),
            new ModLocEntry(QuickSlot.KeyKb01Title, "Slot 2"),
            new ModLocEntry(QuickSlot.KeyKb02Title, "Slot 3"),
            new ModLocEntry(QuickSlot.KeyKb03Title, "Slot 4"),
            new ModLocEntry(QuickSlot.KeyKb04Title, "Slot 5"),
            new ModLocEntry(QuickSlot.KeyToggleForAllTitle, "For all selected chararacters"),
            new ModLocEntry(QuickSlot.KeyToggleQuickSlotPlacementDesc, 
                "Items displayed on the Quick Slot bar are alwasy left aligned regardless of their position in the inventory panel. With this option enabled the placement of the Quick Slot Bar is used otherwise that of the inventory panel."),
            new ModLocEntry(QuickSlot.KeyToggleQuickSlotPlacementTitle, "Use quick slot placement"),

            new ModLocEntry(CharacterSelect.KeyHeader, "Character Select"),
            new ModLocEntry(CharacterSelect.KeyBtnEnableTitle, "Enable Character Select hotkeys"),
            new ModLocEntry(CharacterSelect.KeyKbNextTitle, "Next character"),
            new ModLocEntry(CharacterSelect.KeyKbPrevTitle, "Previous character"),

            new ModLocEntry(Formation.KeyHeader, "Formations"),
            new ModLocEntry(Formation.KeyBtnEnableTitle, "Enable Formations hotkeys"),
            new ModLocEntry(Formation.KeyKb00Title, "Formation: auto"),
            new ModLocEntry(Formation.KeyKb01Title, "Formation: triangle"),
            new ModLocEntry(Formation.KeyKb02Title, "Formation: star"),
            new ModLocEntry(Formation.KeyKb03Title, "Formation: waves"),
            new ModLocEntry(Formation.KeyKb04Title, "Formation: circle"),
            new ModLocEntry(Formation.KeyKb05Title, "Formation: hammer"),
            new ModLocEntry(Formation.KeyKbCycleTitle, "Formation cycle"),
        };
    }
}
