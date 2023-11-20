﻿using AuroraMonitor.Utilities.Logger;

namespace AuroraMonitor.Patches
{
    [HarmonyPatch(typeof(Panel_FirstAid), nameof(Panel_FirstAid.Enable), new Type[] { typeof(bool) })]
    public class Panel_FirstAid_Enable
    {
        private static bool WasInit { get; set; } = false;
        public static void Prefix(Panel_FirstAid __instance)
        {
            if (!__instance.enabled) return;
            if (!WasInit)
            {
                __instance.gameObject.AddComponent<FirstAidPanel_Addons>();
                WasInit = true;

                Main.Logger.Log(FlaggedLoggingLevel.Debug, "Added FirstAidPanel_Addons to Panel_FirstAid");
            }
        }
    }
}
