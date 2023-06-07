namespace AuroraMonitor
{
    internal class AuroraMonitor : MelonMod
    {
        // This is used to init the mod. If you have no settings or other dependent mods, this method is not needed
        public static bool AuroraActive;
        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
            RegisterCommands();
#if DEBUG
            Utilities.Log($"Mod has loaded with version: {BuildInfo.Version}");
#endif
        }

        // Needed because its always set to false on scene change
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName.Contains("SANDBOX"))
            {
                GameManager.GetAuroraManager().SetCinematicColours(Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic);
            }
            base.OnSceneWasLoaded(buildIndex, sceneName);
        }

        /// <summary>
        /// Used to fetch aurora time. Currently not implemented
        /// </summary>
        internal static void FetchAuroraTime()
        {
            Utilities.Log($"Aurora Time Left: {GameManager.GetAuroraManager().GetNormalizedAlpha()}");
        }

        /// <summary>
        /// Used to fetch aurora colour
        /// </summary>
        internal static void FetchAuroraColour()
        {
            Color AuroraColor = GameManager.GetAuroraManager().GetAuroraColour();
            Utilities.Log($"Aurora Color: R:{AuroraColor.r} G:{AuroraColor.g} B:{AuroraColor.b} A:{AuroraColor.a}");
        }
        
        /// <summary>
        /// Prints all debug info to the MelonLog
        /// </summary>
        internal static void PrintDebugInfo()
        {
            Weather weatherComponent        = GameManager.GetWeatherComponent();
            WeatherTransition weather       = GameManager.GetWeatherTransitionComponent();
            UniStormWeatherSystem uniStorm  = GameManager.GetUniStorm();
            AuroraManager auroraManager     = GameManager.GetAuroraManager();

            float alpha                     = auroraManager.GetNormalizedAlpha();
            bool cinematicColours           = auroraManager.IsUsingCinematicColours();
            int numFramesNotActive          = auroraManager.m_NumFramesNotActive;
            bool forcedAuroraNext           = auroraManager.m_ForceAuroraNextOpportunity;
            int numAuroraSave               = auroraManager.m_NumAurorasForSave;
            bool started                    = auroraManager.AuroraIsActive();
            bool fullystarted               = auroraManager.IsFullyActive();
            bool boosted                    = auroraManager.IsAuroraBoostEnabled();

            float secondsSinceLastChange    = uniStorm.m_SecondsSinceLastWeatherChange;

            Utilities.LogSeperator();

            Utilities.Log($"Current Day:                    {uniStorm.GetDayNumber()}");
            Utilities.Log($"Current Weather:                {uniStorm.GetWeatherStage()}");
            Utilities.Log($"Time Since Last Change:         {secondsSinceLastChange}");

            Utilities.Log($"Aurora Early Chance:            {weatherComponent.m_AuroraEarlyWindowProbability}");
            Utilities.Log($"Aurora Late Chance:             {weatherComponent.m_AuroraLateWindowProbability}");

            Utilities.Log($"cinematicColours:               {cinematicColours}");
            Utilities.Log($"numFramesNotActive:             {numFramesNotActive}");
            Utilities.Log($"forcedAuroraNext:               {forcedAuroraNext}");
            Utilities.Log($"numAuroraSave:                  {numAuroraSave}");
            Utilities.Log($"started:                        {started}");
            Utilities.Log($"fullystarted:                   {fullystarted}");
            Utilities.Log($"boosted:                        {boosted}");
            Utilities.Log($"AuroraActive:                   {AuroraMonitor.AuroraActive}");

            FetchAuroraColour();

            Utilities.LogSeperator();

            //Utilities.Log($"");
        }

        internal static void ForceUpdate()
        {
            AuroraActive = !AuroraActive;
        }

        private void RegisterCommands()
        {
            uConsole.RegisterCommand("get_aurora_color",            new Action(FetchAuroraColour));
            uConsole.RegisterCommand("get_aurora_time",             new Action(FetchAuroraTime));
            uConsole.RegisterCommand("AuroraMonitorDebug",          new Action(PrintDebugInfo));
            uConsole.RegisterCommand("AuroraMonitorForceUpdate",    new Action(ForceUpdate));
        }
    }
}