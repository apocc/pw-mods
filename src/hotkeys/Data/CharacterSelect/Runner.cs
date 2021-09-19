// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using System.Linq;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.Common;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data.CharacterSelect
{
    internal static class Runner
    {
        private static int _selectedCharacterIndex = 0;

        private static void ChangeCharacter(bool next = true)
        {
#if DEBUG
            var parties = new Dictionary<string, IEnumerable<UnitEntityData>>
            {
                { "group capital", UIUtility.GetGroup(Game.Instance.LoadedAreaState.Settings.CapitalPartyMode.Value) },
                { "group capital and pets", UIUtility.GetGroup(Game.Instance.LoadedAreaState.Settings.CapitalPartyMode.Value, true) },
                { "party", Game.Instance.Player.Party },
                { "party and pets", Game.Instance.Player.PartyAndPets },
                { "party and pets detached", Game.Instance.Player.PartyAndPetsDetached },
                { "party characters", Game.Instance.Player.PartyCharacters.Select(u => u.Value).ToList() },
                { "remote companions", Game.Instance.Player.RemoteCompanions },
                { "active companions", Game.Instance.Player.ActiveCompanions },
                { "all characters", Game.Instance.Player.AllCharacters }
            };

            foreach (var p in parties)
                UIUtility.SendWarning($"{ p.Key}: { string.Join(",", p.Value.Select(c => c.CharacterName))}");
#endif
            var isInFullScreenUi = Utilities.IsFullScreenUiWithCharSelect();
            var party = Utilities.GetParty();

            if (Main.Settings.EnableVerboseLogging)
            {
                Log.Log($"ChangeCharacter: in inventory: {isInFullScreenUi}, next: {next}", Globals.LogPrefix);
                Log.Log($"ChangeCharacter: party: {string.Join(",", party.Select(c => c.CharacterName))}", Globals.LogPrefix);
            }

            if (isInFullScreenUi)
            {
                UpdateIndex(next, party);
            }
            else
            {
                if (Game.Instance.UI.SelectionManager.SelectedUnits.Count > 1)
                    _selectedCharacterIndex = 0;
                else
                    UpdateIndex(next, party);
            }

            UnitEntityData newChar = party[_selectedCharacterIndex];

            if (Main.Settings.EnableVerboseLogging)
            {
                Log.Log($"ChangeCharacter: change to character: {newChar.CharacterName}", Globals.LogPrefix);
            }

            Game.Instance.SelectionCharacter.SetSelected(newChar);
        }

        private static void UpdateIndex(bool next, List<UnitEntityData> party)
        {
            UnitEntityData current = UIUtility.GetCurrentCharacter();
            for (var i = 0; i < party.Count; i++)
            {
                if (party[i].UniqueId != current.UniqueId) continue;
                _selectedCharacterIndex = i;

                break;
            }

            var newIndex = _selectedCharacterIndex + (next ? 1 : -1);
            _selectedCharacterIndex = newIndex >= party.Count ? 0 : (newIndex < 0 ? party.Count - 1 : newIndex);

            if (Main.Settings.EnableVerboseLogging)
            {
                Log.Log($"ChangeCharacter: new index: {_selectedCharacterIndex}", Globals.LogPrefix);
            }
        }

        internal static void Run()
        {
            if (Input.GetKeyUp(Main.Settings.CsKeyCodeNext))
                ChangeCharacter();

            if (Input.GetKeyUp(Main.Settings.CsKeyCodePrev))
                ChangeCharacter(false);
        }
    }
}
