using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraMonitor.Patches
{
	/// <summary>
	/// This is to allow the radio to work at any time. <c>Main.SettingsInstance.UnlockRadio</c>
	/// </summary>
	[HarmonyPatch(typeof(HandheldShortwaveItem), nameof(HandheldShortwaveItem.CanTrack))]
	public class HandheldRadio_CanTrack
	{
		[HarmonyPrefix]
		public static bool Apply(HandheldShortwaveItem __instance, ref bool __result)
		{
			if (__instance == null) return true;
			if (Main.SettingsInstance.UnlockRadio) __result = true;
			return !Main.SettingsInstance.UnlockRadio;
		}
	}

	[HarmonyPatch(typeof(HandheldShortwaveItem), nameof(HandheldShortwaveItem.Update))]
	public class HandheldShortwaveItem_Update
	{
		[HarmonyPostfix]
		public static void Apply(HandheldShortwaveItem __instance)
		{
			if (__instance == null) return;
			if (WeatherUtilities.IsAuroraFullyActive(GameManager.GetAuroraManager())) return;
			if (Main.SettingsInstance.UnlockRadio)
			{
				__instance.m_LastElectrostaticForceActive = true;
			}
		}
	}
}
