using AuroraMonitor.Utilities.Logger;

namespace AuroraMonitor.ModSettings
{
	internal class Settings : JsonModSettings
	{
		#region Settings
		[Section("Basic")]

		[Name("Enable")]
		[Description("This allows you to entirely disable the mod")]
		public bool EnableMod = true;

		[Name("Boost Aurora")]
		[Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you don't mind unpredictable things to happen")]
		public bool BoostAurora = false;

		[Section( "Aurora Probability" )]

		[Name( "Aurora Early Chance" )]
		[Description( "Change this to change how likely an Aurora will happen" )]
		[Slider( 0, 100 )]
		public int AuroraChanceEarly = 20;

		[Name( "Aurora Late Chance" )]
		[Description( "Change this to change how likely an Aurora will happen" )]
		[Slider( 0, 100 )]
		public int AuroraChanceLate = 10;

		[Section( "Notifications" )]

		[Name("Units to use")]
		[Description("Change this to change the displayed units")]
		[Choice(new string[] { "KM/H", "M/S", "MP/H" })]
		public UnitUse UnitsToUse = UnitUse.Metric;

		[Name( "Weather Stage Time" )]
		[Description( "Displays when a new weather stage is chosen" )]
		[Slider( 0f, 60f, 61 )]
		public float WeatherNotificationsTime = 15f;

		[Name("Delay Time")]
		[Description("When enabled, this will delay the notification. It is in seconds")]
		[Slider(2f, 60f, 61)]
		public float WeatherNotificationsDelay = 2f;

		[Name( "Display Inside" )]
		[Description( "Show the message while indoors" )]
		public bool WeatherNotificationsIndoors = false;

		[Name( "Show On Scene Change" )]
		[Description( "Show the message whenever you change scenes" )]
		public bool WeatherNotificationsSceneChange = false;

		[Name("Toxic Fog icon")]
		[Description("Three different icons for Toxic Fog (UNSUPPORTED)")]
		[Choice(new string[] { "Level 1", "Level 2", "Level 3" })]
		public ToxicFogImages fogImages = ToxicFogImages.l1;

		[Section("Forced Aurora")]

		[Name("Force Aurora")]
		[Description("Will force an Aurora at the next oppertunity")]
		public bool forceNextAurora         = false;

		[Name("Force Early")]
		[Description("This will force the Aurora to appear right when night starts")]
		public bool forceEarly              = false;

		[Name("Force Late")]
		[Description("This will force the Aurora to appear later at night")]
		public bool forceLate               = false;

		[Name("Duration Override")]
		[Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you don't mind unpredictable things to happen")]
		public bool forceDuration           = false;

		[Name("Duration Overide Time")]
		[Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you don't mind unpredictable things to happen")]
		public float forceDurationTime      = 0f;

		[Section("RGBa Colour Chooser")]

		[Name("Colour Presets")]
		[Description("Default = Vanilla, Cinematic = Colourful, Custom = your choice")]
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

		[Section("First Aid Screen")]

		[Name("Enabled")]
		public bool FirstAidScreen_Enabled = true;

		[Name("Font Size")]
		public int FirstAidScreen_FontSize = 16;

		[Name("Units to use")]
		[Description("Change this to change the displayed units")]
		[Choice(new string[] { "KM/H", "M/S", "MP/H" })]
		public UnitUse FirstAidScreen_UnitsToUse = UnitUse.Metric;

		[Section("Forcast")]

		[Name("Temperature Units")]
		[Description("Which unit type to use. Celsius, Kelvin or Fahrenheit")]
		public TemperatureUnits TemperatureUnits = TemperatureUnits.Celsius;

		[Section("Logging Levels")]

		[Name("Trace")]
		public bool Trace			= false;

		[Name("Debug")]
		public bool Debug			= false;

		[Name("Information")]
		public bool Information		= false;

		[Name("Warning")]
		public bool Warning			= true;

		[Name("Error")]
		public bool Error			= true;

		[Name("Critical")]
		public bool Critical		= true;
		#endregion

		protected override void OnChange(FieldInfo field, object? oldValue, object? newValue)
		{
			if (field.Name == nameof(forceNextAurora))
			{
				RefreshFields();
			}
			if (field.Name == nameof(AuroraColour))
			{
				RefreshFields();
			}

			//UIUtilities.UpdateLabelFontSize(UIUtilities.WeatherMonitorWindSpeedUnit, Main.SettingsInstance.FirstAidScreen_FontSize);
			//UIUtilities.UpdateLabelFontSize(UIUtilities.WeatherMonitorWindSpeed, Main.SettingsInstance.FirstAidScreen_FontSize);
			//UIUtilities.UpdateLabelFontSize(UIUtilities.WeatherMonitorWeather, Main.SettingsInstance.FirstAidScreen_FontSize);

			base.OnChange(field, oldValue, newValue);
		}

		protected override void OnConfirm()
		{
			Main.SettingsInstance.OnLoadConfirm();
			base.OnConfirm();
		}

		private void RefreshFields()
		{
			// Section:Forced Aurora
			SetFieldVisible(nameof(forceEarly),                 Main.SettingsInstance.forceNextAurora);
			SetFieldVisible(nameof(forceLate),                  Main.SettingsInstance.forceNextAurora);
			SetFieldVisible(nameof(forceDuration),              Main.SettingsInstance.forceNextAurora);
			SetFieldVisible(nameof(forceDurationTime),          Main.SettingsInstance.forceNextAurora);

			// Section: RGBa Colour Selector
			SetFieldVisible(nameof(AuroraColour_R),             Main.SettingsInstance.AuroraColour == AuroraColourSettings.Custom);
			SetFieldVisible(nameof(AuroraColour_G),             Main.SettingsInstance.AuroraColour == AuroraColourSettings.Custom);
			SetFieldVisible(nameof(AuroraColour_B),             Main.SettingsInstance.AuroraColour == AuroraColourSettings.Custom);
		}

#pragma warning disable CA1822 // Mark members as static
		public void OnLoadConfirm()
		{
			if (Main.SettingsInstance.Trace) 
				ComplexLogger.AddLevel<Main>(FlaggedLoggingLevel.Trace);
			else if (!Main.SettingsInstance.Trace) 
				ComplexLogger.RemoveLevel<Main>(FlaggedLoggingLevel.Trace);

			if (Main.SettingsInstance.Debug) 
				ComplexLogger.AddLevel<Main>(FlaggedLoggingLevel.Debug);
			else if (!Main.SettingsInstance.Debug) 
				ComplexLogger.RemoveLevel<Main>(FlaggedLoggingLevel.Debug);

			if (Main.SettingsInstance.Information) 
				ComplexLogger.AddLevel<Main>(FlaggedLoggingLevel.Information);
			else if (!Main.SettingsInstance.Information) 
				ComplexLogger.RemoveLevel<Main>(FlaggedLoggingLevel.Information);

			if (Main.SettingsInstance.Warning)
				ComplexLogger.AddLevel<Main>(FlaggedLoggingLevel.Warning);
			else if (!Main.SettingsInstance.Warning)
				ComplexLogger.RemoveLevel<Main>(FlaggedLoggingLevel.Warning);

			if (Main.SettingsInstance.Error) 
				ComplexLogger.AddLevel<Main>(FlaggedLoggingLevel.Error);
			else if (!Main.SettingsInstance.Error)
				ComplexLogger.RemoveLevel<Main>(FlaggedLoggingLevel.Error);

			if (Main.SettingsInstance.Critical) 
				ComplexLogger.AddLevel<Main>(FlaggedLoggingLevel.Critical);
			else if (!Main.SettingsInstance.Critical)
				ComplexLogger.RemoveLevel<Main>(FlaggedLoggingLevel.Critical);

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
			Main.SettingsInstance.RefreshFields();
			Main.SettingsInstance.RefreshGUI();
		}
	}
}
