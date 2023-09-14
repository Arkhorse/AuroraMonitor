namespace AuroraMonitor
{
    internal class AuroraSettings : JsonModSettings
    {
        internal static AuroraSettings Instance { get; } = new();

        [Name("Boost Aurora")]
        [Description("Currently an unknown option")]
        public bool BoostAurora = false;

        [Section("Better Aurora")]

        [Name("Enable")]
        [Description("This makes the aurora weather better. No wind, higher temperature")]
        public bool EnableBetterAurora = true;

        [Name("Temperature")]
        [Slider(-50, 50)]
        public int BetterAuroraTemperature = -10;

        [Section("Aurora Probability")]

        [Name("Aurora Early Chance")]
        [Description("Change this to change how likely an Aurora will happen")]
        [Slider(0, 100)]
        public int AuroraChanceEarly = 20;

        [Name("Aurora Late Chance")]
        [Description("Change this to change how likely an Aurora will happen")]
        [Slider(0, 100)]
        public int AuroraChanceLate = 10;

        [Name("Remember choice")]
        [Description("This makes the chances only save for the current game session when off")]
        public bool AuroraChanceRemember = false;

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
        [Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you dont mind unpredicatable things to happen")]
        public bool forceDuration = false;

        [Name("Duration Overide Time")]
        [Description("WARNING: This is an unknown setting that does unknown things. Only enable it if you dont mind unpredicatable things to happen")]
        public float forceDurationTime = 0f;

        [Section("RGBa Colour Chooser")]

        [Name("Colour Presets")]
        [Description("Default = Vanilla, Cinematic = Colourful, Custom = your choice")]
        public AuroraColourSettings AuroraColour = AuroraColourSettings.Default;

        [Name("Red")]
        [Slider(0.01f, 1f)]
        public float AuroraColour_R = 0.3f;

        [Name("Green")]
        [Slider(0.01f, 1f)]
        public float AuroraColour_G = 0.2f;

        [Name("Blue")]
        [Slider(0.01f, 1f)]
        public float AuroraColour_B = 0.35f;

        [Name("Alpha")]
        [Slider(0f, 1f)]
        [Description("Alpha refers to brightness but is also used to control how visible the aurora is. You shouldnt touch this")]
        public float AuroraColour_A = 0f;

        [Name("Normalize Alpha")]
        [Description("Will normalize alpha, which is the amount the aurora is active (1f when fully active, 0f when not)")]
        public bool AuroraColour_Normalize = true;

        private void RefreshFields()
        {
            // Section:Forced Aurora
            SetFieldVisible(nameof(forceEarly), Instance.forceNextAurora);
            SetFieldVisible(nameof(forceLate), Instance.forceNextAurora);
            SetFieldVisible(nameof(forceDuration), Instance.forceNextAurora);
            SetFieldVisible(nameof(forceDurationTime), Instance.forceNextAurora);

            // Section: RGBa Colour Selector
            SetFieldVisible(nameof(AuroraColour_R), Instance.AuroraColour == AuroraColourSettings.Custom);
            SetFieldVisible(nameof(AuroraColour_G), Instance.AuroraColour == AuroraColourSettings.Custom);
            SetFieldVisible(nameof(AuroraColour_B), Instance.AuroraColour == AuroraColourSettings.Custom);
            SetFieldVisible(nameof(AuroraColour_A), Instance.AuroraColour == AuroraColourSettings.Custom);
            SetFieldVisible(nameof(AuroraColour_Normalize), Instance.AuroraColour == AuroraColourSettings.Custom);
        }

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
            if (!Instance.AuroraChanceRemember && Instance.AuroraChanceEarly != GameManager.GetWeatherComponent().m_AuroraEarlyWindowProbability)
            {
                Utilities.Aurora.SetAuroraChancesEarly(Instance.AuroraChanceEarly);
            }
            else if (Instance.AuroraChanceRemember && Instance.AuroraChanceEarly != 20)
            {
                Utilities.Aurora.SetAuroraChancesEarly(Instance.AuroraChanceEarly);
            }
            if (!Instance.AuroraChanceRemember && Instance.AuroraChanceLate != GameManager.GetWeatherComponent().m_AuroraLateWindowProbability)
            {
                Utilities.Aurora.SetAuroraChancesLate(Instance.AuroraChanceLate);
            }
            else if (Instance.AuroraChanceRemember && Instance.AuroraChanceLate != 10)
            {
                Utilities.Aurora.SetAuroraChancesLate(Instance.AuroraChanceLate);
            }

            Utilities.Utilities.UpdateAuroraColor();

            base.OnConfirm();
        }

        internal static void OnLoad()
        {
            Instance.AddToModSettings("[Weather Monitor] Aurora Settings");
            Instance.RefreshFields();
            Instance.RefreshGUI();
        }
    }
}
