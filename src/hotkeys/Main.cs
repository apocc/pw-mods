// Copyright (c) apocc.
// Licensed under MIT License.

using System;
using System.Reflection;
using Apocc.Pw.Hotkeys.Data;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using UnityModManagerNet;

using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
#if DEBUG

    [EnableReloading]
#endif
    public static class Main
    {
        [HarmonyPatch(typeof(BlueprintsCache))]
        static class BlueprintsCache_Patches
        {
            [HarmonyPriority(Priority.First)]
            [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
            static void Postfix()
            {
                try
                {
                    if (ModMenuSettings == null)
                        ModMenuSettings = new ModMenuSettings();

                    ModMenuSettings.Init();
                }
                catch (Exception e)
                {
                    Log.Error("Postfix: Error BlueprintsCache.Init", Globals.LogPrefix);
                    Globals.LogException(e);
                }
            }
        }

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
            }
        }

        internal static ModMenuSettings ModMenuSettings;
        internal static IDisposable DisposableReporter;
        internal static FullScreenUiTypeReporter Reporter;

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
                Settings = Settings.LoadSettings(modEntry);

                var h = new Harmony(modEntry.Info.Id);
                h.PatchAll(Assembly.GetExecutingAssembly());

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
