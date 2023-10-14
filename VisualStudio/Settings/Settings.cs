namespace AuroraMonitor
{
	internal class Settings : JsonModSettings
	{
		internal static Settings Instance { get; } = new();

		public enum AuroraColourSettings { Default, Cinematic, Custom };
		public enum UnitUse { Metric, Scientific, Imperial }

		/*
			Cinematic #6496fa   : 0.392156869f, 0.5882353f, 0.980392158f, 1f
			Default             : 0.3, 0.2, 0.35, 0
		*/

		public enum ToxicFogImages { l1, l2, l3 }

		#region Settings

		[Section("Basic")]

		[Name("Enable")]
		[Description("This allows you to entirely disable the mod")]
		public bool EnableMod = true;

		[Name("Print Debug Data")]
		[Description("Enable this to print debug data to the Melon Log when you click confirm. This can also be done using \"AuroraMonitorDebug\" in the Console")]
		public bool PRINTDEBUG = false;

		[Name("Print debug to log")]
		[Description("Adds additional debug info to the log. This is not the same as the above setting")]
		public bool PRINTDEBUGLOG = false;

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

            //UIUtilities.UpdateLabelFontSize(UIUtilities.WeatherMonitorWindSpeedUnit, Instance.FirstAidScreen_FontSize);
            //UIUtilities.UpdateLabelFontSize(UIUtilities.WeatherMonitorWindSpeed, Instance.FirstAidScreen_FontSize);
            //UIUtilities.UpdateLabelFontSize(UIUtilities.WeatherMonitorWeather, Instance.FirstAidScreen_FontSize);

            base.OnChange(field, oldValue, newValue);
		}

		protected override void OnConfirm()
		{
			Instance.OnLoadConfirm();
			base.OnConfirm();
		}

		private void RefreshFields()
		{
			// Section:Forced Aurora
			SetFieldVisible(nameof(forceEarly),                 Instance.forceNextAurora);
			SetFieldVisible(nameof(forceLate),                  Instance.forceNextAurora);
			SetFieldVisible(nameof(forceDuration),              Instance.forceNextAurora);
			SetFieldVisible(nameof(forceDurationTime),          Instance.forceNextAurora);

			// Section: RGBa Colour Selector
			SetFieldVisible(nameof(AuroraColour_R),             Instance.AuroraColour == AuroraColourSettings.Custom);
			SetFieldVisible(nameof(AuroraColour_G),             Instance.AuroraColour == AuroraColourSettings.Custom);
			SetFieldVisible(nameof(AuroraColour_B),             Instance.AuroraColour == AuroraColourSettings.Custom);
		}

		public void OnLoadConfirm()
		{
			if (Instance.BoostAurora && !GameManager.GetAuroraManager().IsAuroraBoostEnabled())
			{
				GameManager.GetAuroraManager().BoostAurora(true);
			}
			if (Instance.forceNextAurora)
			{
				GameManager.GetAuroraManager().ForceAuroraNextOpportunity(Instance.forceEarly, Instance.forceLate, Instance.forceDurationTime);
			}
			if (Instance.PRINTDEBUG)
			{
				ConsoleCommands.PrintDebugInfo();
				Instance.PRINTDEBUG = false;
			}
			if (Instance.AuroraColour == AuroraColourSettings.Cinematic)
			{
				GameManager.GetAuroraManager().SetCinematicColours(true);
			}
		}


		// This is used to load the settings
		internal static void OnLoad()
		{
			Instance.AddToModSettings(BuildInfo.GUIName);
			Instance.RefreshFields();
			Instance.RefreshGUI();
		}
	}
}
