namespace AuroraMonitor
{
	/// <summary>
	/// The Setup class is used to setup all file based stuff
	/// </summary>
	public class Setup
	{
		public async static Task<bool> Init()
		{
			if (SetupInstancedClasses())
			{
			 	if (await SetupConfigurationData())
				{
					if (LoadAssets())
					{
						Main.Logger.Log($"Init Completed", FlaggedLoggingLevel.Debug);
						return true;
					}
				}
				else
				{
					Main.Logger.Log($"Attempting to setup config data failed", FlaggedLoggingLevel.Critical);
				}
			}
			Main.Logger.Log($"Init Failed", FlaggedLoggingLevel.Debug);
			return false;
		}

		// All of these should use the ??= thingy to ensure prexisting data is kept
		private static bool SetupInstancedClasses()
		{
			Main.Config					??= new();
			Main.SettingsInstance		??= new();
			Main.MonitorData			??= new();
			Main.WeatherDataTracking	??= new();
			Main.WeatherIcons			??= new();

			return true;
		}

		private static async Task<bool> SetupConfigurationData()
		{
			if (SetupFolders())
			{
				if (await LoadAllFiles())
				{
					if (Main.MonitorData != null && Main.Config != null && Main.WeatherDataTracking != null)
					{
						Main.Logger.Log("All configs successfully loaded", FlaggedLoggingLevel.Verbose);
						return true;
					}
				}
			}

			Main.Logger.Log($"All or some configs failed to load: Monitor Data: {(Main.MonitorData == null ? "null" : "not null")} | Config: {(Main.Config == null ? "null" : "not null")} | Weather Tracking: {(Main.WeatherDataTracking == null ? "null" : "not null")}", FlaggedLoggingLevel.Warning);
			return false;
		}

		private async static Task<bool> LoadAllFiles()
		{
			Main.MonitorData			= await JsonFile.LoadAsync<WeatherMonitorData>(Main.MonitorMainConfig, true);
			Main.Config					= await JsonFile.LoadAsync<MainConfig>(Main.MonitorConfig, true);
			Main.WeatherDataTracking	= await JsonFile.LoadAsync<WeatherDataTracking>(Main.WeatherTrackingFile, true);

			return true;
		}

		private static bool LoadAssets()
		{
			foreach (string file in Main.WeatherIconNames)
			{
				Texture2D? texture = ImageUtilities.GetPNG("Monitor//Textures", file);
				if (texture != null)
				{
					texture.name = file;
					var _ = new TextureDefinition() { Name = file, Texture = texture };
					Main.WeatherIcons.Add(_);
				}
			}

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
	}
}
