//namespace AuroraMonitor
//{
//    [HarmonyPatch(typeof(Panel_FirstAid), nameof(Panel_FirstAid.Enable), new Type[] { typeof(bool) })]
//    internal class Panel_FirstAid_Enable
//    {
//        private static bool added { get; set; } = false;
//        public static void Prefix(Panel_FirstAid __instance, ref bool enable)
//        {
//            if (added) return;

//            Vector3 pos = __instance.m_LabelConditionPercent.transform.localPosition;
//        }
//    }
//}
