namespace AuroraMonitor.Utilities
{
    internal class Utilities
    {
        /// <summary>
        /// Used to fetch aurora colour
        /// </summary>
        internal static void FetchAuroraColour()
        {
            Color AuroraColor = GameManager.GetAuroraManager().GetAuroraColour();
            Logger.Log($"Aurora Color: R:{AuroraColor.r} G:{AuroraColor.g} B:{AuroraColor.b} A:{AuroraColor.a}");
        }

        public static void UpdateAuroraColor()
        {
            if (AuroraSettings.Instance.AuroraColour == AuroraColourSettings.Cinematic) GameManager.GetAuroraManager().SetCinematicColours(true);
            else GameManager.GetAuroraManager().SetCinematicColours(false);
        }

        /// <summary>
        /// Used to fetch aurora time. Currently not implemented
        /// </summary>
        internal static void FetchAuroraTime()
        {
            Logger.Log($"Aurora Time Left: {GameManager.GetAuroraManager().GetNormalizedAlpha()}");
        }
    }
}