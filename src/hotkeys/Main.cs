using HarmonyLib;
using Kingmaker;
using Kingmaker.GameModes;
using System;
using System.Reflection;
using UnityModManagerNet;

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

                    if (Settings.Instance.EnableTws)
                        ToggleWeaponSet.Run();

                    if (Settings.Instance.EnableTAiS)
                        ToggleAiStealth.Run();

                    if (Settings.Instance.EnableUsit)
                        UsableItems.Run();
                }
                catch (Exception e)
                {
                    Globals.LogException(e);
                }
            }
        }

        public static bool enabled;
        public static UnityModManager.ModEntry.ModLogger Log;

#if DEBUG

        private static bool Unload(UnityModManager.ModEntry modEntry)
        {
            new Harmony(modEntry.Info.Id).UnpatchAll();

            return true;
        }

#endif

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            Log = modEntry.Logger;

            try
            {
                Settings.Load(modEntry);

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
                Globals.LogException(e);
            }

            return false;
        }

        public static void OnSave(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Settings.Instance.Save(modEntry);
            }
            catch (Exception e)
            {
                Log.Error($"Could not save settings!");
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
