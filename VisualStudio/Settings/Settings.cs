namespace AuroraMonitor
{
    internal class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();

        [Name("Enable")]
        [Description("This allows you to entirely disable the mod")]
        public bool EnableMod = true;

        [Name("Print Debug Data")]
        [Description("Enable this to print debug data to the Melon Log when you click confirm. This can also be done using \"AuroraMonitorDebug\" in the Console")]
        public bool PRINTDEBUG = false;

        [Name("Print debug to log")]
        [Description("Adds additional debug info to the log. This is not the same as the above setting")]
        public bool PRINTDEBUGLOG = false;

        protected override void OnConfirm()
        {
            if (Instance.PRINTDEBUG)
            {
                ConsoleCommands.PrintDebugInfo();
                Instance.PRINTDEBUG = false;
            };

            base.OnConfirm();
        }

        internal static void OnLoad()
        {
            Instance.AddToModSettings("[Weather Monitor] Settings");
            Instance.RefreshGUI();
        }
    }
}
