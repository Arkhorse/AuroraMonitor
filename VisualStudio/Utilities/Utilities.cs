using System.Diagnostics.CodeAnalysis;

namespace AuroraMonitor
{
    internal class Utilities
    {
        public static void AuroraMonitorMessage(string message, float messageTime, bool log = false)
        {
            if (message == null)            return;
            if (Main.AuroraActive) return;
            Main.AuroraActive = true;
            if (log) Logger.Log($"Aurora {message}");
            GearMessage.AddMessage("ico_journal", "Aurora Monitor", $"{message}", messageTime, false, true);
        }

        public static void UpdateAuroraColor()
        {
            if (Settings.Instance.AuroraColour == Settings.AuroraColourSettings.Cinematic) GameManager.GetAuroraManager().SetCinematicColours(true);
            else GameManager.GetAuroraManager().SetCinematicColours(false);
        }
    }
}