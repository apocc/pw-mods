using HarmonyLib;
using Kingmaker;
using Kingmaker.GameModes;
using System;
using System.Reflection;
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

                try
                {
                    GameModeType mode = Game.Instance.CurrentMode;
                    if (mode != GameModeType.Default
                        && mode != GameModeType.Pause
                        && mode != GameModeType.FullScreenUi)
                        return;

                    if (Settings.EnableTws)
                        ToggleWeaponSet.Run(Settings);

                    if (Settings.EnableTAiS)
                        ToggleAiStealth.Run(Settings);

                    if (Settings.EnableUsit)
                        UsableItems.Run(Settings);
                }
                catch (Exception e)
                {
                    Log.Error("Postfix: Couldn't exec Postfix", Globals.LogPrefix);
                    Globals.LogException(e);
                }
            }
        }

        public static bool enabled;
        public static Settings Settings;

#if DEBUG

        private static bool Unload(UnityModManager.ModEntry modEntry)
        {
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

                modEntry.OnGUI = (UnityModManager.ModEntry me) => Gui.OnGUI(enabled, me, Settings);
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
