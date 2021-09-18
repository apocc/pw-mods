﻿// Copyright (c) apocc.
// Licensed under MIT License.

using System;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys
{
    public static class Globals
    {
        public const int ButtonWidth = 100;
        public const int DescWidth = 350;
        public const string I18NLocation = "I18N";
        public const string DefaultCulture = "en-gb";
        public const int LabelWidth = 225;
        public const int TextFieldWidth = 125;
        public const int ControlSpace = 20;

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
