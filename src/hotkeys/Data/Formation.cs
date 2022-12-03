// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker;
using Kingmaker.Blueprints.Root;
using Kingmaker.UI.MVVM._PCView.Formation;
using ModMenu.Settings;
using Owlcat.Runtime.UI.Controls.Button;
using static Kingmaker.UI.KeyboardAccess;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data
{
    internal static class Formation
    {
        internal static readonly string KeyBtnEnable = Utilities.GetKey("fm.btn.enable");
        internal static readonly string KeyKb00 = Utilities.GetKey("fm.kb.00");
        internal static readonly string KeyKb01 = Utilities.GetKey("fm.kb.01");
        internal static readonly string KeyKb02 = Utilities.GetKey("fm.kb.02");
        internal static readonly string KeyKb03 = Utilities.GetKey("fm.kb.03");
        internal static readonly string KeyKb04 = Utilities.GetKey("fm.kb.04");
        internal static readonly string KeyKb05 = Utilities.GetKey("fm.kb.05");
        internal static readonly string KeyKbCycle = Utilities.GetKey("fm.kb.cycle");

        internal static readonly string KeyBtnEnableDesc = Utilities.GetKey("fm.btn.enable.desc");
        internal static readonly string KeyHeader = Utilities.GetKey("fm.header");
        internal static readonly string KeyKb00Desc = Utilities.GetKey("fm.kb.00.desc");
        internal static readonly string KeyKb01Desc = Utilities.GetKey("fm.kb.01.desc");
        internal static readonly string KeyKb02Desc = Utilities.GetKey("fm.kb.02.desc");
        internal static readonly string KeyKb03Desc = Utilities.GetKey("fm.kb.03.desc");
        internal static readonly string KeyKb04Desc = Utilities.GetKey("fm.kb.04.desc");
        internal static readonly string KeyKb05Desc = Utilities.GetKey("fm.kb.05.desc");
        internal static readonly string KeyKbCycleDesc = Utilities.GetKey("fm.kb.cycle.desc");

        internal static void AddModMenuSettings(SettingsBuilder sb)
        {
            Log.Log("Initializing weapon set settings", Globals.LogPrefix);

            sb.AddSubHeader(Utilities.GetString(KeyHeader))
                .AddToggle(
                    Toggle.New(KeyBtnEnable, false, Utilities.KeyBtnEnableString).WithLongDescription(Utilities.GetString(KeyBtnEnableDesc)))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb00, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb00Desc)), () => OnPress(0))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb01, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb01Desc)), () => OnPress(1))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb02, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb02Desc)), () => OnPress(2))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb03, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb03Desc)), () => OnPress(3))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb04, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb04Desc)), () => OnPress(4))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb05, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb05Desc)), () => OnPress(5))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbCycle, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbCycleDesc)), () => OnPress(-1));
        }

        private static void OnPress(int index)
        {
            var enabled = ModMenu.ModMenu.GetSettingValue<bool>(KeyBtnEnable);
            if (enabled && Utilities.TypesForForm.Contains(Main.Reporter.CurrentFullScreenUIType))
                SetNewFormationIndex(index);
        }

        private static void SetNewFormationIndex(int index)
        {
            var ci = Game.Instance.Player.FormationManager.CurrentFormationIndex;
            if (index == ci)
            {
                if (Main.Settings.EnableVerboseLogging)
                    Log.Log($"Formation: Index hasn't changed, no update needed.", Globals.LogPrefix);

                return;
            }

            var formLength = BlueprintRoot.Instance.Formations.PredefinedFormations.Length;
            var nIdx = index < 0 ? (ci + 1) % formLength : index;

            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"Formation: Updating formation index ({nIdx})", Globals.LogPrefix);

            Game.Instance.Player.FormationManager.CurrentFormationIndex = nIdx;

            var form = Game.Instance.UI.MainCanvas.transform.Find("NestedCanvas1/FormationPCView/FormationSelectorPCView");
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
    }
}
