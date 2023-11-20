namespace AuroraMonitor
{
    public class Setup
    {
        public static bool Init()
        {
            SetupInstancedClasses();
            SetupConfigurationData();
            return true;
        }

        private static bool SetupFolders()
        {
            if (!Directory.Exists(Main.MonitorFolder))
            {
                Directory.CreateDirectory(Main.MonitorFolder);
            }

            return true;
        }

        private static bool SetupInstancedClasses()
        {
            Main.Config ??= new();
            Main.SettingsInstance ??= new();
            Main.MonitorData ??= new();

            return true;
        }

        private static bool SetupConfigurationData()
        {
            if (SetupFolders())
            {
                if (!File.Exists(Main.MonitorMainConfig))
                {
                    JsonFile.Save<WeatherMonitorData>(Main.MonitorMainConfig, Main.MonitorData);
                    Main.MonitorData = default;
                }
                else
                {
                    Main.MonitorData = JsonFile.Load<WeatherMonitorData>(Main.MonitorMainConfig);
                }

                return true;
            }

            return false;
        }
    }
}
