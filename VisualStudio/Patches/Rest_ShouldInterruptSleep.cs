namespace AuroraMonitor.Patches
{
	[HarmonyPatch(typeof(Rest), nameof(Rest.ShouldInterruptSleep))]
	public class Rest_ShouldInterruptSleep
	{
		public static bool Prefix(Rest __instance, ref bool __result)
		{
			if (__instance != null)
			{
				if (Main.SettingsInstance.AllowSleepDuringAurora) return true;
				if (!(GameManager.GetUniStorm().m_CurrentWeatherStage == WeatherStage.ClearAurora)) return true;
				Main.Logger.Log($"Aurora happening, cant sleep", FlaggedLoggingLevel.Debug);
				__result = true;
				return false;
			}
			return true;
		}
	}
}
