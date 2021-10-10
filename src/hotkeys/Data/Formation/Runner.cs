// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker;
using Kingmaker.Blueprints.Root;
using Kingmaker.UI.MVVM._PCView.Formation;
using Owlcat.Runtime.UI.Controls.Button;
using UnityEngine;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data.Formation
{
    internal static class Runner
    {
        private static void SetNewFormationIndex(int index = -1)
        {
            var ci = Game.Instance.Player.FormationManager.CurrentFormationIndex;
            if (index == ci)
            {
                if (Main.Settings.EnableVerboseLogging)
                    Log.Log($"Formation: Index hasn't changed, no update needed.)", Globals.LogPrefix);

                return;
            }

            var formLength = BlueprintRoot.Instance.Formations.PredefinedFormations.Length;
            var nIdx = index < 0 ? (ci + 1) % formLength : index;

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"Formation: Updating formation index ({nIdx})", Globals.LogPrefix);

            Game.Instance.Player.FormationManager.CurrentFormationIndex = nIdx;

            var form = Game.Instance.UI.MainCanvas.transform.Find("FormationPCView/FormationSelectorPCView");
            if (form == null)
            {
                Log.Error($"Formation: Formation selector prefab wasn't found, UI will not update.", Globals.LogPrefix);
                return;
            }

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"Formation: FormationSelectorPCView found", Globals.LogPrefix);

            var items = form.gameObject.GetComponentsInChildren<FormationSelectionItemPCView>();
            if (items.Length != formLength)
            {
                Log.Error($"Formation: Button length is invalid, expected {formLength} but was {items.Length}. UI will not update.", Globals.LogPrefix);
                return;
            }

            var cIdxStr = nIdx.ToString();
            foreach (var view in items)
            {
                if (!view.name.EndsWith(cIdxStr)) continue;
                if (Main.Settings.EnableVerboseLogging)
                    Log.Log($"Formation: FormationSelectorPCView selected ({view.name})", Globals.LogPrefix);

                var btn = view.GetComponentInChildren<OwlcatMultiButton>();
                if (btn == null)
                {
                    Log.Error($"Formation: FormationSelectorPCView contains no button component, UI will not update", Globals.LogPrefix);
                    break;
                }

                if (Main.Settings.EnableVerboseLogging)
                    Log.Log($"Formation: OwlcatMultiButton found ({btn.name})", Globals.LogPrefix);

                btn.ConfirmClickEvent.Invoke();
            }
        }

        internal static void Run()
        {
            if (Input.GetKeyUp(Main.Settings.FormKeyCode00))
                SetNewFormationIndex(0);
            if (Input.GetKeyUp(Main.Settings.FormKeyCode01))
                SetNewFormationIndex(1);
            if (Input.GetKeyUp(Main.Settings.FormKeyCode02))
                SetNewFormationIndex(2);
            if (Input.GetKeyUp(Main.Settings.FormKeyCode03))
                SetNewFormationIndex(3);
            if (Input.GetKeyUp(Main.Settings.FormKeyCode04))
                SetNewFormationIndex(4);
            if (Input.GetKeyUp(Main.Settings.FormKeyCode05))
                SetNewFormationIndex(5);

            if (Input.GetKeyUp(Main.Settings.FormKeyCodeCircle))
                SetNewFormationIndex();
        }
    }
}
