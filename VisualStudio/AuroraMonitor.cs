global using AuroraMonitor.Utilities.Aurora;
global using AuroraMonitor.Data;
global using AuroraMonitor.GUI;
global using AuroraMonitor.GUI.Addons;
global using AuroraMonitor.Utilities.JSON;
global using AuroraMonitor.Notifications;
global using AuroraMonitor.Patches;
global using AuroraMonitor.Utilities;
global using AuroraMonitor.Utilities.Enums;
global using AuroraMonitor.Utilities.Exceptions;
global using MelonLoader.Utils;
global using ComplexLogger;
using AuroraMonitor.ModSettings;
using UnityEngine.UIElements;

namespace AuroraMonitor
{
	internal class Main : MelonMod
	{
		public static string MonitorFolder { get; } = Path.Combine(MelonEnvironment.ModsDirectory, "Monitor");
		public static string MonitorMainConfig { get; } = Path.Combine(MonitorFolder, "main.json");
		public static string MonitorConfig { get; } = Path.Combine(MonitorFolder, "config.json");
		public static string WeatherTrackingFile { get; } = Path.Combine(MonitorFolder, "WeatherDataTracking.json");
		public static string Progressbar_AddonsBundlePath { get; } = "AuroraMonitor.Resources.progressbar";

		public static int GridCellHeight { get; } = 33;

		#region Class Instances
		public static WeatherDataTracking? WeatherDataTracking { get; set; }
		public static MainConfig? Config { get; set; }
		public static WeatherMonitorData? MonitorData { get; set; }
		public static ModSettings.Settings SettingsInstance { get; set; } = new();
		public static ComplexLogger<Main> Logger { get; } = new();
		#endregion

		#region CurrentWeatherDisplay
		public static GameObject? CurrentWeather { get; set; }
		
		#endregion
		#region Weather Icons
		internal static List<string> WeatherIconNames =
		[
			"DenseFog",
			"LightSnow",
			"HeavySnow",
			"PartlyCloudy",
			"PartlyCloudy_Night",
			"Clear",
			"Clear_Night",
			"Cloudy",
			"LightFog",
			"Blizzard",
			"ClearAurora",
			"ToxicFog",
			"ElectrostaticFog"
		];
		internal static List<TextureDefinition> WeatherIcons = [];
		#endregion

		//public static List<WeatherAPI> RegisteredWeatherAPIs { get; } = new();

		public async override void OnInitializeMelon()
		{
			if (await Setup.Init())
			{
				ModSettings.Settings.OnLoad();
				ConsoleCommands.RegisterCommands();
			}
			else
			{
				Logger.Log($"Attempting to setup mod failed", FlaggedLoggingLevel.Critical);
			}
		}

		public static AssetBundle LoadAssetBundle(string name)
		{
			using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
			{
				MemoryStream? memory = new((int)stream.Length);
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

				//if (RegisteredWeatherAPIs.Count == 0 && GameManager.GetWeatherTransitionComponent().m_UnmanagedWeatherStage == WeatherStage.Undefined)
				//{
				//	Logger.Log(FlaggedLoggingLevel.Warning, "Found no registered weather API users and the unmanaged weather state is set. Reverting to default weather");

				//	GameManager.GetWeatherTransitionComponent().ActivateDefaultWeatherSet();
				//}

				//if (SettingsInstance.WeatherNotificationsSceneChange) WeatherNotifications.MaybeDisplayWeatherNotification();
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

		public static void UpdateStages(WeatherStage current)
		{
			if (MonitorData == null) return;

			MonitorData.Prev = current;
		}

		//public override void OnLateUpdate()
		//{
		//	base.OnLateUpdate();
		//	try
		//	{
		//		if (!SceneUtilities.IsScenePlayable()) return;
		//	}
		//	catch { }

		//	try
		//	{
		//		if (!GameManager.GetUniStorm()) return;
		//	}
		//	catch { }

		//	try
		//	{
		//		WeatherNotifications.MaybeDisplayWeatherNotification();
		//	}
		//	catch { }
		//}

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
