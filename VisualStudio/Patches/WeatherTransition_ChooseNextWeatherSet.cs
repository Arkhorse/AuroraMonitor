namespace AuroraMonitor
{
	[HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.ChooseNextWeatherSet), new Type[] { typeof(WeatherTransitionWeights), typeof(bool), typeof(bool) })]
	public class WeatherTransition_ChooseNextWeatherSet
	{
		public static void Postfix(WeatherTransition __instance, ref WeatherTransitionWeights customWeights, ref bool forceClear, ref bool forceAurora)
		{
			if (__instance == null) return;
			if (Main.Config == null || Main.Config.WeatherWeights == null) return;

			if (forceClear) Main.Logger.Log("Forced Clear", FlaggedLoggingLevel.Debug);
			if (forceAurora) Main.Logger.Log("Forced Aurora", FlaggedLoggingLevel.Debug);
		}
	}
}
