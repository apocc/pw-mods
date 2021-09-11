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
                    if (Game.Instance.CurrentMode != GameModeType.Default && Game.Instance.CurrentMode != GameModeType.Pause)
                        return;

                    if (Settings.EnableTws)
                        ToggleWeaponSet.Run(Settings);

                    if (Settings.EnableTAiS)
                        ToggleAiStealth.Run(Settings);
                }
                catch (Exception e)
                {
                    LogException(e);
                }
            }
        }

        public static Settings Settings;
        public static bool enabled;
        public static UnityModManager.ModEntry.ModLogger Log;
        public static string ModTitle = "Hotkeys for Weapon Sets, AI and Stealth";
        public static string Sepatator = "---------------";

        private static void LoadSettings(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Settings = Settings.Load(modEntry);
            }
            catch (Exception e)
            {
                Log.Warning("Could not parse settings object!");
                LogException(e);

                Settings = new Settings();
            }

            Settings.SetLog(modEntry.Logger);
        }

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
                LoadSettings(modEntry);

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
                LogException(e);
            }

            return false;
        }

        public static void LogException(Exception e) => Log?.Error($"{Sepatator}{ModTitle}\n{e.Message}:\n{e.StackTrace}\n{Sepatator}");

        public static void OnSave(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Settings.Save(modEntry);
            }
            catch (Exception e)
            {
                Log.Error($"Could not save settings!");
                LogException(e);
            }
        }

        public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            enabled = value;
            return true;
        }
    }
}
