// Copyright (c) apocc.
// Licensed under MIT License.

using Kingmaker.UI.FullScreenUITypes;
using Log = UnityModManagerNet.UnityModManager.Logger;

namespace Apocc.Pw.Hotkeys.Data
{
    internal sealed class FullScreenUiTypeReporter : Kingmaker.PubSubSystem.IFullScreenUIHandler
    {
        public FullScreenUIType CurrentFullScreenUIType { get; private set; } = FullScreenUIType.Unknown;

        public void HandleFullScreenUiChanged(bool state, FullScreenUIType fullScreenUIType)
        {
            if (Main.Settings.EnableVerboseLogging)
                Log.Log($"FullScreenUIType state {state} changed to {fullScreenUIType}", Globals.LogPrefix);

            CurrentFullScreenUIType = state ? fullScreenUIType : FullScreenUIType.Unknown;
        }
    }
}
