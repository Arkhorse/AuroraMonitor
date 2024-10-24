
namespace AuroraMonitor.ModSettings
{
	internal class Settings : JsonModSettings
	{
		#region Basic
		[Section("Basic")]

		[Name("Enable")]
		[Description("This allows you to entirely disable the mod")]
		public bool EnableMod = true;

		[Name("Can sleep during Aurora")]
		public bool AllowSleepDuringAurora = true;

		[Name("Units to use")]
		[Description("Change this to change the displayed units")]
		[Choice(["KM/H", "M/S", "MP/H"])]
		public UnitUse UnitUse = UnitUse.Metric;

		[Name("Boost Aurora")]
		[Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you don't mind unpredictable things to happen")]
		public bool BoostAurora = false;

		[Name("Aurora Audio")]
		[Description("Turn this off if you dont want the Aurora audio. You can also use this to fix the broken audio by toggling it")]
		public bool PlayAuroraAudio = true;

		[Name("Light Flickering")]
		[Description("Disable this if you dont want lights to flicker (BETA)")]
		public bool AuroraLightsFlicker = true;

		[Name("Unlock Radio Usability")]
		[Description("This will allow you to use the radio at any time, providing you repaired the relevent tower")]
		public bool UnlockRadio = false;
		#endregion
		#region Aurora Probability
		[Section( "Aurora Probability" )]

		[Name( "Aurora Early Chance" )]
		[Description( "Change this to change how likely an Aurora will happen" )]
		[Slider( 0, 100 )]
		public int AuroraChanceEarly = 20;

		[Name( "Aurora Late Chance" )]
		[Description( "Change this to change how likely an Aurora will happen" )]
		[Slider( 0, 100 )]
		public int AuroraChanceLate = 10;
		#endregion
		#region RGBa Colour Chooser
		[Section("RGBa Colour Chooser")]

		[Name("Colour Presets")]
		[Description("Default = Vanilla, Cinematic = VERY Bright & Colourful, Custom = your choice")]
		public AuroraColourSettings AuroraColour = AuroraColourSettings.Default;

		[Name("Red")]
		[Slider(0.01f, 1f)]
		public float AuroraColour_R         = 0.3f;

		[Name("Green")]
		[Slider(0.01f, 1f)]
		public float AuroraColour_G         = 0.2f;

		[Name("Blue")]
		[Slider(0.01f, 1f)]
		public float AuroraColour_B         = 0.35f;
		#endregion
		#region Weather Display
		[Section("Weather Display")]

		[Name("Enable")]
		public bool WeatherDisplayEnable = true;

		[Name("Temperature Units")]
		[Description("Which unit type to use. Celsius, Kelvin or Fahrenheit")]
		public TemperatureUnits TemperatureUnits = TemperatureUnits.Celsius;

		//[Name("Font Size")]
		//public int FontSize = 16;
		#endregion
		#region Notifications
		//[Section("Notifications")]

		//[Name("Units to use")]
		//[Description("Change this to change the displayed units")]
		//[Choice(["KM/H", "M/S", "MP/H"])]
		//public UnitUse UnitsToUse = UnitUse.Metric;

		//[Name("Weather Stage Time")]
		//[Description("Displays when a new weather stage is chosen")]
		//[Slider(0f, 60f, 61)]
		//public float WeatherNotificationsTime = 15f;

		//[Name("Delay Time")]
		//[Description("When enabled, this will delay the notification. It is in seconds")]
		//[Slider(2f, 60f, 61)]
		//public float WeatherNotificationsDelay = 2f;

		//[Name("Display Inside")]
		//[Description("Show the message while indoors")]
		//public bool WeatherNotificationsIndoors = false;

		//[Name("Show On Scene Change")]
		//[Description("Show the message whenever you change scenes")]
		//public bool WeatherNotificationsSceneChange = false;

		//[Name("Toxic Fog icon")]
		//[Description("Three different icons for Toxic Fog (UNSUPPORTED)")]
		//[Choice(["Level 1", "Level 2", "Level 3"])]
		//public ToxicFogImages fogImages = ToxicFogImages.l1;
		#endregion
		#region Forced Aurora
		[Section("Forced Aurora")]

		[Name("Force Aurora")]
		[Description("Will force an Aurora at the next oppertunity")]
		public bool forceNextAurora = false;

		[Name("Force Early")]
		[Description("This will force the Aurora to appear right when night starts")]
		public bool forceEarly = false;

		[Name("Force Late")]
		[Description("This will force the Aurora to appear later at night")]
		public bool forceLate = false;

		[Name("Duration Override")]
		[Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you don't mind unpredictable things to happen")]
		public bool forceDuration = false;

		[Name("Duration Overide Time")]
		[Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you don't mind unpredictable things to happen")]
		public float forceDurationTime = 0f;
		#endregion

		protected override void OnChange(FieldInfo field, object? oldValue, object? newValue)
		{
			if (field.Name == nameof(Main.SettingsInstance.PlayAuroraAudio))
			{
				if (Main.SettingsInstance.PlayAuroraAudio)
				{
					GameManager.GetAuroraManager().UpdateAuroraAudio();
				}
				else
				{
					GameManager.GetAuroraManager().AuroraAudioStop();
				}
			}
		}

		protected override void OnConfirm()
		{
			Main.SettingsInstance.OnLoadConfirm();
			base.OnConfirm();
		}

#pragma warning disable CA1822 // Mark members as static
		public void OnLoadConfirm()
		{
			if (Main.SettingsInstance.BoostAurora && !GameManager.GetAuroraManager().IsAuroraBoostEnabled())
			{
				GameManager.GetAuroraManager().BoostAurora(true);
			}
			if (Main.SettingsInstance.forceNextAurora)
			{
				GameManager.GetAuroraManager().ForceAuroraNextOpportunity(Main.SettingsInstance.forceEarly, Main.SettingsInstance.forceLate, Main.SettingsInstance.forceDurationTime);
			}

			if (Main.SettingsInstance.AuroraColour == AuroraColourSettings.Cinematic)
			{
				GameManager.GetAuroraManager().SetCinematicColours(true);
			}
			else if (Main.SettingsInstance.AuroraColour == AuroraColourSettings.Custom)
			{
				GameManager.GetAuroraManager().GetAuroraColour();
			}
		}
#pragma warning restore CA1822 // Mark members as static
		// This is used to load the settings
		internal static void OnLoad()
		{
			Main.SettingsInstance.AddToModSettings(BuildInfo.GUIName);
			Main.SettingsInstance.RefreshGUI();

			Main.SettingsInstance.SetFieldVisible(nameof(Main.SettingsInstance.UnlockRadio), false);
		}
	}
}
