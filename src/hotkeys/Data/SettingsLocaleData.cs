﻿// Copyright (c) apocc.
// Licensed under MIT License.

namespace Apocc.Pw.Hotkeys.Data
{
    public sealed class SettingsLocaleData
    {
        public static SettingsLocaleData Default = new SettingsLocaleData
        {
            LabelGenNotInGame = "Not in game, hotkeys are disabled",
            LabelGenNoValidation = "There is no validation. Make sure other installed mods don't use the same key",
            LabelGenVerboseLogging = "Enable verbose logging",
            LabelTaisEnable = "Enable Toggle AI and Stealth",
            LabelTaisKeyAi = "Key to toggle AI",
            LabelTaisKeyStealth = "Key to toggle Stealth",
            LabelCsEnable = "Enable character selection",
            LabelCsNext = "Key to select next character",
            LabelCsPrev = "Key to select previous character",
            LabelUsitEnable = "Enable Usable Items hotkeys",
            LabelUsitEnableForAll = "For all selected chararacters",
            LabelUsitKey00 = "Key Slot 1",
            LabelUsitKey01 = "Key Slot 2",
            LabelUsitKey02 = "Key Slot 3",
            LabelUsitKey03 = "Key Slot 4",
            LabelUsitKey04 = "Key Slot 5",
            LabelUsitUseActonBarPlacement = "Use action bar placement",
            LabelTwsCycle = "Key weapon cycle",
            LabelTwsEnable = "Enable Toggle Weapon Sets",
            LabelTwsEnableForAll = "For all selected chararacters",
            LabelTwsEnableForceChangeInFullScreen = "Force weapon set change for all selected characters even when in full screen ui",
            LabelTwsEnableFullScreen = "Enable in full screen ui",
            LabelTwsKey00 = "Key for weapon set 1",
            LabelTwsKey01 = "Key for weapon set 2",
            LabelTwsKey02 = "Key for weapon set 3",
            LabelTwsKey03 = "Key for weapon set 4",

            DescTwsCircle = "Circle through all weapon sets",
            DescUsitUseActonBarPlacement = "Items displayed in the Usable Items bar are always left aligned regardless of their position on the inventory panel. With this option enabled the placement of the action bar is used, otherwise that of the inventory.",
            DescTwsEnableForAll = "When enabled, weapon set hotkey functions will be applied to all selected characters",
            DescTwsEnableForceChangeInFullScreen = "When enabled, weapon set hotkey functions are still applied to all selected characters even when the full screen ui is active. <b>(The selected character in the full screen ui does not represent the selection 'on the field')</b>",
            DescUsitMain= "<color=#e6e600>Known issue: Selected items a character cannot use, i.e. spells, etc., are activatable via hotkey but the items ability is not executed and the item is not consumed.</color>",
            DescTwsEnableFullScreen = "When enabled, weapon set hotkey functions are still applied when the full screen ui, i.e. inventory, spellbook, etc., is active",

            LabelGuiButtonClear = "Clear"
        };

        #region label

        public string LabelCsEnable { get; set; }
        public string LabelCsNext { get; set; }
        public string LabelCsPrev { get; set; }
        public string LabelGenNotInGame { get; set; }
        public string LabelGenNoValidation { get; set; }
        public string LabelGenVerboseLogging { get; set; }

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

        #region gui

        public string LabelGuiButtonClear { get; set; }

        #endregion gui
    }
}
