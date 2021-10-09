// Copyright (c) apocc.
// Licensed under MIT License.

using System.Collections.Generic;
using System.Linq;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.GameModes;
using Kingmaker.UI.Common;
using Kingmaker.UI.FullScreenUITypes;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public static class Utilities
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

#if DEBUG

        public static List<string> GetObjectPaths()
        {
            var res = new List<string>();
            var objects = Resources.FindObjectsOfTypeAll<GameObject>();

            Log.Log($"GetChildGameObjectPaths: sub components: {objects.Length}");
            foreach (var obj in objects)
            {
                var path = $"/{obj.name}";
                var parent = obj.transform.parent;
                while (parent != null)
                {
                    path = $"/{parent.gameObject.name}{path}";
                    parent = parent.parent;
                }

                res.Add(path);
            }

            return res;
        }

#endif

        public static string ToStr(this IEnumerable<string> @this, string separator)
        {
            return string.Join(separator, @this);
        }
    }
}
