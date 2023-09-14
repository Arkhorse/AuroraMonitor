global using AuroraMonitor.Utilities;
global using AuroraMonitor.Utilities.Enums;
global using AuroraMonitor.WeatherSets;

namespace AuroraMonitor
{
    public class Main : MelonMod
    {
        public static bool BaseLoaded { get; set; }                     = false;
        public static bool SandboxLoaded { get; set; }                  = false;
        public static bool DLC01Loaded { get; set; }                    = false;
        public static string BaseSceneName { get; set; }                = string.Empty;
        public static string SandboxSceneName { get; set; }             = string.Empty;
        public static string DLC01SceneName { get; set; }               = string.Empty;
        public static bool ModInitiliated { get; set; }                 = false;
        private static BetterAurora BetterAurora { get; set; }          = new();
        private static bool BetterAuroraActive { get; set; }            = false;

        public override void OnInitializeMelon()
        {
            BetterAurora.enabled = false;
            Settings.OnLoad();
            AuroraSettings.OnLoad();
            WeatherSettings.OnLoad();
            ConsoleCommands.RegisterCommands();

            if (Settings.Instance.PRINTDEBUGLOG)
            {
                Logger.Log($"Mod has loaded with version: {BuildInfo.Version}");
            }
        }

        // Using this actually works 100% of the time. OnSceneWasLoaded and OnSceneWasUnloaded is Additative scene loads ONLY
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);

            bool RegionOrZone = sceneName.Contains("Region", StringComparison.InvariantCultureIgnoreCase) || sceneName.Contains("Zone", StringComparison.InvariantCultureIgnoreCase);
            bool IsAdditiveScene = sceneName.Contains("SANDBOX", StringComparison.InvariantCultureIgnoreCase) || sceneName.EndsWith("DARKWALKER", StringComparison.InvariantCultureIgnoreCase) || sceneName.EndsWith("DLC01", StringComparison.InvariantCultureIgnoreCase);

            // only needed if you want to know what the scene name is. The buildIndex is always -1 or 0
            if (Settings.Instance.PRINTDEBUGLOG) Logger.Log($"Scene Initialized, name: {sceneName}, index: {buildIndex}");

            // prevents a null ref error
            if (sceneName == "Empty" || sceneName == "Boot" || sceneName.StartsWith("MainMenu", StringComparison.InvariantCultureIgnoreCase)) return;

            if (RegionOrZone && !IsAdditiveScene)
            {
                BaseLoaded = true;
                BaseSceneName = sceneName;

                if (WeatherSettings.Instance.WeatherNotificationsSceneChange) MaybeDisplayWeatherNotification();
            }
            else if (sceneName.EndsWith("SANDBOX", StringComparison.InvariantCultureIgnoreCase))
            {
                SandboxLoaded = true;
                SandboxSceneName = sceneName;
            }
            else if (sceneName.EndsWith("DLC01", StringComparison.InvariantCultureIgnoreCase))
            {
                DLC01Loaded = true;
                DLC01SceneName = sceneName;
            }

            //SceneHandle.OnSceneWasLoaded(sceneName);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            try
            {
                string currentScene = GameManager.m_ActiveScene;

                if (currentScene == "Empty" || currentScene == "Boot" || currentScene.StartsWith("MainMenu", StringComparison.InvariantCultureIgnoreCase)) return;
            }
            catch { }

            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, WeatherSettings.Instance.DisplayNotification))
            {
                MaybeDisplayWeatherNotification(true);
            }
        }
        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            if (GameManager.GetWeatherComponent() && !ModInitiliated)
            {
                if (AuroraSettings.Instance.AuroraChanceRemember)
                {
                    Aurora.SetAuroraChances(AuroraSettings.Instance.AuroraChanceEarly, AuroraSettings.Instance.AuroraChanceLate);
                }
                else if (!AuroraSettings.Instance.AuroraChanceRemember)
                {
                    AuroraSettings.Instance.AuroraChanceEarly = GameManager.GetWeatherComponent().m_AuroraEarlyWindowProbability;
                    AuroraSettings.Instance.AuroraChanceLate = GameManager.GetWeatherComponent().m_AuroraLateWindowProbability;
                }

                ModInitiliated = true;
            }

            if (GameManager.GetWindComponent())
            {
                if (AuroraSettings.Instance.EnableBetterAurora && !BetterAuroraActive)
                {
                    BetterAurora.Activate();
                    BetterAuroraActive = true;
                }
            }

            try
            {
                string currentScene = GameManager.m_ActiveScene;

                if (currentScene == "Empty" || currentScene == "Boot" || currentScene.StartsWith("MainMenu", StringComparison.InvariantCultureIgnoreCase)) return;
            }
            catch { }

            try
            {
                if (!GameManager.GetUniStorm()) return;
            }
            catch { }

            try
            {
                MaybeDisplayWeatherNotification();
            }
            catch { }
        }

        public static void MaybeDisplayWeatherNotification(bool force = false)
        {
            if (GameManager.GetPlayerManagerComponent() == null) return;
            if (GameManager.GetPlayerManagerComponent().m_ControlMode == PlayerControlMode.Locked) return;

            if (WeatherUtilities.IsValidSceneForWeather(GameManager.m_ActiveScene) || force)
            {
                if (WeatherUtilities.Prev != GameManager.GetUniStorm().m_CurrentWeatherStage || force)
                {
                    if (WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()) is null) return;
                    if (WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm()) is null) return;

                    GearMessageUtilities.AddGearMessage(
                        WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())!,
                        "Weather Monitor",
                        $"Weather: {Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()))}",
                        WeatherSettings.Instance.WeatherNotificationsTime);

                    WeatherUtilities.UpdateStages(GameManager.GetUniStorm().m_CurrentWeatherStage);
                }
            }
        }
    }
}