using System;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public static class Globals
    {
        public const int ButtonWidth = 75;
        public const string ClearButtonText = "Clear";
        public const int LabelWidth = 225;
        public const int TextFieldWidth = 125;

        public static string LogPrefix = "[ApoccHotkeys] ";
        public static string ModTitle = "Additional Hotkeys";
        public static string Sepatator = "---------------";

        public static void LogException(Exception e)
        {
            Log.Error($"{Sepatator}{ModTitle}\n{e.Message}:\n{e.StackTrace}\n{Sepatator}", LogPrefix);

            if (e.InnerException == null) return;

            LogException(e.InnerException);
        }
    }
}
