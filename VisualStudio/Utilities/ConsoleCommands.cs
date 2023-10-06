namespace AuroraMonitor
{
    internal class ConsoleCommands
    {
        /// <summary>
        /// Prints all debug info to the MelonLog
        /// </summary>
        internal static void PrintDebugInfo()
        {
            Il2Cpp.Weather weatherComponent         = GameManager.GetWeatherComponent();
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
            bool fullystarted                       = auroraManager.IsFullyActive();
            bool boosted                            = auroraManager.IsAuroraBoostEnabled();
            //int temperature                       = uniStorm.m_Temperature;

            float secondsSinceLastChange            = uniStorm.m_SecondsSinceLastWeatherChange;

			Logging.LogSeperator();

			Logging.LogIntraSeparator( "Time Information" );

			Logging.Log($"Current Day:                       {uniStorm.GetDayNumber()}");
			Logging.Log( $"Time of day:                      {time.GetHour()}h:{time.GetMinutes()}m" );

			Logging.LogIntraSeparator("Weather Information");

			Logging.Log($"Previous Weather:                  {Main.MonitorData.Prev}");
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
        }

        public static void TriggerMessage()
        {
            WeatherNotifications.MaybeDisplayWeatherNotification(true);
        }

        public static void ForceUpdateNotifications()
        {
			WeatherNotifications.MaybeDisplayWeatherNotification(true);
            uConsole.Log("Triggered update");
        }

        /// <summary>
        /// 
        /// </summary>
        internal static void RegisterCommands()
        {
            uConsole.RegisterCommand("get_aurora_color", new Action( AuroraUtilities.FetchAuroraColour));
            uConsole.RegisterCommand("get_aurora_time", new Action( AuroraUtilities.FetchAuroraTime));
            uConsole.RegisterCommand("AuroraMonitorDebug", new Action(PrintDebugInfo));
            uConsole.RegisterCommand("TriggerMessage", new Action(TriggerMessage));
            uConsole.RegisterCommand("ForceUpdateNotifications", new Action(ForceUpdateNotifications));

        }

        /// <summary>
        /// 
        /// </summary>
        internal static void UnRegisterCommands()
        {
            uConsole.UnRegisterCommand("get_aurora_color");
            uConsole.UnRegisterCommand("get_aurora_time");
            uConsole.UnRegisterCommand("AuroraMonitorDebug");
        }
    }
}