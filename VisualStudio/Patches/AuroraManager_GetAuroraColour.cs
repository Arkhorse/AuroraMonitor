namespace AuroraMonitor
{
    [HarmonyPatch(typeof(AuroraManager), nameof(AuroraManager.GetAuroraColour))]
    internal class AuroraManager_GetAuroraColour
    {
        private static bool Prefix()
        {
            if (GameManager.GetExperienceModeManagerComponent().IsChallengeActive()) return true; // mod not usable in challenges
            return false;
        }

        private static void Postfix(ref Color __result)
        {
            if (GameManager.GetExperienceModeManagerComponent().IsChallengeActive()) return; // mod not usable in challenges
            float normalizedAlpha = GameManager.GetAuroraManager().GetNormalizedAlpha();
            if ( (GameManager.GetAuroraManager().m_UseCinematicColours && Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Default) || Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic)
            {
                __result = new Color(0.392156869f, 0.5882353f, 0.980392158f, 1f);
            }
            if (Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Custom)
            {
                Color White = Color.white;

                White.r = Settings.Instance.AuroraColour_R;
                White.g = Settings.Instance.AuroraColour_G;
                White.b = Settings.Instance.AuroraColour_B;
                White.a = Settings.Instance.AuroraColour_A;
                if (Settings.Instance.AuroraColour_Normalize)
                {
                    White.a *= Mathf.Pow(normalizedAlpha, 2);
                }
                __result = White;
            }
            Color white = Color.white;
            white.r = Mathf.Lerp(GameManager.GetAuroraManager().m_RedTint.GetMin(),         GameManager.GetAuroraManager().m_RedTint.GetMax(),          Mathf.Sin(GameManager.GetAuroraManager().m_ColorTimers.x));
            white.g = Mathf.Lerp(GameManager.GetAuroraManager().m_GreenTint.GetMin(),       GameManager.GetAuroraManager().m_GreenTint.GetMax(),        Mathf.Sin(GameManager.GetAuroraManager().m_ColorTimers.y));
            white.b = Mathf.Lerp(GameManager.GetAuroraManager().m_BlueTint.GetMin(),        GameManager.GetAuroraManager().m_BlueTint.GetMax(),         Mathf.Sin(GameManager.GetAuroraManager().m_ColorTimers.z));
            white.a = Mathf.Lerp(GameManager.GetAuroraManager().m_AlphaControl.GetMin(),    GameManager.GetAuroraManager().m_AlphaControl.GetMax(),     Mathf.Sin(GameManager.GetAuroraManager().m_ColorTimers.w) ) * normalizedAlpha * normalizedAlpha;
            __result = white;
        }
    }

    [HarmonyPatch(typeof(InteriorLightingManager), nameof(InteriorLightingManager.GetAuroraColours))]
    //[HarmonyAfter(nameof(AuroraManager_GetAuroraColour))]
    internal class InteriorLightingManager_GetAuroraColours
    {
        private static bool Prefix()
        {
            if (GameManager.GetExperienceModeManagerComponent().IsChallengeActive()) return true; // mod not usable in challenges
            return false;
        }
        private static void Postfix(InteriorLightingManager __instance, ref Color __result)
        {
            Color auroraColour = GameManager.GetAuroraManager().GetAuroraColour();
            float normalizedAlpha = GameManager.GetAuroraManager().GetNormalizedAlpha();

            if (GameManager.GetAuroraManager().IsUsingCinematicColours())
            {
                __result = auroraColour;
            }
            else if (Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic)
            {
                GameManager.GetAuroraManager().SetCinematicColours( true );
            }
            else if (Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Custom)
            {
                Color White = Color.white;

                White.r = Settings.Instance.AuroraColour_R;
                White.g = Settings.Instance.AuroraColour_G;
                White.b = Settings.Instance.AuroraColour_B;
                White.a = Settings.Instance.AuroraColour_A;
                if (Settings.Instance.AuroraColour_Normalize)
                {
                    White.a *= Mathf.Pow(normalizedAlpha, 2);
                }
                __result = White;
            }
            else
            {
                auroraColour.r *= __instance.m_AuroraRed;
                auroraColour.g *= __instance.m_AuroraGreen;
                auroraColour.b *= __instance.m_AuroraBlue;
            }
        }
    }
}
