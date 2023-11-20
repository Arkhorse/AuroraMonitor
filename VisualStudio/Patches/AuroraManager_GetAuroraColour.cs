using AuroraMonitor.Utilities.Logger;

namespace AuroraMonitor.Patches
{
    [HarmonyPatch(typeof(AuroraManager), nameof(AuroraManager.GetAuroraColour))]
    internal class AuroraManager_GetAuroraColour
    {
        private static bool Prefix()
        {
            return false;
        }

        private static void Postfix(ref Color __result)
        {
            float normalizedAlpha = GameManager.GetAuroraManager().GetNormalizedAlpha();
            Color white = Color.white;

            if (Main.SettingsInstance.AuroraColour == AuroraColourSettings.Custom)
            {
                white.r = Main.SettingsInstance.AuroraColour_R;
                white.g = Main.SettingsInstance.AuroraColour_G;
                white.b = Main.SettingsInstance.AuroraColour_B;
                white.a = Mathf.Pow(normalizedAlpha, 2);

                __result = white;

                Main.Logger.Log(FlaggedLoggingLevel.Debug, $"Aurora Color retrieved from AuroraManager.GetAuroraColour(), current color is {__result}");
                return;
            }

            if ( (GameManager.GetAuroraManager().m_UseCinematicColours && Main.SettingsInstance.AuroraColour == AuroraColourSettings.Default) || Main.SettingsInstance.AuroraColour == AuroraColourSettings.Cinematic)
            {
                __result = new Color(0.392156869f, 0.5882353f, 0.980392158f, 1f);

                Main.Logger.Log(FlaggedLoggingLevel.Debug, $"Aurora Color retrieved from AuroraManager.GetAuroraColour(), current color is {__result}");
                return;
            }

            white.r = Mathf.Lerp(GameManager.GetAuroraManager().m_RedTint.GetMin(),         GameManager.GetAuroraManager().m_RedTint.GetMax(),          Mathf.Sin(GameManager.GetAuroraManager().m_ColorTimers.x));
            white.g = Mathf.Lerp(GameManager.GetAuroraManager().m_GreenTint.GetMin(),       GameManager.GetAuroraManager().m_GreenTint.GetMax(),        Mathf.Sin(GameManager.GetAuroraManager().m_ColorTimers.y));
            white.b = Mathf.Lerp(GameManager.GetAuroraManager().m_BlueTint.GetMin(),        GameManager.GetAuroraManager().m_BlueTint.GetMax(),         Mathf.Sin(GameManager.GetAuroraManager().m_ColorTimers.z));
            white.a = Mathf.Lerp(GameManager.GetAuroraManager().m_AlphaControl.GetMin(),    GameManager.GetAuroraManager().m_AlphaControl.GetMax(),     Mathf.Sin(GameManager.GetAuroraManager().m_ColorTimers.w) ) * Mathf.Pow(normalizedAlpha, 2);

            __result = white;

            Main.Logger.Log(FlaggedLoggingLevel.Debug, $"Aurora Color retrieved from AuroraManager.GetAuroraColour(), current color is {__result}");
            return;
        }
    }

    [HarmonyPatch(typeof(InteriorLightingManager), nameof(InteriorLightingManager.GetAuroraColours))]
    //[HarmonyAfter(nameof(AuroraManager_GetAuroraColour))]
    internal class InteriorLightingManager_GetAuroraColours
    {
        private static bool Prefix()
        {
            return false;
        }
        private static void Postfix(InteriorLightingManager __instance, ref Color __result)
        {
            Color auroraColour = GameManager.GetAuroraManager().GetAuroraColour();
            float normalizedAlpha = GameManager.GetAuroraManager().GetNormalizedAlpha();

            if (Main.SettingsInstance.AuroraColour == AuroraColourSettings.Cinematic)
            {
                GameManager.GetAuroraManager().SetCinematicColours( true );

                Main.Logger.Log(FlaggedLoggingLevel.Debug, $"Aurora Color retrieved from InteriorLightingManager.GetAuroraColour(), current color is {__result}");
                return;
            }
            else if (Main.SettingsInstance.AuroraColour == AuroraColourSettings.Custom)
            {
                Color White = Color.white;

                White.r = Main.SettingsInstance.AuroraColour_R;
                White.g = Main.SettingsInstance.AuroraColour_G;
                White.b = Main.SettingsInstance.AuroraColour_B;
                White.a = Mathf.Pow(normalizedAlpha, 2);

                __result = White;

                Main.Logger.Log(FlaggedLoggingLevel.Debug, $"Aurora Color retrieved from InteriorLightingManager.GetAuroraColour(), current color is {__result}");
                return;
            }
            else
            {
                auroraColour.r *= __instance.m_AuroraRed;
                auroraColour.g *= __instance.m_AuroraGreen;
                auroraColour.b *= __instance.m_AuroraBlue;

                __result = auroraColour;

                Main.Logger.Log(FlaggedLoggingLevel.Debug, $"Aurora Color retrieved from InteriorLightingManager.GetAuroraColour(), current color is {auroraColour}");
            }
        }
    }
}
