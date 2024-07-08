namespace AuroraMonitor.Utilities
{
	public static class WeatherUtilities
	{
		//public static List<GearMessage.GearMessageInfo> WeatherPresets { get; } =
		//[
		//	new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_WeatherHeavyFog") { m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_MeltSnow", "Weather Monitor", "GAMEPLAY_WeatherLightSnow") { m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_MeltSnow", "Weather Monitor", "GAMEPLAY_WeatherHeavySnow") { m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_PartlyCloudy") { m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_WeatherClear"){ m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_Cloudy") { m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_journal", "Weather Monitor", "GAMEPLAY_WeatherLightFog") { m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_almanac_hurricane", "Weather Monitor", "GAMEPLAY_WeatherBlizzard") { m_Color = Color.red, m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_Warning", "Weather Monitor", "GAMEPLAY_know_th_AuroraObservations1_Title") { m_Color = Color.yellow, m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo(GetToxicFogIcon(), "Weather Monitor", "GAMEPLAY_AfflictionToxicFog") { m_Color = Color.red, m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime },
		//	new GearMessage.GearMessageInfo("ico_Warning", "Weather Monitor", "GAMEPLAY_ElectrostaticFog") { m_Color = Color.red, m_DisplayTime = Main.SettingsInstance.WeatherNotificationsTime }
		//];
		//public static GearMessage.GearMessageInfo? GetCurrentMessage( UniStormWeatherSystem uniStorm )
		//{
		//	return uniStorm.m_CurrentWeatherStage switch
		//	{
		//		WeatherStage.DenseFog           => WeatherPresets[0],
		//		WeatherStage.LightSnow          => WeatherPresets[1],
		//		WeatherStage.HeavySnow          => WeatherPresets[2],
		//		WeatherStage.PartlyCloudy       => WeatherPresets[3],
		//		WeatherStage.Clear              => WeatherPresets[4],
		//		WeatherStage.Cloudy             => WeatherPresets[5],
		//		WeatherStage.LightFog           => WeatherPresets[6],
		//		WeatherStage.Blizzard           => WeatherPresets[7],
		//		WeatherStage.ClearAurora        => WeatherPresets[8],       // Aurora Borealis
		//		WeatherStage.ToxicFog           => WeatherPresets[9],       // Toxic Fog - only darkwalker challenge as of 2.22
		//		WeatherStage.ElectrostaticFog   => WeatherPresets[10],      // Glimmer Fog
		//		_ => null,
		//	};
		//}
		//public static string? GetToxicFogIcon()
		//{
		//	return Main.SettingsInstance.fogImages switch
		//	{
		//		ToxicFogImages.l1 => "ico_toxicFog_L1",
		//		ToxicFogImages.l2 => "ico_toxicFog_L2",
		//		ToxicFogImages.l3 => "ico_toxicFog_L3",
		//		_ => null
		//	};
		//}
		public static float ConvertMilesKilometerHour(float mph)
		{
			return mph * 1.609344f;
		}
		public static float ConvertMilesMetersSecond(float mph)
		{
			return mph * 0.44704f;
		}
		public static string? GetCurrentWeatherLoc(UniStormWeatherSystem uniStorm)
		{
			return GetWeatherLoc(uniStorm.m_CurrentWeatherStage);
		}
		public static Vector3 GetWindDirection()
		{
			return GameManager.GetWindComponent().m_CurrentDirection;
		}
		public static string GetDayString()
		{
			return (GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused() / 24f).ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stage"></param>
		/// <returns></returns>
		public static string? GetWeatherLoc(WeatherStage stage)
		{
			return stage switch
			{
				WeatherStage.DenseFog			=> "GAMEPLAY_WeatherHeavyFog",
				WeatherStage.LightSnow			=> "GAMEPLAY_WeatherLightSnow",
				WeatherStage.HeavySnow			=> "GAMEPLAY_WeatherHeavySnow",
				WeatherStage.PartlyCloudy		=> "GAMEPLAY_PartlyCloudy",
				WeatherStage.Clear				=> "GAMEPLAY_WeatherClear",
				WeatherStage.Cloudy				=> "GAMEPLAY_Cloudy",
				WeatherStage.LightFog			=> "GAMEPLAY_WeatherLightFog",
				WeatherStage.Blizzard			=> "GAMEPLAY_WeatherBlizzard",
				WeatherStage.ClearAurora		=> "GAMEPLAY_know_th_AuroraObservations1_Title",    // Aurora Borealis
				WeatherStage.ToxicFog			=> "GAMEPLAY_AfflictionToxicFog",                   // Toxic Fog - only darkwalker challenge as of 2.22
				WeatherStage.ElectrostaticFog	=> "GAMEPLAY_ElectrostaticFog",                     // Glimmer Fog
				_ => null,
			};
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="uniStorm"></param>
		/// <returns></returns>
		public static string? GetCurrentWeatherIcon(UniStormWeatherSystem uniStorm)
		{
			return GetWeatherSpriteName(uniStorm.m_CurrentWeatherStage);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="stage"></param>
		/// <returns></returns>
		public static Texture2D? GetWeatherTexture(WeatherStage stage)
		{
			return stage switch
			{
				WeatherStage.DenseFog => Main.WeatherIcons.First(f => f.Name == "DenseFog").Texture,
				WeatherStage.LightSnow => Main.WeatherIcons.First(f => f.Name == "LightSnow").Texture,
				WeatherStage.HeavySnow => Main.WeatherIcons.First(f => f.Name == "HeavySnow").Texture,
				WeatherStage.PartlyCloudy when GameManager.GetUniStorm().IsNightOrNightBlend() => Main.WeatherIcons.First(f => f.Name == "PartlyCloudy_Night").Texture,
				WeatherStage.PartlyCloudy when !GameManager.GetUniStorm().IsNightOrNightBlend() => Main.WeatherIcons.First(f => f.Name == "PartlyCloudy").Texture,
				WeatherStage.Clear when GameManager.GetUniStorm().IsNightOrNightBlend() => Main.WeatherIcons.First(f => f.Name == "Clear_Night").Texture,
				WeatherStage.Clear when !GameManager.GetUniStorm().IsNightOrNightBlend() => Main.WeatherIcons.First(f => f.Name == "Clear").Texture,
				WeatherStage.Cloudy => Main.WeatherIcons.First(f => f.Name == "Cloudy").Texture,
				WeatherStage.LightFog => Main.WeatherIcons.First(f => f.Name == "LightFog").Texture,
				WeatherStage.Blizzard => Main.WeatherIcons.First(f => f.Name == "Blizzard").Texture,
				WeatherStage.ClearAurora => Main.WeatherIcons.First(f => f.Name == "ClearAurora").Texture,					// Aurora Borealis
				WeatherStage.ToxicFog => Main.WeatherIcons.First(f => f.Name == "ToxicFog").Texture,						// Toxic Fog - only darkwalker challenge as of 2.22
				WeatherStage.ElectrostaticFog => Main.WeatherIcons.First(f => f.Name == "ElectrostaticFog").Texture,		// Glimmer Fog
				_ => null,
			};
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="stage"></param>
		/// <returns></returns>
		public static string? GetWeatherSpriteName(WeatherStage stage)
		{
			return stage switch
			{
				WeatherStage.DenseFog => "ico_journal",
				WeatherStage.LightSnow => "ico_MeltSnow",
				WeatherStage.HeavySnow => "ico_MeltSnow",
				WeatherStage.PartlyCloudy => "ico_journal",
				WeatherStage.Clear => "ico_journal",
				WeatherStage.Cloudy => "ico_journal",
				WeatherStage.LightFog => "ico_journal",
				WeatherStage.Blizzard => "ico_almanac_hurricane",
				WeatherStage.ClearAurora => "ico_Warning",       // Aurora Borealis
				WeatherStage.ToxicFog => "ico_toxicFog_L1",   // Toxic Fog - only darkwalker challenge as of 2.22
				WeatherStage.ElectrostaticFog => "ico_journal",       // Glimmer Fog
				WeatherStage.Undefined => null,
				_ => null,
			};
		}
		#region Aurora Specific
		/// <summary>
		/// 
		/// </summary>
		/// <param name="auroraManager"></param>
		/// <returns></returns>
		public static float GetAlteredAlpha(AuroraManager auroraManager)
		{
			if (GameManager.GetAuroraManager().GetNormalizedAlpha() >= 0.95f) return 1f;
			else return GameManager.GetAuroraManager().GetNormalizedAlpha();
		}
		/// <summary>
		/// Gets if the Aurora is either loading or dissipating
		/// </summary>
		/// <param name="auroraManager"></param>
		/// <returns></returns>
		public static bool AuroraChanging(AuroraManager auroraManager)
		{
			bool min = auroraManager.GetNormalizedAlpha() > 0.01f;
			bool max = auroraManager.GetNormalizedAlpha() < auroraManager.m_FullyActiveValue;
			return min && max;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="auroraManager"></param>
		/// <returns></returns>
		public static bool IsAuroraFullyActive(AuroraManager auroraManager)
		{
			return auroraManager.GetNormalizedAlpha() >= auroraManager.m_FullyActiveValue;
		}
		#endregion
	}
}
