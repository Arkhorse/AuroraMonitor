namespace AuroraMonitor
{
    internal class Main : MelonMod
    {
        // This is used to init the mod. If you have no settings or other dependent mods, this method is not needed
        public static bool AuroraActive { get; set; }
        public Main Instance { get; set; }
        public override void OnInitializeMelon()
        {
            Instance = this;
            Settings.OnLoad();
            RegisterCommands();
#if DEBUG
            Logger.Log($"Mod has loaded with version: {BuildInfo.Version}");
#endif
        }

        // Needed because its always set to false on scene change
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName.Contains("SANDBOX"))
            {
                if (Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic)
                {
                    GameManager.GetAuroraManager().SetCinematicColours(Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic);
                }
            }
            base.OnSceneWasLoaded(buildIndex, sceneName);
        }

        /// <summary>
        /// Used to fetch aurora time. Currently not implemented
        /// </summary>
        internal static void FetchAuroraTime()
        {
            Logger.Log($"Aurora Time Left: {GameManager.GetAuroraManager().GetNormalizedAlpha()}");
        }

        /// <summary>
        /// Used to fetch aurora colour
        /// </summary>
        internal static void FetchAuroraColour()
        {
            Color AuroraColor = GameManager.GetAuroraManager().GetAuroraColour();
            Logger.Log($"Aurora Color: R:{AuroraColor.r} G:{AuroraColor.g} B:{AuroraColor.b} A:{AuroraColor.a}");
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

            Logger.LogSeperator();

            Logger.Log($"Current Day:                    {uniStorm.GetDayNumber()}");
            Logger.Log($"Current Weather:                {uniStorm.GetWeatherStage()}");
            Logger.Log($"Time Since Last Change:         {secondsSinceLastChange}");

            Logger.Log($"Aurora Early Chance:            {weatherComponent.m_AuroraEarlyWindowProbability}");
            Logger.Log($"Aurora Late Chance:             {weatherComponent.m_AuroraLateWindowProbability}");

            Logger.Log($"cinematicColours:               {cinematicColours}");
            Logger.Log($"numFramesNotActive:             {numFramesNotActive}");
            Logger.Log($"forcedAuroraNext:               {forcedAuroraNext}");
            Logger.Log($"numAuroraSave:                  {numAuroraSave}");
            Logger.Log($"started:                        {started}");
            Logger.Log($"fullystarted:                   {fullystarted}");
            Logger.Log($"boosted:                        {boosted}");
            Logger.Log($"AuroraActive:                   {Main.AuroraActive}");

            FetchAuroraColour();

            Logger.LogSeperator();

            //Logger.Log($"");
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