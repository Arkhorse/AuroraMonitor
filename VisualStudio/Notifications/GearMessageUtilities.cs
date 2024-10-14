namespace AuroraMonitor.Notifications
{
    public class GearMessageUtilities
    {
        public static void AddGearMessage(string prefab, string header, string message, float time)
        {
            GearMessage.AddMessage(prefab, header, message, time);
            Main.Logger.Log($"Added GearMessage with the following data: {prefab}, {header}, {message}, {time}", ComplexLogger.FlaggedLoggingLevel.Debug);
        }
    }
}
