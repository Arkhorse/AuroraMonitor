//namespace AuroraMonitor
//{
//    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.Deserialize), new Type[] { typeof(string) })]
//    public class WeatherTransition_Deserialize
//    {
//        public static void Postfix(WeatherTransition __instance, ref string text)
//        {
//            Weather weatherComponent = GameManager.GetWeatherComponent();
//            weatherComponent.m_AuroraEarlyWindowProbability = Settings.Instance.AuroraEarlyWindowProb;
//            weatherComponent.m_AuroraLateWindowProbability = Settings.Instance.AuroraLateWindowProb;
//        }
//    }
//}
