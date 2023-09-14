using AuroraMonitor.Utilities;

namespace AuroraMonitor
{
    internal class ConsoleCommands
    {
        /// <summary>
        /// Prints all debug info to the MelonLog
        /// </summary>
        internal static void PrintDebugInfo()
        {
            Il2Cpp.Weather weatherComponent        = GameManager.GetWeatherComponent();
            WeatherTransition weather       = GameManager.GetWeatherTransitionComponent();
            UniStormWeatherSystem uniStorm  = GameManager.GetUniStorm();
            AuroraManager auroraManager     = GameManager.GetAuroraManager();
            Wind wind                       = GameManager.GetWindComponent();

            float alpha                     = auroraManager.GetNormalizedAlpha();
            bool cinematicColours           = auroraManager.IsUsingCinematicColours();
            int numFramesNotActive          = auroraManager.m_NumFramesNotActive;
            bool forcedAuroraNext           = auroraManager.m_ForceAuroraNextOpportunity;
            int numAuroraSave               = auroraManager.m_NumAurorasForSave;
            bool started                    = auroraManager.AuroraIsActive();
            bool fullystarted               = auroraManager.IsFullyActive();
            bool boosted                    = auroraManager.IsAuroraBoostEnabled();
            //int temperature                 = uniStorm.m_Temperature;

            float secondsSinceLastChange    = uniStorm.m_SecondsSinceLastWeatherChange;

            Logger.LogSeperator();

            Logger.Log($"Current Day:                       {uniStorm.GetDayNumber()}");

            Logger.LogIntraSeparator("Weather Information");

            Logger.Log($"Previous Weather:                  {WeatherUtilities.Prev}");
            Logger.Log($"UniStorm Previous Weather:         {uniStorm.m_PreviousWeatherStage}");
            Logger.Log($"Current Weather:                   {uniStorm.GetWeatherStage()}");
            Logger.Log($"GetCurrentWeatherLoc:              {WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm())}");
            Logger.Log($"   Result:                         {Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()))}");
            Logger.Log($"GetCurrentWeatherIcon:             {WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())}");

            Logger.LogIntraSeparator("Wind Information");

            Logger.Log($"Wind Speed:                        {wind.GetSpeedMPH()}");
            Logger.Log($"Wind angle:                        {wind.GetWindAngleRelativeToPlayer()}");
            Logger.Log($"Wind Direction:                    {WeatherUtilities.GetWindDirection()}");
            Logger.Log($"Wind Mult:                         {GameManager.GetPlayerMovementComponent().GetWindMovementMultiplier()}");

            Logger.LogIntraSeparator("Aurora Information");

            Logger.Log($"Time Since Last Change:            {secondsSinceLastChange}s");
            Logger.Log($"Aurora Early Chance:               {weatherComponent.m_AuroraEarlyWindowProbability}");
            Logger.Log($"Aurora Late Chance:                {weatherComponent.m_AuroraLateWindowProbability}");

            Logger.Log($"cinematicColours:                  {cinematicColours}");
            Logger.Log($"numFramesNotActive:                {numFramesNotActive}");
            Logger.Log($"forcedAuroraNext:                  {forcedAuroraNext}");
            Logger.Log($"numAuroraSave:                     {numAuroraSave}");
            Logger.Log($"started:                           {started}");
            Logger.Log($"fullystarted:                      {fullystarted}");
            Logger.Log($"boosted:                           {boosted}");
            Utilities.Utilities.FetchAuroraColour();

            Logger.LogIntraSeparator("Extra Debug Data");

            Logger.Log($"BaseLoaded:                        {Main.BaseLoaded}");
            Logger.Log($"SandboxLoaded:                     {Main.SandboxLoaded}");
            Logger.Log($"DLC01Loaded:                       {Main.DLC01Loaded}");
            Logger.Log($"Current Scene:                     {GameManager.m_ActiveScene}");
            Logger.Log($"BaseSceneName:                     {Main.BaseSceneName}");
            Logger.Log($"SandboxSceneName:                  {Main.SandboxSceneName}");
            Logger.Log($"DLC01SceneName:                    {Main.DLC01SceneName}");

            Logger.LogSeperator();

            //Logger.Log($"");
        }

        public static void TriggerMessage()
        {
            Main.MaybeDisplayWeatherNotification(true);
        }

        public static void ForceUpdateNotifications()
        {
            Main.MaybeDisplayWeatherNotification(true);
            uConsole.Log("Triggered update");
        }

        /// <summary>
        /// 
        /// </summary>
        internal static void RegisterCommands()
        {
            uConsole.RegisterCommand("get_aurora_color", new Action(Utilities.Utilities.FetchAuroraColour));
            uConsole.RegisterCommand("get_aurora_time", new Action(Utilities.Utilities.FetchAuroraTime));
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
