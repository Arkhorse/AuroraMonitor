global using AuroraUtilities = AuroraMonitor.Aurora.AuroraUtilities;
global using WeatherNotifications = AuroraMonitor.Notifications.WeatherNotifications;
global using WeatherUtilities = AuroraMonitor.Utilities.WeatherUtilities;

using MelonLoader.Utils;

namespace AuroraMonitor
{
    internal class Main : MelonMod
    {
        public static string MonitorFolder                      = Path.Combine(MelonEnvironment.ModsDirectory, "Monitor");
        public static bool BaseLoaded { get; set; }             = false;
        public static bool SandboxLoaded { get; set; }          = false;
        public static bool DLC01Loaded { get; set; }            = false;
        public static string BaseSceneName { get; set; }        = string.Empty;
        public static string SandboxSceneName { get; set; }     = string.Empty;
        public static string DLC01SceneName { get; set; }       = string.Empty;
        public static bool ModInitiliated { get; set; }         = false;

        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
            ConsoleCommands.RegisterCommands();
            if (Settings.Instance.PRINTDEBUGLOG)
            {
                Logging.LogStarter();
            }
        }

        // Using this actually works 100% of the time. OnSceneWasLoaded and OnSceneWasUnloaded is Additative scene loads ONLY
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);
			if ( sceneName == "Empty" || sceneName == "Boot" || sceneName.StartsWith( "MainMenu", StringComparison.InvariantCultureIgnoreCase ) ) return;

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

                if (Settings.Instance.WeatherNotificationsSceneChange) WeatherNotifications.MaybeDisplayWeatherNotification();
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

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            if (GameManager.GetWeatherComponent() && !ModInitiliated)
            {
                if (Settings.Instance.AuroraChanceRemember)
                {
                    AuroraUtilities.SetAuroraChances(Settings.Instance.AuroraChanceEarly, Settings.Instance.AuroraChanceLate);
                }
                else if (!Settings.Instance.AuroraChanceRemember)
                {
                    Settings.Instance.AuroraChanceEarly = GameManager.GetWeatherComponent().m_AuroraEarlyWindowProbability;
                    Settings.Instance.AuroraChanceLate = GameManager.GetWeatherComponent().m_AuroraLateWindowProbability;
                }

                ModInitiliated = true;
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
				WeatherNotifications.MaybeDisplayWeatherNotification();
            }
            catch { }
        }
    }
}