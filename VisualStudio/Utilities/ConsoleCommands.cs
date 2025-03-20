namespace AuroraMonitor
{
	internal class ConsoleCommands
	{
		//private static void PrintSavedWeatherData()
		//{
		//	Main.Load();
		//	if (Main.MonitorData.PreviousStages.Count() == 0 ) return;
		//	ComplexLogger.Log( "Previous Weather:" );
		//	var stagecopy = Main.MonitorData.PreviousStages;
		//	stagecopy.Reverse();
		//	foreach ( WeatherStage stage in stagecopy )
		//	{
		//		ComplexLogger.Log( $"\t{stage}" );
		//	}
		//}
		/// <summary>
		/// Prints all debug info to the MelonLog
		/// </summary>
		internal static void PrintDebugInfo()
		{
			Weather weatherComponent                = GameManager.GetWeatherComponent();
			WeatherTransition weather               = GameManager.GetWeatherTransitionComponent();
			UniStormWeatherSystem uniStorm          = GameManager.GetUniStorm();
			AuroraManager auroraManager             = GameManager.GetAuroraManager();
			Wind wind                               = GameManager.GetWindComponent();
			TimeOfDay time                          = GameManager.GetTimeOfDayComponent();


			float alpha                             = auroraManager.GetNormalizedAlpha();
			bool cinematicColours                   = auroraManager.IsUsingCinematicColours();
			int numFramesNotActive                  = auroraManager.m_NumFramesNotActive;
			bool forcedAuroraNext                   = auroraManager.m_ForceAuroraNextOpportunity;
			int numAuroraSave                       = auroraManager.m_NumAurorasForSave;
			bool started                            = auroraManager.AuroraIsActive();
			bool fullystarted                       = auroraManager.m_IsElectrolizerActive;
			bool boosted                            = auroraManager.IsAuroraBoostEnabled();
			//int temperature                       = uniStorm.m_Temperature;

			float secondsSinceLastChange            = uniStorm.m_SecondsSinceLastWeatherChange;
			float daysSincLastChange                = secondsSinceLastChange / 86400;
			float hoursSinceLastChange              = secondsSinceLastChange / 1440;
			float minutesSinceLastChange            = secondsSinceLastChange / 60;

            Main.Logger.Log( "Time Information", FlaggedLoggingLevel.Always, LoggingSubType.IntraSeparator);

			Main.Logger.Log($"Current Day:                       {uniStorm.GetDayNumber()}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"Time of day:                      {time.GetHour()}h:{time.GetMinutes()}m", FlaggedLoggingLevel.Always);

            Main.Logger.Log("Weather Information", FlaggedLoggingLevel.Always, LoggingSubType.IntraSeparator);

			Main.Logger.Log($"Previous Weather:                  {Main.MonitorData!.Prev}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"UniStorm Previous Weather:         {uniStorm.m_PreviousWeatherStage}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"Current Weather:                   {uniStorm.GetWeatherStage()}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"GetCurrentWeatherLoc:              {WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm())}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"\tResult:                          {Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()))}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"GetCurrentWeatherIcon:             {WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())}", FlaggedLoggingLevel.Always);

            //PrintSavedWeatherData();

            Main.Logger.Log("Wind Information", FlaggedLoggingLevel.Always, LoggingSubType.IntraSeparator);

			Main.Logger.Log($"Wind Speed:                        {wind.GetSpeedMPH()}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"Wind angle:                        {wind.GetWindAngleRelativeToPlayer()}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"Wind Direction:                    {WeatherUtilities.GetWindDirection()}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"Wind Mult:                         {GameManager.GetPlayerMovementComponent().GetWindMovementMultiplier()}", FlaggedLoggingLevel.Always);

            Main.Logger.Log("Aurora Information", FlaggedLoggingLevel.Always, LoggingSubType.IntraSeparator);


			Main.Logger.Log($"Time since last Aurora:            {(int)daysSincLastChange}:{(int)hoursSinceLastChange}:{(int)minutesSinceLastChange}:{secondsSinceLastChange}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"Aurora Early Chance:               {GameManager.GetWeatherComponent().m_AuroraEarlyWindowProbability}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"Aurora Late Chance:                {GameManager.GetWeatherComponent().m_AuroraLateWindowProbability}", FlaggedLoggingLevel.Always);

			Main.Logger.Log($"cinematicColours:                  {cinematicColours}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"numFramesNotActive:                {numFramesNotActive}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"forcedAuroraNext:                  {forcedAuroraNext}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"numAuroraSave:                     {numAuroraSave}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"started:                           {started}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"fullystarted:                      {fullystarted}", FlaggedLoggingLevel.Always);
			Main.Logger.Log($"boosted:                           {boosted}", FlaggedLoggingLevel.Always);
			AuroraUtilities.FetchAuroraColour();

            Main.Logger.Log("Extra Debug Data", FlaggedLoggingLevel.Always, LoggingSubType.IntraSeparator);

			if (Main.Config != null)
			{
                Main.Logger.Log($"BaseLoaded:                        {Main.Config.BaseLoaded}", FlaggedLoggingLevel.Always);
                Main.Logger.Log($"SandboxLoaded:                     {Main.Config.SandboxLoaded}", FlaggedLoggingLevel.Always);
                Main.Logger.Log($"DLC01Loaded:                       {Main.Config.DLC01Loaded}", FlaggedLoggingLevel.Always);
                Main.Logger.Log($"Current Scene:                     {GameManager.m_ActiveScene}", FlaggedLoggingLevel.Always);
                Main.Logger.Log($"BaseSceneName:                     {Main.Config.BaseSceneName}", FlaggedLoggingLevel.Always);
                Main.Logger.Log($"SandboxSceneName:                  {Main.Config.SandboxSceneName}", FlaggedLoggingLevel.Always);
                Main.Logger.Log($"DLC01SceneName:                    {Main.Config.DLC01SceneName}", FlaggedLoggingLevel.Always);
            }

            Main.Logger.Log(FlaggedLoggingLevel.Always, LoggingSubType.Separator);

            //Main.Logger.Log(FlaggedLoggingLevel.Always, $"");
        }

  //      public static void ForceDisplay()
		//{
		//	WeatherNotifications.MaybeDisplayWeatherNotification();
		//}

		public static void FixAuroraSound()
		{
			GameManager.GetAuroraManager().UpdateAuroraAudio();
		}

		/// <summary>
		/// 
		/// </summary>
		internal static void RegisterCommands()
		{
			uConsole.RegisterCommand("FixAuroraSound", new Action(FixAuroraSound));
			uConsole.RegisterCommand("get_aurora_color", new Action( AuroraUtilities.FetchAuroraColour));
			uConsole.RegisterCommand("get_aurora_time", new Action( AuroraUtilities.FetchAuroraTime));
			uConsole.RegisterCommand("AuroraMonitorDebug", new Action(PrintDebugInfo));
			//uConsole.RegisterCommand("ForceDisplay", new Action(ForceDisplay));
		}

		/// <summary>
		/// 
		/// </summary>
		internal static void UnRegisterCommands()
		{
			uConsole.UnRegisterCommand("FixAuroraSound");
			uConsole.UnRegisterCommand("get_aurora_color");
			uConsole.UnRegisterCommand("get_aurora_time");
			uConsole.UnRegisterCommand("AuroraMonitorDebug");
            //uConsole.UnRegisterCommand("ForceDisplay");
        }
	}
}
