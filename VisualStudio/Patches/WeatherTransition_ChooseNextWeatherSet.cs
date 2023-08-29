namespace AuroraMonitor
{
    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.ChooseNextWeatherSet))]
    internal class WeatherTransition_ChooseNextWeatherSet
    {
        private static void Postfix()
        {
            if (GameManager.GetExperienceModeManagerComponent().IsChallengeActive()) return; // mod not usable in challenges
            string? nextWeather = null;
            nextWeather = GameManager.GetWeatherComponent().GetWeatherStage() switch
            {
                WeatherStage.DenseFog           => "GAMEPLAY_WeatherHeavyFog",
                WeatherStage.LightSnow          => "GAMEPLAY_WeatherLightSnow",
                WeatherStage.HeavySnow          => "GAMEPLAY_WeatherHeavySnow",
                WeatherStage.PartlyCloudy       => "GAMEPLAY_PartlyCloudy",
                WeatherStage.Clear              => "GAMEPLAY_WeatherClear",
                WeatherStage.Cloudy             => "GAMEPLAY_Cloudy",
                WeatherStage.LightFog           => "GAMEPLAY_WeatherLightFog",
                WeatherStage.Blizzard           => "GAMEPLAY_WeatherBlizzard",
                WeatherStage.ClearAurora        => "GAMEPLAY_know_th_AuroraObservations1_Title",    // Aurora Borealis
                WeatherStage.ToxicFog           => "GAMEPLAY_AfflictionToxicFog",                   // Toxic Fog - only darkwalker challenge as of 2.22
                WeatherStage.ElectrostaticFog   => "GAMEPLAY_ElectrostaticFog",                     // Glimmer Fog
                WeatherStage.Undefined          => null,
                _ => null,
            };
            if (nextWeather == null) return;
            Utilities.AuroraMonitorMessage($"Weather Update: {Localization.Get(nextWeather)}", Settings.Instance.WeatherStageNotificationTime);
        }
    }
}
