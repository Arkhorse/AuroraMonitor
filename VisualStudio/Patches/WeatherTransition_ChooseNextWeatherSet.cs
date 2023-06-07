namespace AuroraMonitor
{
    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.ChooseNextWeatherSet))]
    internal class WeatherTransition_ChooseNextWeatherSet
    {
        private static void Postfix()
        {
            GearMessage.AddMessage("ico_journal", "Aurora Monitor", $"Next Weather Set: {GameManager.GetWeatherComponent().GetWeatherStage()}", 15f, false, true);
        }
    }
}
