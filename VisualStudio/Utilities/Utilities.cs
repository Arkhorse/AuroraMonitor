using System.Diagnostics.CodeAnalysis;

namespace AuroraMonitor
{
    internal class Utilities
    {
        // This is used to load prefab info of a GearItem
        // NOTE: This is a volitile method. Ensure it is always up to date as otherwise it can break anything tied to GearItem

        /// <summary>
        /// Gets the prefab for a GearItem
        /// </summary>
        /// <param name="name">The name of the GearItem</param>
        /// <returns></returns>
        [return: NotNullIfNotNull(nameof(name))]
        public static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<GearItem>();
        // This is used to load prefab info of a ToolsItem
        // NOTE: This is a volitile method. Ensure it is always up to date as otherwise it can break anything tied to GearItem

        /// <summary>
        /// Gets the prefab for a ToolsItem
        /// </summary>
        /// <param name="name">The name of the ToolsItem</param>
        /// <returns></returns>
        [return: NotNullIfNotNull(nameof(name))]
        public static ToolsItem GetToolItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<ToolsItem>();

        // use this to ensure that gear names are properly named
        [return: NotNullIfNotNull(nameof(name))]
        public static string? NormalizeName(string name) => name.Replace("(Clone)", "").Trim();

        public static void Log(string message, params object[] parameters) => MelonLogger.Msg($"{message}", parameters);
        public static void LogWarning(string message, params object[] parameters) => MelonLogger.Warning($"{message}", parameters);
        public static void LogError(string message, params object[] parameters) => MelonLogger.Error($"{message}", parameters);
        public static void LogSeperator() => Log("==============================================================================");

        // Aurora Monitor Utils

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