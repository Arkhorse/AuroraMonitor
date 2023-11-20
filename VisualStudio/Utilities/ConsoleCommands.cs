using AuroraMonitor.Utilities.Logger;

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

			Main.Logger.WriteSeperator();

            Main.Logger.WriteIntraSeparator( "Time Information" );

			Main.Logger.Log(FlaggedLoggingLevel.None, $"Current Day:                       {uniStorm.GetDayNumber()}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"Time of day:                      {time.GetHour()}h:{time.GetMinutes()}m" );

            Main.Logger.WriteIntraSeparator("Weather Information");

			Main.Logger.Log(FlaggedLoggingLevel.None, $"Previous Weather:                  {Main.MonitorData!.Prev}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"UniStorm Previous Weather:         {uniStorm.m_PreviousWeatherStage}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"Current Weather:                   {uniStorm.GetWeatherStage()}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"GetCurrentWeatherLoc:              {WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm())}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"\tResult:                          {Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()))}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"GetCurrentWeatherIcon:             {WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())}");

            //PrintSavedWeatherData();

            Main.Logger.WriteIntraSeparator("Wind Information");

			Main.Logger.Log(FlaggedLoggingLevel.None, $"Wind Speed:                        {wind.GetSpeedMPH()}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"Wind angle:                        {wind.GetWindAngleRelativeToPlayer()}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"Wind Direction:                    {WeatherUtilities.GetWindDirection()}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"Wind Mult:                         {GameManager.GetPlayerMovementComponent().GetWindMovementMultiplier()}");

            Main.Logger.WriteIntraSeparator("Aurora Information");


			Main.Logger.Log(FlaggedLoggingLevel.None, $"Time since last Aurora:            {(int)daysSincLastChange}:{(int)hoursSinceLastChange}:{(int)minutesSinceLastChange}:{secondsSinceLastChange}" );
			Main.Logger.Log(FlaggedLoggingLevel.None, $"Aurora Early Chance:               {GameManager.GetWeatherComponent().m_AuroraEarlyWindowProbability}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"Aurora Late Chance:                {GameManager.GetWeatherComponent().m_AuroraLateWindowProbability}");

			Main.Logger.Log(FlaggedLoggingLevel.None, $"cinematicColours:                  {cinematicColours}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"numFramesNotActive:                {numFramesNotActive}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"forcedAuroraNext:                  {forcedAuroraNext}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"numAuroraSave:                     {numAuroraSave}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"started:                           {started}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"fullystarted:                      {fullystarted}");
			Main.Logger.Log(FlaggedLoggingLevel.None, $"boosted:                           {boosted}");
			AuroraUtilities.FetchAuroraColour();

            Main.Logger.WriteIntraSeparator("Extra Debug Data");

			if (Main.Config != null)
			{
                Main.Logger.Log(FlaggedLoggingLevel.None, $"BaseLoaded:                        {Main.Config.BaseLoaded}");
                Main.Logger.Log(FlaggedLoggingLevel.None, $"SandboxLoaded:                     {Main.Config.SandboxLoaded}");
                Main.Logger.Log(FlaggedLoggingLevel.None, $"DLC01Loaded:                       {Main.Config.DLC01Loaded}");
                Main.Logger.Log(FlaggedLoggingLevel.None, $"Current Scene:                     {GameManager.m_ActiveScene}");
                Main.Logger.Log(FlaggedLoggingLevel.None, $"BaseSceneName:                     {Main.Config.BaseSceneName}");
                Main.Logger.Log(FlaggedLoggingLevel.None, $"SandboxSceneName:                  {Main.Config.SandboxSceneName}");
                Main.Logger.Log(FlaggedLoggingLevel.None, $"DLC01SceneName:                    {Main.Config.DLC01SceneName}");
            }

            Main.Logger.WriteSeperator();

            //Main.Logger.Log(FlaggedLoggingLevel.None, $"");
        }

        public static void ForceDisplay()
		{
			WeatherNotifications.MaybeDisplayWeatherNotification();
		}

		/// <summary>
		/// 
		/// </summary>
		internal static void RegisterCommands()
		{
			uConsole.RegisterCommand("get_aurora_color", new Action( AuroraUtilities.FetchAuroraColour));
			uConsole.RegisterCommand("get_aurora_time", new Action( AuroraUtilities.FetchAuroraTime));
			uConsole.RegisterCommand("AuroraMonitorDebug", new Action(PrintDebugInfo));
			uConsole.RegisterCommand("ForceDisplay", new Action(ForceDisplay));
		}

		/// <summary>
		/// 
		/// </summary>
		internal static void UnRegisterCommands()
		{
			uConsole.UnRegisterCommand("get_aurora_color");
			uConsole.UnRegisterCommand("get_aurora_time");
			uConsole.UnRegisterCommand("AuroraMonitorDebug");
            uConsole.UnRegisterCommand("ForceDisplay");
        }
	}
}