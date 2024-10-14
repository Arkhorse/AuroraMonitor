//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AuroraMonitor.Patches
//{
//	/// <summary>
//	/// This is to allow the radio to work at any time. <c>Main.SettingsInstance.UnlockRadio</c>
//	/// </summary>
//	//[HarmonyPatch(typeof(HandheldShortwaveItem), nameof(HandheldShortwaveItem.CanTrack))]
//	//public class HandheldRadio_CanTrack
//	//{
//	//	[HarmonyPrefix]
//	//	public static bool Apply(HandheldShortwaveItem __instance, ref bool __result)
//	//	{
//	//		if (__instance == null) return true;
//	//		if (Main.SettingsInstance.UnlockRadio) __result = true;
//	//		return !Main.SettingsInstance.UnlockRadio;
//	//	}
//	//}

//	[HarmonyPatch(typeof(HandheldShortwaveItem), nameof(HandheldShortwaveItem.Update))]
//	public class HandheldShortwaveItem_Update
//	{
//		[HarmonyPostfix]
//		public static void Apply(HandheldShortwaveItem __instance)
//		{
//			if (__instance == null) return;
//			if (WeatherUtilities.IsAuroraFullyActive(GameManager.GetAuroraManager())) return;
//			if (Main.SettingsInstance.UnlockRadio)
//			{
//				__instance.m_LastElectrostaticForceActive	= true;
//				__instance.m_LedBlinkTimer					= 0.2886f;
//				__instance.m_NearestAudioRange				= 900f;
//				__instance.m_NearestDistance				= 1000000f;
//				__instance.m_NearestFullSignalRange			= 20f;
//				__instance.m_NearestMaxRange				= 1000f;
//			}
//		}
//	}

//	[HarmonyPatch(typeof(Il2CppInteractiveObjects.TransmitterManager), nameof(Il2CppInteractiveObjects.TransmitterManager.DeserializeAll), [typeof(string)])]
//	public class Il2CppInteractiveObjects_TransmitterRuntimeState
//	{
//		[HarmonyPostfix]
//		public static void Apply(Il2CppInteractiveObjects.TransmitterManager __instance)
//		{
//			for (int i = 0; i < __instance.m_TransmitterList.Count; i++)
//			{
//				var transmitter = __instance.m_TransmitterList[i];
//				if (transmitter != null)
//				{
//					var transmitterstate = __instance.GetTransmitterState(transmitter);
//					if (transmitterstate != null)
//					{
//						if (transmitterstate.m_IsFixed)
//						{
//							transmitterstate. = true;
//						}
//					}
//				}
//			}
//		}
//	}
//}
