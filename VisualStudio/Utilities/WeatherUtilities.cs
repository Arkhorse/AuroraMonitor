﻿using AuroraMonitor.Notifications;
using AuroraMonitor.Utilities.Enums;

namespace AuroraMonitor.Utilities
{
	public static class WeatherUtilities
	{
		public static List<GearMessage.GearMessageInfo> WeatherPresets { get; } = new()
		{
			new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_WeatherHeavyFog") { m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_MeltSnow", "Weather Monitor", "GAMEPLAY_WeatherLightSnow") { m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_MeltSnow", "Weather Monitor", "GAMEPLAY_WeatherHeavySnow") { m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_PartlyCloudy") { m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_WeatherClear"){ m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_Cloudy") { m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_WeatherLightFog") { m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_almanac_hurricane", "Weather Monitor", "GAMEPLAY_WeatherBlizzard") { m_Color = Color.red, m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_Warning", "Weather Monitor", "GAMEPLAY_know_th_AuroraObservations1_Title") { m_Color = Color.yellow, m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo(GetToxicFogIcon(), "Weather Monitor", "GAMEPLAY_AfflictionToxicFog") { m_Color = Color.red, m_DisplayTime = Settings.Instance.WeatherNotificationsTime },
			new GearMessage.GearMessageInfo("ico_Warning", "Weather Monitor", "GAMEPLAY_ElectrostaticFog") { m_Color = Color.red, m_DisplayTime = Settings.Instance.WeatherNotificationsTime }
		};

		public static string? GetCurrentWeatherLoc(UniStormWeatherSystem uniStorm)
		{
			return uniStorm.m_CurrentWeatherStage switch
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
		}

		public static string? GetCurrentWeatherIcon(UniStormWeatherSystem uniStorm)
		{
			return uniStorm.m_CurrentWeatherStage switch
			{
				WeatherStage.DenseFog           => "ico_journal",
				WeatherStage.LightSnow          => "ico_MeltSnow",
				WeatherStage.HeavySnow          => "ico_MeltSnow",
				WeatherStage.PartlyCloudy       => "ico_journal",
				WeatherStage.Clear              => "ico_journal",
				WeatherStage.Cloudy             => "ico_journal",
				WeatherStage.LightFog           => "ico_journal",
				WeatherStage.Blizzard           => "ico_almanac_hurricane",
				WeatherStage.ClearAurora        => "ico_Warning",       // Aurora Borealis
				WeatherStage.ToxicFog           => GetToxicFogIcon(),   // Toxic Fog - only darkwalker challenge as of 2.22
				WeatherStage.ElectrostaticFog   => "ico_journal",       // Glimmer Fog
				WeatherStage.Undefined          => null,
				_ => null,
			};
		}

		public static GearMessage.GearMessageInfo? GetCurrentMessage( UniStormWeatherSystem uniStorm )
		{
			return uniStorm.m_CurrentWeatherStage switch
			{
				WeatherStage.DenseFog           => WeatherPresets[0],
				WeatherStage.LightSnow          => WeatherPresets[1],
				WeatherStage.HeavySnow          => WeatherPresets[2],
				WeatherStage.PartlyCloudy       => WeatherPresets[3],
				WeatherStage.Clear              => WeatherPresets[4],
				WeatherStage.Cloudy             => WeatherPresets[5],
				WeatherStage.LightFog           => WeatherPresets[6],
				WeatherStage.Blizzard           => WeatherPresets[7],
				WeatherStage.ClearAurora        => WeatherPresets[8],       // Aurora Borealis
				WeatherStage.ToxicFog           => WeatherPresets[9],       // Toxic Fog - only darkwalker challenge as of 2.22
				WeatherStage.ElectrostaticFog   => WeatherPresets[10],      // Glimmer Fog
				WeatherStage.Undefined          => null,
				_ => null,
			};
		}
		public static string? GetToxicFogIcon()
		{
			return Settings.Instance.fogImages switch
			{
				ToxicFogImages.l1 => "ico_toxicFog_L1",
				ToxicFogImages.l2 => "ico_toxicFog_L2",
				ToxicFogImages.l3 => "ico_toxicFog_L3",
				_ => null
			};
		}

		public static float ConvertMilesKilometerHour(float mph)
		{
            return mph * 1.609344f;
        }

		public static float ConvertMilesMetersSecond(float mph)
		{
			return mph * 0.44704f;
        }

		public static void UpdateStages(WeatherStage current)
		{
			if (Main.MonitorData == null) return;

			Main.MonitorData.Prev = current;
		}

		public static Vector3 GetWindDirection()
		{
			Wind wind = GameManager.GetWindComponent();
			return wind.m_CurrentDirection;
		}

        public static string GetDayString()
        {
            float result = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused() / 24f;
            return result.ToString();
        }

        public static float GetCurrentUnits(float input)
        {
            return Settings.Instance.UnitsToUse switch
            {
                UnitUse.Metric => WeatherUtilities.ConvertMilesKilometerHour(input),
                UnitUse.Scientific => WeatherUtilities.ConvertMilesMetersSecond(input),
                _ => input
            };
        }

        public static string GetCurrentUnitsString(int section)
        {
            if (section == 0)
            {
                return Settings.Instance.UnitsToUse switch
                {
                    UnitUse.Metric => "KM/H",
                    UnitUse.Scientific => "M/S",
                    _ => "MP/H"
                };
            }
            else if (section == 1)
            {
                return Settings.Instance.FirstAidScreen_UnitsToUse switch
                {
                    UnitUse.Metric => "KM/H",
                    UnitUse.Scientific => "M/S",
                    _ => "MP/H"
                };
            }

            return string.Empty;
        }

        public static int GetNormalizedSpeed(float input)
        {
            float num = GetCurrentUnits(input);
            float num1 = Mathf.Ceil(num);
            return (int)num1;
        }

		public static bool IsAuroraFullyActive(AuroraManager auroraManager)
		{
			if (auroraManager.GetNormalizedAlpha() >= auroraManager.m_FullyActiveValue) return true;
			return false;
		}
    }
}