// Copyright (c) apocc.
// Licensed under MIT License.

using System;
using System.Collections.Generic;
using System.Reflection;
using Apocc.Pw.Hotkeys.Data;
using HarmonyLib;
using Kingmaker;
using Kingmaker.GameModes;
using Kingmaker.PubSubSystem;
using Kingmaker.UI.FullScreenUITypes;
using UnityModManagerNet;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
#if DEBUG

    [EnableReloading]
#endif
    public static class Main
    {
        [HarmonyPatch(typeof(UnityModManager.UI), "Update")]
        public static class UnityModManager_UI_Update_Patch
        {
            public static void Postfix(UnityModManager.UI __instance)
            {
                if (!enabled)
                    return;

                if (Reporter == null)
                {
                    Log.Log("Init FullScreenUiTypeReporter", Globals.LogPrefix);

                    Reporter = new FullScreenUiTypeReporter();
                    DisposableReporter = EventBus.Subscribe(Reporter);
                }

                try
                {
                    GameModeType mode = Game.Instance.CurrentMode;
                    if (mode != GameModeType.Default
                        && mode != GameModeType.Pause
                        && mode != GameModeType.FullScreenUi)
                        return;

                    if (Settings.EnableTws && TypesForTws.Contains(Reporter.CurrentFullScreenUIType))
                        Data.WeaponSets.Runner.Run();

                    if (mode != GameModeType.FullScreenUi && Settings.EnableTAiS)
                        Data.AiStealth.Runner.Run();

                    if (Settings.EnableUsit && TypesForUsit.Contains(Reporter.CurrentFullScreenUIType))
                        Data.UsableItems.Runner.Run();

                    if (Settings.EnableCharSel &&
                        Utilities.TypesCharacterSelectionVisible.Contains(Reporter.CurrentFullScreenUIType))
                        Data.CharacterSelect.Runner.Run();

                    if (Settings.EnableForm &&
                        TypesForForm.Contains(Reporter.CurrentFullScreenUIType))
                        Data.Formation.Runner.Run();
                }
                catch (Exception e)
                {
                    Log.Error("Postfix: Couldn't exec Postfix", Globals.LogPrefix);
                    Globals.LogException(e);
                }
            }
        }

        internal static IDisposable DisposableReporter;
        internal static FullScreenUiTypeReporter Reporter;

        internal static List<FullScreenUIType> TypesForForm = new List<FullScreenUIType>
        {
            FullScreenUIType.Unknown,
        };

        internal static List<FullScreenUIType> TypesForTws = new List<FullScreenUIType>
        {
            FullScreenUIType.Unknown,
            FullScreenUIType.Inventory,
        };

        internal static List<FullScreenUIType> TypesForUsit = new List<FullScreenUIType>
        {
            FullScreenUIType.Unknown,
            FullScreenUIType.Inventory,
        };

        public static bool enabled;
        public static Settings Settings;
#if DEBUG

        private static bool Unload(UnityModManager.ModEntry modEntry)
        {
            DisposableReporter?.Dispose();

            new Harmony(modEntry.Info.Id).UnpatchAll();
            return true;
        }

#endif

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Settings = Settings.Load(modEntry);

                var h = new Harmony(modEntry.Info.Id);
                h.PatchAll(Assembly.GetExecutingAssembly());

                modEntry.OnShowGUI = (UnityModManager.ModEntry me) => Gui.OnShowGUI(enabled, me);
                modEntry.OnGUI = (UnityModManager.ModEntry me) => Gui.OnGUI(enabled, me);
                modEntry.OnSaveGUI = OnSave;
                modEntry.OnToggle = OnToggle;
#if DEBUG
                modEntry.OnUnload = Unload;
#endif
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Main: Couldn't load mod", Globals.LogPrefix);
                Globals.LogException(e);
            }

            return false;
        }

        public static void OnSave(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Settings.Save(modEntry);
            }
            catch (Exception e)
            {
                Log.Error($"OnSave: Couldn't save settings!", Globals.LogPrefix);
                Globals.LogException(e);
            }
        }

        public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            enabled = value;
            return true;
        }
    }
}
