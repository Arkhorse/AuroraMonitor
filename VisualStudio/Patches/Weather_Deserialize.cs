namespace AuroraMonitor
{
    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.Deserialize), new Type[] { typeof(string) })]
    public class WeatherTransition_Deserialize
    {
        public static void Postfix()
        {
            Weather weatherComponent = GameManager.GetWeatherComponent();
            weatherComponent.m_AuroraEarlyWindowProbability = Settings.Instance.AuroraChanceEarly;
            weatherComponent.m_AuroraLateWindowProbability = Settings.Instance.AuroraChanceLate;
        }
    }
}
