global using AuroraMonitor.Utilities.Aurora;
global using AuroraMonitor.Data;
global using AuroraMonitor.GUI;
global using AuroraMonitor.GUI.Addons;
global using AuroraMonitor.JSON;
global using AuroraMonitor.Notifications;
global using AuroraMonitor.Patches;
global using AuroraMonitor.Utilities;
global using AuroraMonitor.Utilities.Enums;
global using AuroraMonitor.Utilities.Exceptions;
global using MelonLoader.Utils;
using AuroraMonitor.ModSettings;
using AuroraMonitor.Utilities.Logger;

namespace AuroraMonitor
{
    internal class Main : MelonMod
    {
        public static string MonitorFolder { get; } = Path.Combine(MelonEnvironment.ModsDirectory, "Monitor");
        public static string MonitorMainConfig { get; } = Path.Combine(MonitorFolder, "main.json");
        public static string Panel_FirstAid_AddonsBundlePath { get; } = "AuroraMonitor.Resources.panel_firstaid_addons";

        public static int GridCellHeight { get; } = 33;

        #region Class Instances
        public static MainConfig? Config { get; set; }
        public static WeatherMonitorData? MonitorData { get; set; }
        public static Settings SettingsInstance { get; set; } = new();
        public static ComplexLogger Logger { get; } = new();
        #endregion

        public override void OnInitializeMelon()
        {
            Setup.Init();

            Settings.OnLoad();
            ConsoleCommands.RegisterCommands();

            Logger.Log(FlaggedLoggingLevel.Verbose, $"Mod loaded with v{BuildInfo.Version}");
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

            if (Config == null) return;

            if (SceneUtilities.IsSceneEmpty(sceneName) || SceneUtilities.IsSceneBoot(sceneName)) return;

            if (SceneUtilities.IsSceneMenu(sceneName))
            {
                SettingsInstance.OnLoadConfirm();
                return;
            }

            if (SceneUtilities.IsSceneBase(sceneName) && !SceneUtilities.IsSceneAdditive(sceneName))
            {
                SettingsInstance.OnLoadConfirm();
                Config.BaseLoaded = true;
                Config.BaseSceneName = sceneName;

                if (SettingsInstance.WeatherNotificationsSceneChange) WeatherNotifications.MaybeDisplayWeatherNotification();
            }
            else if (SceneUtilities.IsSceneSandbox(sceneName))
            {
                Config.SandboxLoaded = true;
                Config.SandboxSceneName = sceneName;
            }
            else if (SceneUtilities.IsSceneDLC01(sceneName))
            {
                Config.DLC01Loaded = true;
                Config.DLC01SceneName = sceneName;
            }
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();
            try
            {
                if (!SceneUtilities.IsScenePlayable()) return;
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

        /// <summary>
        /// Checks if the player is currently involved in anything that would make modded actions unwanted
        /// </summary>
        /// <param name="PlayerManagerComponent">The current instance of the PlayerManager component, use <see cref="GameManager.GetPlayerManagerComponent()"/></param>
        /// <returns><c>true</c> if the player isnt dead, in a conversation, locked or in a cinematic</returns>
        public static bool IsPlayerAvailable(PlayerManager PlayerManagerComponent)
        {
            if (PlayerManagerComponent == null) return false;

            bool first      = PlayerManagerComponent.m_ControlMode == PlayerControlMode.Dead;
            bool second     = PlayerManagerComponent.m_ControlMode == PlayerControlMode.InConversation;
            bool third      = PlayerManagerComponent.m_ControlMode == PlayerControlMode.Locked;
            bool fourth     = PlayerManagerComponent.m_ControlMode == PlayerControlMode.InFPCinematic;

            return first && second && third && fourth;
        }
    }
}