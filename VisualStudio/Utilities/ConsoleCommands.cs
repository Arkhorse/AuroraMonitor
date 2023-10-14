namespace AuroraMonitor
{
	internal class ConsoleCommands
	{
		//private static void PrintSavedWeatherData()
		//{
		//	Main.Load();
		//	if (Main.MonitorData.PreviousStages.Count() == 0 ) return;
		//	Logging.Log( "Previous Weather:" );
		//	var stagecopy = Main.MonitorData.PreviousStages;
		//	stagecopy.Reverse();
		//	foreach ( WeatherStage stage in stagecopy )
		//	{
		//		Logging.Log( $"\t{stage}" );
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

			Logging.LogSeperator();

			Logging.LogIntraSeparator( "Time Information" );

			Logging.Log($"Current Day:                       {uniStorm.GetDayNumber()}");
			Logging.Log( $"Time of day:                      {time.GetHour()}h:{time.GetMinutes()}m" );

			Logging.LogIntraSeparator("Weather Information");

			Logging.Log($"Previous Weather:                  {Main.MonitorData!.Prev}");
			Logging.Log($"UniStorm Previous Weather:         {uniStorm.m_PreviousWeatherStage}");
			Logging.Log($"Current Weather:                   {uniStorm.GetWeatherStage()}");
			Logging.Log($"GetCurrentWeatherLoc:              {WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm())}");
			Logging.Log($"\tResult:                          {Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()))}");
			Logging.Log($"GetCurrentWeatherIcon:             {WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())}");

			//PrintSavedWeatherData();

			Logging.LogIntraSeparator("Wind Information");

			Logging.Log($"Wind Speed:                        {wind.GetSpeedMPH()}");
			Logging.Log($"Wind angle:                        {wind.GetWindAngleRelativeToPlayer()}");
			Logging.Log($"Wind Direction:                    {WeatherUtilities.GetWindDirection()}");
			Logging.Log($"Wind Mult:                         {GameManager.GetPlayerMovementComponent().GetWindMovementMultiplier()}");

			Logging.LogIntraSeparator("Aurora Information");


			Logging.Log( $"Time since last Aurora:          {(int)daysSincLastChange}:{(int)hoursSinceLastChange}:{(int)minutesSinceLastChange}:{secondsSinceLastChange}" );
			Logging.Log($"Aurora Early Chance:               {GameManager.GetWeatherComponent().m_AuroraEarlyWindowProbability}");
			Logging.Log($"Aurora Late Chance:                {GameManager.GetWeatherComponent().m_AuroraLateWindowProbability}");

			Logging.Log($"cinematicColours:                  {cinematicColours}");
			Logging.Log($"numFramesNotActive:                {numFramesNotActive}");
			Logging.Log($"forcedAuroraNext:                  {forcedAuroraNext}");
			Logging.Log($"numAuroraSave:                     {numAuroraSave}");
			Logging.Log($"started:                           {started}");
			Logging.Log($"fullystarted:                      {fullystarted}");
			Logging.Log($"boosted:                           {boosted}");
			AuroraUtilities.FetchAuroraColour();

			Logging.LogIntraSeparator("Extra Debug Data");

			Logging.Log($"BaseLoaded:                        {Main.BaseLoaded}");
			Logging.Log($"SandboxLoaded:                     {Main.SandboxLoaded}");
			Logging.Log($"DLC01Loaded:                       {Main.DLC01Loaded}");
			Logging.Log($"Current Scene:                     {GameManager.m_ActiveScene}");
			Logging.Log($"BaseSceneName:                     {Main.BaseSceneName}");
			Logging.Log($"SandboxSceneName:                  {Main.SandboxSceneName}");
			Logging.Log($"DLC01SceneName:                    {Main.DLC01SceneName}");

			Logging.LogSeperator();

			//Logging.Log($"");
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