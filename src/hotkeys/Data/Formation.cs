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

        internal static readonly string KeyBtnEnableTitle = Utilities.GetKey("fm.btn.enable.title");
        internal static readonly string KeyHeader = Utilities.GetKey("fm.header");
        internal static readonly string KeyKb00Title = Utilities.GetKey("fm.kb.00.title");
        internal static readonly string KeyKb01Title = Utilities.GetKey("fm.kb.01.title");
        internal static readonly string KeyKb02Title = Utilities.GetKey("fm.kb.02.title");
        internal static readonly string KeyKb03Title = Utilities.GetKey("fm.kb.03.title");
        internal static readonly string KeyKb04Title = Utilities.GetKey("fm.kb.04.title");
        internal static readonly string KeyKb05Title = Utilities.GetKey("fm.kb.05.title");
        internal static readonly string KeyKbCycleTitle = Utilities.GetKey("fm.kb.cycle.title");

        internal static void AddModMenuSettings(SettingsBuilder sb)
        {
            Log.Log("Initializing weapon set settings", Globals.LogPrefix);

            sb.AddSubHeader(Utilities.GetString(KeyHeader))
                .AddToggle(
                    Toggle.New(KeyBtnEnable, false, Utilities.KeyBtnEnableString).WithLongDescription(Utilities.GetString(KeyBtnEnableTitle)))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb00, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb00Title)), () => OnPress(0))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb01, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb01Title)), () => OnPress(1))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb02, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb02Title)), () => OnPress(2))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb03, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb03Title)), () => OnPress(3))
                 .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb04, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb04Title)), () => OnPress(4))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKb05, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKb05Title)), () => OnPress(5))
                .AddKeyBinding(
                    KeyBinding
                        .New(KeyKbCycle, GameModesGroup.AllExceptBugReport, Utilities.GetString(KeyKbCycleTitle)), () => OnPress(-1));
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
