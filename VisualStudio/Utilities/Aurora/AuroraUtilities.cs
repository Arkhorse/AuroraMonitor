namespace AuroraMonitor.Utilities.Aurora
{
    public class AuroraUtilities
    {
		/// <summary>
		/// Used to fetch aurora time. Currently not implemented
		/// </summary>
		internal static void FetchAuroraTime()
        {
            Main.Logger.Log($"Aurora Time Left: {GameManager.GetAuroraManager().GetNormalizedAlpha()}", FlaggedLoggingLevel.None);
        }

        /// <summary>
        /// Used to fetch aurora colour for the log
        /// </summary>
        internal static void FetchAuroraColour()
        {
            Color AuroraColor = GameManager.GetAuroraManager().GetAuroraColour();
            Main.Logger.Log($"Aurora Color: R:{AuroraColor.r} G:{AuroraColor.g} B:{AuroraColor.b} A:{AuroraColor.a}", FlaggedLoggingLevel.None);
        }

		public static void SetAuroraChancesEarly(Weather weather, int early )
		{
			weather.m_AuroraEarlyWindowProbability = early;
		}
		public static void SetAuroraChancesLate( Weather weather, int late )
		{
			weather.m_AuroraLateWindowProbability = late;
		}
		public static void SetAuroraChances( Weather weather, int early, int late )
		{
			weather.m_AuroraEarlyWindowProbability = early;
			weather.m_AuroraLateWindowProbability = late;
		}
	}
}
