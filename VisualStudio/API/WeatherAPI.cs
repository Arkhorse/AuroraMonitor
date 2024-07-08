//namespace AuroraMonitor.API
//{
//	/*
//	How to use:

//	Create a new instance of this class EG:
//		public static WeatherAPI m_WeatherAPI { get; } = new(BuildInfo.Name);
//	Then you can call the functions, like:
//		m_WeatherAPI.ForceWeather(WeatherStage.Clear, true);
//	When you want to stop using the API (IE: when the user decides to remove your mod), call m_WeatherAPI.Uninstall();
//		This will revert any relevant changes back to the defaults

//	ForceWeather(WeatherStage, bool);
//		This method supports the ability to set weather without making it static, force it and make it static or to set a random weather
//		If you want to set a random stage, just call it like this:
//			ForceWeather();
//	*/
//	public class WeatherAPI
//	{
//		#region instance variables
//		public string? Name { get; private set; }
//		private bool WeatherStageStatic { get; set; } = false;
//		#endregion

//		/// <summary>
//		/// 
//		/// </summary>
//		/// <param name="stage"></param>
//		/// <param name="static"></param>
//		/// <returns></returns>
//		public bool ForceWeather(WeatherStage stage = WeatherStage.Undefined, bool @static = false)
//		{
//			Main.Logger.Log(FlaggedLoggingLevel.Debug, $"API Call for: {Name}:ForceWeather({stage}, {@static})");

//			if (GameManager.GetWeatherTransitionComponent() != null)
//			{
//				WeatherTransition transition = GameManager.GetWeatherTransitionComponent();

//				if (stage != WeatherStage.Undefined && @static)
//				{
//					transition.ForceUnmanagedWeatherStage(stage, 0f);


//					WeatherStageStatic = true;
//					return true;
//				}
//				else if (stage == WeatherStage.Undefined)
//				{
//					int weatherchoice = UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherStage)).Length - 2);
//					GameManager.GetWeatherTransitionComponent().ForceUnmanagedWeatherStage((WeatherStage)weatherchoice, 0f);

//					Main.Logger.Log(FlaggedLoggingLevel.Debug, $"API Call for: {Name}: random weather created, choice: {weatherchoice}");

//					return true;
//				}
//				else
//				{
//					if (stage == WeatherStage.ClearAurora && AuroraManager.m_ForceDisable)
//					{
//						AuroraManager.m_ForceDisable = false;
//						Main.Logger.Log(FlaggedLoggingLevel.Verbose, $"API Verbose Call for: {Name}: requested weather stage is Aurora and Aurora is force disabled. This will cause force disabled to be deactivated");
//					}

//					GameManager.GetWeatherTransitionComponent().ActivateWeatherSet(stage);
//					WeatherTransition.m_WeatherTransitionTimeScalar = 1f;
//				}
//			}

//			Main.Logger.Log(FlaggedLoggingLevel.Warning, $"API Call for: {Name}:ForceWeather() failed. Please ensure that you are calling them when \"GameManager.GetWeatherTransitionComponent()\" does not return null");
//			return false;
//		}

//		/// <summary>
//		/// Call this to uninstall your mods instance of the API. Currently the best way is to use a command to do this. See: <see cref="uConsole.RegisterCommand(string, string, uConsole.DebugCommand)"/>
//		/// </summary>
//		/// <returns>true when complete, false if it fails</returns>
//		public bool Uninstall()
//		{
//			Main.Logger.Log(FlaggedLoggingLevel.Debug, $"API Call for: {Name}:Uninstall()");
//			if (WeatherStageStatic)
//			{
//				GameManager.GetWeatherTransitionComponent().ActivateDefaultWeatherSet();
//				Main.RegisteredWeatherAPIs.Remove(this);
//				return true;
//			}

//			return false;
//		}

//		/// <summary>
//		/// 
//		/// </summary>
//		/// <param name="Name">The name of your mod or whatever you want to show when the API sends log messages</param>
//		public WeatherAPI(string Name)
//		{
//			this.Name = Name;

//			Main.RegisteredWeatherAPIs.Add(this);
//		}
//	}

//	public class WeatherAPIData : APIBase
//	{
//		public class WeatherAPIJSON
//		{
//			public class Entry
//			{
//				public string? Name { get; private set; }
//			}
//		}

//		public static List<WeatherAPI> RegisteredAPIConsumers { get; } = new();

//		public override void MaybeSaveLocalData()
//		{

//		}

//		public override bool MaybeLoadLocalData()
//		{
//			throw new NotImplementedException();
//		}

//		public override void RegisterEntry(WeatherAPI weatherAPI)
//		{
//			throw new NotImplementedException();
//		}

//		public override void DeRegisterEntry()
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
