namespace AuroraMonitor.Utilities
{
    public class Aurora
    {
        private static AuroraManager manager = GameManager.GetAuroraManager();
        private static Il2Cpp.Weather weatherComponent = GameManager.GetWeatherComponent();
        public static void SetAuroraChancesEarly(int early)
        {
            weatherComponent.m_AuroraEarlyWindowProbability = early;
        }
        public static void SetAuroraChancesLate(int late)
        {
            weatherComponent.m_AuroraLateWindowProbability = late;
        }

        public static void SetAuroraChances(int early, int late)
        {
            weatherComponent.m_AuroraEarlyWindowProbability = early;
            weatherComponent.m_AuroraLateWindowProbability = late;
        }
    }
}
