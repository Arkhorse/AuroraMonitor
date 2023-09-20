﻿namespace AuroraMonitor.Utilities
{
    public static class WeatherUtilities
    {
        public static string? GetCurrentWeatherLoc(UniStormWeatherSystem uniStorm)
        {
            return uniStorm.m_CurrentWeatherStage switch
            {
                WeatherStage.DenseFog => "GAMEPLAY_WeatherHeavyFog",
                WeatherStage.LightSnow => "GAMEPLAY_WeatherLightSnow",
                WeatherStage.HeavySnow => "GAMEPLAY_WeatherHeavySnow",
                WeatherStage.PartlyCloudy => "GAMEPLAY_PartlyCloudy",
                WeatherStage.Clear => "GAMEPLAY_WeatherClear",
                WeatherStage.Cloudy => "GAMEPLAY_Cloudy",
                WeatherStage.LightFog => "GAMEPLAY_WeatherLightFog",
                WeatherStage.Blizzard => "GAMEPLAY_WeatherBlizzard",
                WeatherStage.ClearAurora => "GAMEPLAY_know_th_AuroraObservations1_Title",    // Aurora Borealis
                WeatherStage.ToxicFog => "GAMEPLAY_AfflictionToxicFog",                   // Toxic Fog - only darkwalker challenge as of 2.22
                WeatherStage.ElectrostaticFog => "GAMEPLAY_ElectrostaticFog",                     // Glimmer Fog
                WeatherStage.Undefined => null,
                _ => null,
            };
        }

        public static string? GetCurrentWeatherIcon(UniStormWeatherSystem uniStorm)
        {
            return uniStorm.m_CurrentWeatherStage switch
            {
                WeatherStage.DenseFog => "ico_journal",
                WeatherStage.LightSnow => "ico_MeltSnow",
                WeatherStage.HeavySnow => "ico_MeltSnow",
                WeatherStage.PartlyCloudy => "ico_journal",
                WeatherStage.Clear => "ico_journal",
                WeatherStage.Cloudy => "ico_journal",
                WeatherStage.LightFog => "ico_journal",
                WeatherStage.Blizzard => "ico_Warning",
                WeatherStage.ClearAurora => "ico_Warning",       // Aurora Borealis
                WeatherStage.ToxicFog => GetToxicFogIcon(),   // Toxic Fog - only darkwalker challenge as of 2.22
                WeatherStage.ElectrostaticFog => "ico_journal",       // Glimmer Fog
                WeatherStage.Undefined => null,
                _ => null,
            };
        }

        public static string? GetToxicFogIcon()
        {
            return Settings.Instance.fogImages switch
            {
                Settings.ToxicFogImages.l1 => "ico_toxicFog_L1",
                Settings.ToxicFogImages.l2 => "ico_toxicFog_L2",
                Settings.ToxicFogImages.l3 => "ico_toxicFog_L3",
                _ => null
            };
        }

        public static WeatherStage Prev { get; set; } = WeatherStage.Undefined;

        public static bool IsValidSceneForWeather(string sceneName)
        {

            bool flag = ((SceneUtilities.IsSceneBase(sceneName)) && !(SceneUtilities.IsSceneAdditive(sceneName)));

            if (flag && !GameManager.GetWeatherComponent().IsIndoorEnvironment())
            {
                return true;
            }
            else if (GameManager.GetWeatherComponent().IsIndoorEnvironment() && Settings.Instance.WeatherNotificationsIndoors)
            {
                return true;
            }

            return false;
        }

        public static void UpdateStages(WeatherStage current)
        {
            Prev = current;
        }

        public static Vector3 GetWindDirection()
        {
            Wind wind = GameManager.GetWindComponent();
            return wind.m_CurrentDirection;
        }
    }
}