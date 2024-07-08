using Il2CppTLD.ModularElectrolizer;

namespace AuroraMonitor.Patches
{
	[HarmonyPatch(typeof(AuroraModularElectrolizer), nameof(AuroraModularElectrolizer.Awake))]
	public class AuroraModularElectrolizer_Awake
	{
		[HarmonyPostfix]
		public static void Apply(AuroraModularElectrolizer __instance)
		{
			if (__instance == null) return;
			if (!Main.SettingsInstance.AuroraLightsFlicker)
			{
				var flickers = __instance.m_FlickerSet;
				for (int f = 0; f < flickers.Count; f++)
				{
					var range = flickers[f].m_FlickerChangeTime;
					var timingcontrol = __instance.m_FlickerTimingControl;
					Main.Logger.Log($"Index: {f}, Name: {flickers[f].name}, current set: {timingcontrol.m_CurrentFlickerSet}, old limit: {timingcontrol.m_FlickerDurationLimit}, old MinMax: {range.m_Min}/{range.m_Max}", FlaggedLoggingLevel.Debug);
					// based on the mostly on flicker set
					range.m_Min = 5;
					range.m_Max = 20;

					flickers[f].m_MaxIntensity = 1;
				}
			}
		}
	}
}
