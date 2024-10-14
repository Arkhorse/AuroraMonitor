using AuroraMonitor.GUI.Addons.FirstAidPanel;
namespace AuroraMonitor.Patches
{
	[HarmonyPatch(typeof(Panel_FirstAid), nameof(Panel_FirstAid.Enable), new Type[] { typeof(bool) })]
	public class Panel_FirstAid_Enable
	{
		private static bool WasInit { get; set; } = false;
		public static void Prefix(Panel_FirstAid __instance)
		{
			if (!__instance.enabled) return;
			if (!WasInit)
			{
				__instance.gameObject.AddComponent<WeatherDisplay>();
				WasInit = true;

				Main.Logger.Log("Added WeatherDisplay to Panel_FirstAid", FlaggedLoggingLevel.Debug);
			}
		}
	}
}
