using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraMonitor.Aurora
{
    public class AuroraUtilities
    {
		/// <summary>
		/// Used to fetch aurora time. Currently not implemented
		/// </summary>
		internal static void FetchAuroraTime()
        {
            Logging.Log($"Aurora Time Left: {GameManager.GetAuroraManager().GetNormalizedAlpha()}");
        }

        /// <summary>
        /// Used to fetch aurora colour for the log
        /// </summary>
        internal static void FetchAuroraColour()
        {
            Color AuroraColor = GameManager.GetAuroraManager().GetAuroraColour();
            Logging.Log($"Aurora Color: R:{AuroraColor.r} G:{AuroraColor.g} B:{AuroraColor.b} A:{AuroraColor.a}");
        }

		public static void SetAuroraChancesEarly( int early )
		{
			GameManager.GetWeatherComponent().m_AuroraEarlyWindowProbability = early;
		}
		public static void SetAuroraChancesLate( int late )
		{
			GameManager.GetWeatherComponent().m_AuroraLateWindowProbability = late;
		}
		public static void SetAuroraChances( int early, int late )
		{
			GameManager.GetWeatherComponent().m_AuroraEarlyWindowProbability = early;
			GameManager.GetWeatherComponent().m_AuroraLateWindowProbability = late;
		}
	}
}
