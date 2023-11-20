using AuroraMonitor.Utilities.Logger;

namespace AuroraMonitor.Notifications
{
    public class GearMessageUtilities
    {
        public static void AddGearMessage(string prefab, string header, string message, float time)
        {
            GearMessage.AddMessage(prefab, header, message, time);
            Main.Logger.Log(FlaggedLoggingLevel.Verbose, $"Added GearMessage with the following data: {prefab}, {header}, {message}, {time}");
        }
    }
}
