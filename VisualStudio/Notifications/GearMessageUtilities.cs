namespace AuroraMonitor.Notifications
{
    public class GearMessageUtilities
    {
        public static void AddGearMessage(string prefab, string header, string message, float time)
        {
            GearMessage.AddMessage(prefab, header, message, time);
            if (Settings.Instance.PRINTDEBUGLOG) Logger.Log($"Added GearMessage with the following data: {prefab}, {header}, {message}, {time}");
        }
    }
}
