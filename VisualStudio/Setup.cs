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
				Main.MonitorData = JsonFile.Load<WeatherMonitorData>(Main.MonitorMainConfig, true);
				Main.Config = JsonFile.Load<MainConfig>(Main.MonitorConfig, true);
				return true;
			}

			return false;
		}
	}
}
