// Copyright (c) apocc.
// Licensed under MIT License.

namespace Apocc.Pw.Hotkeys.Data
{
    public sealed class SettingsLocaleData
    {
        public static SettingsLocaleData Default = new SettingsLocaleData
        {
            LabelGenNoValidation = "There is no validation. Make sure other installed mods don't use the same key",
            LabelGenVerboseLogging = "Enable verbose logging",
            LabelTaisEnable = "Enable Toggle AI and Stealth",
            LabelTaisKeyAi = "Key to toggle AI",
            LabelTaisKeyStealth = "Key to toggle Stealth",
            LabelCsEnable = "Enable character selection",
            LabelCsNext = "Key to select next character",
            LabelCsPrev = "Key to select previous character",
            LabelFormEnable = "Enable formation hotkeys",
            LabelFormKey00 = "Formation: auto",
            LabelFormKey01 = "Formation: triangle",
            LabelFormKey02 = "Formation: star",
            LabelFormKey03 = "Formation: waves",
            LabelFormKey04 = "Formation: circle",
            LabelFormKey05 = "Formation: hammer",
            LabelUsitEnable = "Enable Quick Slot item hotkeys",
            LabelUsitEnableForAll = "For all selected chararacters",
            LabelUsitKey00 = "Key Slot 1",
            LabelUsitKey01 = "Key Slot 2",
            LabelUsitKey02 = "Key Slot 3",
            LabelUsitKey03 = "Key Slot 4",
            LabelUsitKey04 = "Key Slot 5",
            LabelFormKeyCircle = "Circle through formations",
            LabelUsitUseActonBarPlacement = "Use quick slot placement",
            LabelTwsCycle = "Key weapon cycle",
            LabelTwsEnable = "Enable Toggle Weapon Sets",
            LabelTwsEnableForAll = "For all selected chararacters",
            LabelTwsEnableForceChangeInFullScreen = "Force weapon set change for all selected characters when in full screen ui",
            LabelTwsEnableFullScreen = "Enable in full screen ui for selected character",
            LabelTwsKey00 = "Key for weapon set 1",
            LabelTwsKey01 = "Key for weapon set 2",
            LabelTwsKey02 = "Key for weapon set 3",
            LabelTwsKey03 = "Key for weapon set 4",
            LabelActionBarEnable = "Enable action bar hotkeys",
            LabelActionBarToggleAbility = "Toggle ability panel",
            LabelActionBarToggleQuick = "Toggle quick slot panel",
            LabelActionBarToggleSpells = "Toggle spells panel",
            DescTwsCircle = "Circle through all weapon sets",
            DescUsitUseActonBarPlacement = "Items displayed in the Quick Slot bar are always left aligned regardless of their position on the inventory panel. With this option enabled the placement of the Quick Slot bar is used, otherwise that of the inventory.",
            DescTwsEnableForAll = "When enabled, weapon set hotkey functions will be applied to all selected characters",
            DescTwsEnableForceChangeInFullScreen = "When enabled, weapon set hotkey functions will be applied to all selected characters when the full screen ui is active. <b>(The selected character in the full screen ui does not represent the selection 'on the field')</b>",
            DescUsitMain = "<color=#e6e600>Known issue: Selected items a character <b>CANNOT</b> use, i.e. spells, etc., are activatable via hotkey but the items ability is not executed and the item is not consumed</color>",
            DescTwsEnableFullScreen = "When enabled, weapon set hotkey functions will be applied when the full screen ui, i.e. inventory, spellbook, etc., is active",

            LabelGuiButtonClear = "Clear",
            UpdateWarning1x2x = "!!! All bindings need to be redone when updating from version 1.* to version 2.* !!!",

            GroupAc = "Action Bar",
            GroupTais = "AI and Stealth",
            GroupCs = "Character selection",
            GroupForm = "Formations",
            GroupQs = "Quick slots",
            GroupWs = "Weapon sets"
        };

        public string UpdateWarning1x2x { get; set; }

        #region group

        public string GroupAc { get; set; }
        public string GroupCs { get; set; }
        public string GroupForm { get; set; }
        public string GroupQs { get; set; }
        public string GroupTais { get; set; }
        public string GroupWs { get; set; }

        #endregion group

        #region label

        public string LabelActionBarEnable { get; set; }
        public string LabelActionBarToggleAbility { get; set; }
        public string LabelActionBarToggleQuick { get; set; }
        public string LabelActionBarToggleSpells { get; set; }
        public string LabelCsEnable { get; set; }
        public string LabelCsNext { get; set; }
        public string LabelCsPrev { get; set; }
        public string LabelFormEnable { get; set; }
        public string LabelFormKey00 { get; set; }
        public string LabelFormKey01 { get; set; }
        public string LabelFormKey02 { get; set; }
        public string LabelFormKey03 { get; set; }
        public string LabelFormKey04 { get; set; }
        public string LabelFormKey05 { get; set; }
        public string LabelFormKeyCircle { get; set; }
        public string LabelGenNoValidation { get; set; }
        public string LabelGenVerboseLogging { get; set; }
        public string LabelGuiButtonClear { get; set; }
        public string LabelTaisEnable { get; set; }
        public string LabelTaisKeyAi { get; set; }
        public string LabelTaisKeyStealth { get; set; }
        public string LabelTwsCycle { get; set; }
        public string LabelTwsEnable { get; set; }
        public string LabelTwsEnableForAll { get; set; }
        public string LabelTwsEnableForceChangeInFullScreen { get; set; }
        public string LabelTwsEnableFullScreen { get; set; }
        public string LabelTwsKey00 { get; set; }
        public string LabelTwsKey01 { get; set; }
        public string LabelTwsKey02 { get; set; }
        public string LabelTwsKey03 { get; set; }
        public string LabelUsitEnable { get; set; }
        public string LabelUsitEnableForAll { get; set; }
        public string LabelUsitKey00 { get; set; }
        public string LabelUsitKey01 { get; set; }
        public string LabelUsitKey02 { get; set; }
        public string LabelUsitKey03 { get; set; }
        public string LabelUsitKey04 { get; set; }
        public string LabelUsitUseActonBarPlacement { get; set; }

        #endregion label

        #region desc

        public string DescTwsCircle { get; set; }
        public string DescTwsEnableForAll { get; set; }
        public string DescTwsEnableForceChangeInFullScreen { get; set; }
        public string DescTwsEnableFullScreen { get; set; }
        public string DescUsitMain { get; set; }
        public string DescUsitUseActonBarPlacement { get; set; }

        #endregion desc
    }
}
