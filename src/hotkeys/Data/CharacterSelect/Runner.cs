// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.UI.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data.CharacterSelect
{
    internal static class Runner
    {
        private static int _selectedCharacterIndex = 0;

        private static void ChangeCharacter(bool next = true)
        {
            var inInventory = Game.Instance.CurrentMode == GameModeType.FullScreenUi;
            Player.CharactersList list = inInventory ? Player.CharactersList.AllDetachedUnits : Player.CharactersList.ActiveUnits;
            List<UnitEntityData> party = inInventory
                ? Game.Instance.Player.PartyCharacters.Select(reference => reference.Value).ToList()
                : Game.Instance.Player.Party;

            if (Main.Settings.EnableVerboseLogging)
            {
                //Log.Log($"Party: {string.Join(",", Game.Instance.Player.Party.Select(c => c.CharacterName))}", Globals.LogPrefix);
                //Log.Log($"Party + pets: {string.Join(",", Game.Instance.Player.PartyAndPets.Select(c => c.CharacterName))}", Globals.LogPrefix);
                //Log.Log($"Party + pets detached: {string.Join(",", Game.Instance.Player.PartyAndPetsDetached.Select(c => c.CharacterName))}", Globals.LogPrefix);
                //Log.Log($"Party chars: {string.Join(",", Game.Instance.Player.PartyCharacters.Select(c => c.Value.CharacterName))}", Globals.LogPrefix);
                //Log.Log($"Party remote: {string.Join(",", Game.Instance.Player.RemoteCompanions.Select(c => c.CharacterName))}", Globals.LogPrefix);
                //Log.Log($"Party active: {string.Join(",", Game.Instance.Player.ActiveCompanions.Select(c => c.CharacterName))}", Globals.LogPrefix);
                //Log.Log($"Party all: {string.Join(",", Game.Instance.Player.AllCharacters.Select(c => c.CharacterName))}", Globals.LogPrefix);

                Log.Log($"ChangeCharacter: in inventory: {inInventory}, next: {next}", Globals.LogPrefix);
                Log.Log($"ChangeCharacter: party: {string.Join(",", party.Select(c => c.CharacterName))}", Globals.LogPrefix);
            }

            if (inInventory)
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
