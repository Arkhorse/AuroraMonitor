//namespace AuroraMonitor
//{
//    [HarmonyPatch(typeof(Rest), nameof(Rest.EndSleeping), new Type[] { typeof(bool) })]
//    public class Rest_EndSleeping
//    {
//        public static void Postfix()
//        {
//            MessageUtilities.Instance.ShowWeatherAlert();
//        }
//    }
//}
