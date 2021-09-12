using System;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public static class Globals
    {
        public static string LogPrefix = "[ApoccHotkeys] ";
        public static string ModTitle = "Hotkeys for Weapon Sets, AI and Stealth";
        public static string Sepatator = "---------------";
        public static void LogException(Exception e) => 
            Log.Error($"{Sepatator}{ModTitle}\n{e.Message}:\n{e.StackTrace}\n{Sepatator}", LogPrefix);
    }
}
