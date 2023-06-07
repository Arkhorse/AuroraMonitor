namespace AuroraMonitor
{
    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.ChooseNextWeatherSet))]
    internal class WeatherTransition_ChooseNextWeatherSet
    {
        private static void Postfix()
        {
            Utilities.AuroraMonitorMessage($"Next Weather: {GameManager.GetWeatherComponent().GetWeatherStage()}", Settings.Instance.WeatherStageNotificationTime);
        }
    }
}
