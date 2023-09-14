namespace AuroraMonitor
{
    public class WeatherSettings : JsonModSettings
    {
        public static WeatherSettings Instance { get; } = new();

        public enum ToxicFogImages { l1, l2, l3 }

        #region Settings

        [Section("Notifications")]

        [Name("Weather Stage Time")]
        [Description("Displays when a new weather stage is chosen")]
        [Slider(0f, 60f, 60)]
        public float WeatherNotificationsTime = 15f;

        [Name("Display Inside")]
        [Description("Show the message while indoors")]
        public bool WeatherNotificationsIndoors = false;

        [Name("Show On Scene Change")]
        [Description("Show the message whenever you change scenes")]
        public bool WeatherNotificationsSceneChange = false;

        [Name("Toxic Fog icon")]
        [Description("Three different icons for Toxic Fog (UNSUPPORTED)")]
        [Choice(new string[] { "Level 1", "Level 2", "Level 3" })]
        public ToxicFogImages fogImages = ToxicFogImages.l1;

        [Name("Hotkey")]
        [Description("When pressed, will display the weather notification")]
        public KeyCode DisplayNotification = KeyCode.None;

        #endregion

        // This is used to load the settings
        internal static void OnLoad()
        {
            Instance.AddToModSettings("[Weather Monitor] Weather Settings");
            Instance.RefreshGUI();
        }
    }
}