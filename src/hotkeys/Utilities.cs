// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using System.Linq;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.UI.Common;
using Kingmaker.UI.FullScreenUITypes;

namespace Apocc.Pw.Hotkeys
{
    internal static class Utilities
    {
        internal static List<FullScreenUIType> TypesCharacterSelectionVisible = new List<FullScreenUIType>
        {
            FullScreenUIType.Unknown,
            FullScreenUIType.Inventory,
            FullScreenUIType.SpellBook,
            FullScreenUIType.CharacterScreen
        };

        internal static List<UnitEntityData> GetParty()
        {
            var isInFullScreenUiChars = IsFullScreenUiWithCharSelect();

            List<UnitEntityData> party = isInFullScreenUiChars
                ? UIUtility.GetGroup(Game.Instance.LoadedAreaState.Settings.CapitalPartyMode.Value)
                : Game.Instance.Player.PartyAndPets;

            if (isInFullScreenUiChars)
                party.AddRange(Game.Instance.Player.PartyAndPets.Where(u => u.IsPet));

            return party;
        }

        internal static bool IsFullScreenUiWithCharSelect() =>
                    Game.Instance.CurrentMode == GameModeType.FullScreenUi &&
            TypesCharacterSelectionVisible.Contains(Main.Reporter.CurrentFullScreenUIType);
    }
}
