namespace AuroraMonitor
{
    internal class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        public enum AuroraColourSettings { Default, Cinematic, Custom };

        /*
            Cinematic #6496fa   : 0.392156869f, 0.5882353f, 0.980392158f, 1f
            Default             : 0.3, 0.2, 0.35, 0
        */

        #region Settings

        [Section("Basic")]

        [Name("Print Debug Data")]
        [Description("Enable this to print debug data to the Melon Log when you click confirm. This can also be done using \"AuroraMonitorDebug\" in the Console")]
        public bool PRINTDEBUG = false;

        [Name("Print to log when messages are sent")]
        [Description("When AuroraMonitor attempts to send a message, this will print that message to the log as well")]
        public bool PRINTDEBUGLOG = false;

        [Name("Boost Aurora")]
        [Description("Currently an unknown option")]
        public bool BoostAurora             = false;

        [Section("Aurora Probability")]

        [Name("Aurora Early Chance")]
        [Description("Change this to change how likely an Aurora will happen")]
        [Slider(0, 100)]
        public int AuroraEarlyWindowProb    = 20;

        [Name("Aurora Late Chance")]
        [Description("Change this to change how likely an Aurora will happen")]
        [Slider(0, 100)]
        public int AuroraLateWindowProb     = 10;

        [Section("Notifications")]

        [Name("Aurora Notification Time")]
        [Description("Displays when an Aurora starts")]
        [Slider(0f, 60f, 60)]
        public float AuroraNotificationTime = 15f;

        [Name("Weather Stage Time")]
        [Description("Displays when a new weather stage is chosen")]
        [Slider(0f, 60f, 60)]
        public float WeatherStageNotificationTime = 15f;

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
        [Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you dont mind unpredicatable things to happen")]
        public bool forceDuration           = false;

        [Name("Duration Overide Time")]
        [Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you dont mind unpredicatable things to happen")]
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

        [Name("Alpha")]
        [Slider(0f, 1f)]
        [Description("Alpha refers to brightness but is also used to control how visible the aurora is. You shouldnt touch this")]
        public float AuroraColour_A         = 0f;

        [Name("Normalize Alpha")]
        [Description("Will normalize alpha, which is the amount the aurora is active (1f when fully active, 0f when not)")]
        public bool AuroraColour_Normalize  = true;

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
            base.OnChange(field, oldValue, newValue);
        }

        protected override void OnConfirm()
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
                Main.PrintDebugInfo();
                Instance.PRINTDEBUG = false;
            }
            Utilities.UpdateAuroraColor();
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
            SetFieldVisible(nameof(AuroraColour_A),             Instance.AuroraColour == AuroraColourSettings.Custom);
            SetFieldVisible(nameof(AuroraColour_Normalize),     Instance.AuroraColour == AuroraColourSettings.Custom);
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