using Kingmaker;
using UnityEngine;

namespace Apocc.Pw.Hotkeys
{
    public static class ToggleWeaponSet
    {
        private static void TryIncrement(bool allChars)
        {
            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            if (allChars)
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

        private static void TrySetIdx(int nIdx, bool allChars)
        {
            SelectionManagerBase sm = Game.Instance.UI.SelectionManager;

            if (allChars)
            {
                foreach (Kingmaker.EntitySystem.Entities.UnitEntityData unit in sm.SelectedUnits)
                    unit.Body.CurrentHandEquipmentSetIndex = nIdx;

                return;
            }

            if (sm.SelectedUnits.Count != 1)
                return;

            sm.SelectedUnits[0].Body.CurrentHandEquipmentSetIndex = nIdx;
        }

        public static void Run(Settings settings)
        {
            if (Input.GetKeyUp(settings.TwsKeyCode00))
                TrySetIdx(0, settings.EnableAllSelectedCharacters);
            if (Input.GetKeyUp(settings.TwsKeyCode01))
                TrySetIdx(1, settings.EnableAllSelectedCharacters);
            if (Input.GetKeyUp(settings.TwsKeyCode02))
                TrySetIdx(2, settings.EnableAllSelectedCharacters);
            if (Input.GetKeyUp(settings.TwsKeyCode03))
                TrySetIdx(3, settings.EnableAllSelectedCharacters);

            if (Input.GetKeyUp(settings.TwsKeyCodeToggle))
                TryIncrement(settings.EnableAllSelectedCharacters);
        }
    }
}
