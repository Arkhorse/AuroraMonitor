namespace AuroraMonitor
{
    internal class Main : MelonMod
    {
        // This is used to init the mod. If you have no settings or other dependent mods, this method is not needed
        public static bool AuroraActive;
        public override void OnInitializeMelon()
=======
        public static bool AuroraActive { get; set; }
        public Main? Instance { get; set; }
        public WeatherStage SavedStage { get; set; }
        public static string? GetCurrentWeatherLoc { get; } = GameManager.GetWeatherComponent().GetWeatherStage() switch
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

    public override void OnInitializeMelon()
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                GameManager.GetAuroraManager().SetCinematicColours(Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic);
=======
                if (Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic)
                {
                    GameManager.GetAuroraManager().SetCinematicColours(Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic);
                }

                if (GameManager.GetUniStorm())
                {
                    ShowWeatherAlert();
                    //SavedStage = GameManager.GetUniStorm().GetWeatherStage();
                }
>>>>>>> Stashed changes
            }
            base.OnSceneWasLoaded(buildIndex, sceneName);
        }

        //public override void OnLateUpdate()
        //{
        //    base.OnLateUpdate();

        //    if (SavedStage != GameManager.GetUniStorm().GetWeatherStage())
        //    {
        //        if (GameManager.GetVpFPSPlayer() && !InterfaceManager.IsOverlayActiveCached())
        //        {
        //            Utilities.AuroraMonitorMessage($"Weather Update: {Localization.Get(GetCurrentWeatherLoc)}", Settings.Instance.WeatherStageNotificationTime);

        //            SavedStage = GameManager.GetUniStorm().GetWeatherStage();
        //        }
        //    }
        //}

        public static void ShowNewWeatherAlert()
        {
            Utilities.AuroraMonitorMessage($"Weather Update: {Localization.Get(GetCurrentWeatherLoc)}", Settings.Instance.WeatherStageNotificationTime);
        }

        public static void ShowWeatherAlert()
        {
            Utilities.AuroraMonitorMessage($"Weather: {Localization.Get(GetCurrentWeatherLoc)}", Settings.Instance.WeatherStageNotificationTime);
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
            Wind wind                       = GameManager.GetWindComponent();

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

            Utilities.Log($"Wind Speed: {wind.GetSpeedMPH}");

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