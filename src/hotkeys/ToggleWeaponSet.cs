using Kingmaker;
using UnityEngine;

namespace Apocc.Pw.Hotkeys
{
    public static class ToggleWeaponSet
    {
        private static void TryIncrement(Settings s)
        {
            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            if (s.EnableAllSelectedCharacters)
            {
                foreach (Kingmaker.EntitySystem.Entities.UnitEntityData unit in sm.SelectedUnits)
                {
                    var nIdx = unit.Body.CurrentHandEquipmentSetIndex + 1;
                    unit.Body.CurrentHandEquipmentSetIndex = nIdx % 4;
                }

                return;
            }

            if (sm.SelectedUnits.Count != 1)
                return;

            var nIdxs = sm.SelectedUnits[0].Body.CurrentHandEquipmentSetIndex + 1;
            sm.SelectedUnits[0].Body.CurrentHandEquipmentSetIndex = nIdxs % 4;
        }

        private static void TrySetIdx(int nIdx, Settings s)
        {
            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            if (s.EnableAllSelectedCharacters)
            {
                foreach (Kingmaker.EntitySystem.Entities.UnitEntityData unit in sm.SelectedUnits)
                {
                    unit.Body.CurrentHandEquipmentSetIndex = nIdx;
                }
                return;
            }

            if (sm.SelectedUnits.Count != 1)
                return;

            sm.SelectedUnits[0].Body.CurrentHandEquipmentSetIndex = nIdx;
        }

        public static void Run(Settings s)
        {
            if (Input.GetKeyUp(s.TwsKeyCode00))
                TrySetIdx(0, s);
            if (Input.GetKeyUp(s.TwsKeyCode01))
                TrySetIdx(1, s);
            if (Input.GetKeyUp(s.TwsKeyCode02))
                TrySetIdx(2, s);
            if (Input.GetKeyUp(s.TwsKeyCode03))
                TrySetIdx(3, s);

            if (Input.GetKeyUp(s.TwsKeyCodeToggle))
                TryIncrement(s);
        }
    }
}
