// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using System.Linq;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.Common;
using ModMenu.Settings;

using static Kingmaker.UI.KeyboardAccess;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data
{
    internal static class CharacterSelect
    {
        private static int _selectedCharacterIndex = 0;

        internal static readonly string KeyBtnEnable = Utilities.GetKey("cs.btn.enable");
        internal static readonly string KeyKbNext = Utilities.GetKey("cs.kb.next");
        internal static readonly string KeyKbPrev = Utilities.GetKey("cs.kb.prev");

        internal static readonly string KeyBtnEnableDesc = Utilities.GetKey("cs.btn.enable.desc");
        internal static readonly string KeyHeader = Utilities.GetKey("cs.header");
        internal static readonly string KeyKbNextDesc = Utilities.GetKey("cs.kb.next.desc");
        internal static readonly string KeyKbPrevDesc = Utilities.GetKey("cs.kb.prev.desc");

        internal static void AddModMenuSettings(SettingsBuilder sb)
        {
            Log.Log("Initializing character select settings", Globals.LogPrefix);

            sb.AddSubHeader(Utilities.GetString(KeyHeader))
                .AddToggle(
                    Toggle
                        .New(KeyBtnEnable, false, Utilities.GetString(KeyBtnEnableDesc)))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbNext, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbNextDesc)), () => OnPress())
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbPrev, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbPrevDesc)), () => OnPress(false));
        }

        private static void OnPress(bool next = true)
        {
            var enabled = ModMenu.ModMenu.GetSettingValue<bool>(KeyBtnEnable);
            if (enabled && Utilities.TypesCharacterSelectionVisible.Contains(Main.Reporter.CurrentFullScreenUIType))
                ChangeCharacter(next);
        }

        private static void ChangeCharacter(bool next)
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
                UIUtility.SendWarning($"{p.Key}: {string.Join(",", p.Value.Select(c => c.CharacterName))}");
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
                if (Game.Instance.SelectionCharacter.SelectedUnits.Count > 1)
                    _selectedCharacterIndex = 0;
                else
                    UpdateIndex(next, party);
            }

            UnitEntityData newChar = party[_selectedCharacterIndex];

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ChangeCharacter: change to character: {newChar.CharacterName}", Globals.LogPrefix);

            Game.Instance.SelectionCharacter.SetSelected(newChar);
        }

        private static void UpdateIndex(bool next, List<UnitEntityData> party)
        {
            UnitEntityData current = Game.Instance.SelectionCharacter.CurrentSelectedCharacter;
            for (var i = 0; i < party.Count; i++)
            {
                if (party[i].UniqueId != current.UniqueId) continue;
                _selectedCharacterIndex = i;

                break;
            }

            var newIndex = _selectedCharacterIndex + (next ? 1 : -1);
            _selectedCharacterIndex = newIndex >= party.Count ? 0 : (newIndex < 0 ? party.Count - 1 : newIndex);

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"ChangeCharacter: new index: {_selectedCharacterIndex}", Globals.LogPrefix);
        }
    }
}
