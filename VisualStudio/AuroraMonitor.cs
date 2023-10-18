global using AuroraUtilities = AuroraMonitor.Utilities.Aurora.AuroraUtilities;
global using WeatherNotifications = AuroraMonitor.Notifications.WeatherNotifications;
global using WeatherUtilities = AuroraMonitor.Utilities.WeatherUtilities;
global using AuroraMonitor.Utilities;
global using AuroraMonitor.Utilities.Enums;

using AuroraMonitor.Notifications;
using MelonLoader.Utils;
using AuroraMonitor.JSON;
using UnityEngine;

namespace AuroraMonitor
{
    internal class Main : MelonMod
    {
        public static string MonitorFolder { get; } = Path.Combine(MelonEnvironment.ModsDirectory, "Monitor");
        public static string MonitorMainConfig { get; } = Path.Combine(MonitorFolder, "main.json");
        public static bool BaseLoaded { get; set; } = false;
        public static bool SandboxLoaded { get; set; } = false;
        public static bool DLC01Loaded { get; set; } = false;
        public static string BaseSceneName { get; set; } = string.Empty;
        public static string SandboxSceneName { get; set; } = string.Empty;
        public static string DLC01SceneName { get; set; } = string.Empty;
        public static bool ModInitiliated { get; set; } = false;
        public static WeatherMonitorData? MonitorData { get; set; } = new();

        public static AssetBundle? FirstAidAddons { get; set; }
        public static string Panel_FirstAid_AddonsBundlePath { get; } = "AuroraMonitor.Resources.panel_firstaid_addons";

        public override void OnInitializeMelon()
        {
            if (!Directory.Exists(MonitorFolder))
            {
                Directory.CreateDirectory(MonitorFolder);
            }
            if (!File.Exists(MonitorMainConfig))
            {
                JsonFile.Save<WeatherMonitorData>(MonitorMainConfig, MonitorData);
            }

            MonitorData = JsonFile.Load<WeatherMonitorData>(MonitorMainConfig);
            //FirstAidAddons = LoadAssetBundle(Panel_FirstAid_AddonsBundlePath);

            Settings.OnLoad();
            ConsoleCommands.RegisterCommands();
            if (Settings.Instance.PRINTDEBUGLOG)
            {
                Logging.LogStarter();
            }
        }

        public static AssetBundle LoadAssetBundle(string name)
        {
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            using (MemoryStream? memory = new())
            {
                stream!.CopyTo(memory);
                return AssetBundle.LoadFromMemory(memory.ToArray());
            };
        }

        // Using this actually works 100% of the time. OnSceneWasLoaded and OnSceneWasUnloaded is Additative scene loads ONLY
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);
            if (sceneName == "Empty" || sceneName == "Boot" ) return;

            if (sceneName.StartsWith("MainMenu", StringComparison.InvariantCultureIgnoreCase))
            {
                Settings.Instance.OnLoadConfirm();
                return;
            }

            // only needed if you want to know what the scene name is. The buildIndex is always -1 or 0
            //if (Settings.Instance.PRINTDEBUGLOG) Logging.Log($"Scene Initialized, name: {sceneName}, index: {buildIndex}");


            if (SceneUtilities.IsSceneBase(sceneName) && !SceneUtilities.IsSceneAdditive(sceneName))
            {
                Settings.Instance.OnLoadConfirm();
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
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();
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