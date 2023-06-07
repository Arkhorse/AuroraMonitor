namespace AuroraMonitor
{
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.Update))]
    internal class GameManager_Update
    {
        private static void Postfix()
        {
            if (GameManager.GetAuroraManager().GetNormalizedAlpha() > 0f)
            {
                Utilities.AuroraMonitorMessage("Active");
            }
            if (GameManager.GetAuroraManager().GetNormalizedAlpha() == 0f && AuroraMonitor.AuroraActive)
            {
                AuroraMonitor.AuroraActive = false;
            }
        }
    }
}