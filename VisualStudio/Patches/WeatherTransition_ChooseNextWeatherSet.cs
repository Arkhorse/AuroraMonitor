namespace AuroraMonitor
{
    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.ChooseNextWeatherSet))]
    internal class WeatherTransition_ChooseNextWeatherSet
    {
        private static void Postfix()
        {
            if (GameManager.GetExperienceModeManagerComponent().IsChallengeActive()) return; // mod not usable in challenges
            if (Main.GetCurrentWeatherLoc == null) return;
            Main.ShowNewWeatherAlert();
        }
    }
}
