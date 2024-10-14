using System.Diagnostics;
using System.Text.RegularExpressions;

using Il2CppTMPro;

using UnityEngine.UI;
using AuroraMonitor.ModSettings;
using Il2Cpp;
using Harmony;

namespace AuroraMonitor.GUI.Addons.FirstAidPanel
{
	[RegisterTypeInIl2Cpp(false)]
	public class WeatherDisplay : MonoBehaviour
	{
#pragma warning disable CA1822
#pragma warning disable CS8618
		public WeatherDisplay() { }
		public WeatherDisplay(IntPtr pointer) : base(pointer) { }

		public AssetBundle Bundle;

		public GameObject AuroraMonitor_FirstAidPanel;
		public GameObject Panel;
		public GameObject CanvasObject;
		public GameObject WeatherItem;
		public GameObject WindGroup;

		public GameObject AuroraGroup;
		public GameObject WeatherGroup;

		public TextMeshProUGUI AuroraLoadingHeader;
		public Slider AuroraLoadingBar;
		public TextMeshProUGUI AuroraLoadingPercent;

		public TextMeshProUGUI CurrentWeather;
		public TextMeshProUGUI RemainingTime;

		public TextMeshProUGUI WindSpeed;
		public TextMeshProUGUI WindSpeedUnits;
		public Image WindAngle;
		public Image Torch;

		public Image WeatherIconImage;

		//public Dictionary<string, Sprite> WeatherSprites = new();
#pragma warning restore CS8618

		public void Awake()
		{
			Bundle = Main.LoadAssetBundle("AuroraMonitor.Resources.auroramonitor_firstaidpanel");
			AuroraMonitor_FirstAidPanel = Instantiate(Bundle.LoadAsset<GameObject>("Assets/AuroraMonitor_FirstAidPanel.prefab"), gameObject.transform);
			if (AuroraMonitor_FirstAidPanel != null)
			{
				Main.Logger.Log("AuroraMonitor_FirstAidPanel is not null", FlaggedLoggingLevel.Verbose);
				AuroraMonitor_FirstAidPanel.DontUnload();

				CanvasObject = AuroraMonitor_FirstAidPanel.transform.GetChild(0).gameObject;
				Panel = CanvasObject.transform.GetChild(0).gameObject;
				WeatherGroup = Panel.transform.GetChild(1).gameObject;

				WindGroup = Panel.transform.GetChild(2).gameObject;
				WindSpeed = WindGroup.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
				WindSpeedUnits = WindGroup.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
				WindAngle = WindGroup.transform.GetChild(2).gameObject.GetComponent<Image>();

				WeatherItem = Panel.transform.GetChild(3).gameObject;
				CurrentWeather = WeatherItem.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
				WeatherIconImage = WeatherItem.transform.GetChild(1).gameObject.GetComponent<Image>();
				RemainingTime = WeatherItem.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
				
				AuroraGroup = Panel.transform.GetChild(4).gameObject;
				AuroraLoadingHeader = AuroraGroup.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
				AuroraLoadingBar = AuroraGroup.transform.GetChild(1).gameObject.GetComponent<Slider>();
				AuroraLoadingPercent = AuroraGroup.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

				if (CanvasObject == null) Main.Logger.Log("CanvasObject is null", FlaggedLoggingLevel.Debug);
				if (AuroraGroup == null) Main.Logger.Log("AuroraGroup is null", FlaggedLoggingLevel.Debug);
				if (WeatherGroup == null) Main.Logger.Log("WeatherGroup is null", FlaggedLoggingLevel.Debug);
				if (WindGroup == null) Main.Logger.Log("WindGroup is null", FlaggedLoggingLevel.Debug);
			}
			else
			{
				Main.Logger.Log("AuroraMonitor_FirstAidPanel is null", FlaggedLoggingLevel.Critical);
			}
			WeatherDataTracker();
		}

		public void Update()
		{
			if (!Main.SettingsInstance.WeatherDisplayEnable)
			{
				AuroraMonitor_FirstAidPanel.active = false;
				return;
			}

			AuroraMonitor_FirstAidPanel.active = gameObject.activeSelf;
			if (!gameObject.activeSelf) return;
			AuroraGroupController(WeatherUtilities.AuroraChanging(GameManager.GetAuroraManager()) || GameManager.GetUniStorm().m_CurrentWeatherStage == WeatherStage.ClearAurora);
			WeatherGroupController(true);
			WindGroupController(!GameManager.GetWeatherComponent().IsIndoorEnvironment() && !GameManager.GetWeatherComponent().IsIndoorScene());
			WeatherIconController();
		}
		public void OnDestroy()
		{
			WeatherSetData.OnWeatherStageChanged -= SetWeatherStageAction;
			SetWeatherStageAction -= OnWeatherChange;
		}

		#region Group Controllers
		public void AuroraGroupController(bool enable)
		{
			AuroraGroup.active = enable;

			AuroraLoadingBar.value = WeatherUtilities.GetAlteredAlpha(GameManager.GetAuroraManager());
			AuroraLoadingPercent.SetText(AuroraLoadingBar.value.ToString("P0"));
		}
		public void WeatherGroupController(bool enable)
		{
			WeatherGroup.active = enable;
			CurrentWeather.SetText(Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm())));
			RemainingTime.SetText(GetRemainingTime());
		}
		public void WindGroupController(bool enable)
		{
			if (!enable)
			{
				WindAngle.transform.eulerAngles += new Vector3(0, 0, 3) * Time.deltaTime * 65;
			}
			else WindAngle.transform.eulerAngles = new(0, 0, -GameManager.GetWindComponent().GetWindAngleRelativeToPlayer());
			WindSpeed.SetText(GetNormalizedSpeed(GameManager.GetWindComponent().GetSpeedMPH()).ToString());
			WindSpeedUnits.SetText(GetCurrentUnitsString());
			//Torch.m_Color = ()
		}
		public void WeatherIconController()
		{
			if (Main.WeatherIcons != null)
			{
				Texture2D? weather = WeatherUtilities.GetWeatherTexture(GameManager.GetUniStorm().m_CurrentWeatherStage);
				
				if (weather != null)
				{
					Sprite sprite = Sprite.Create(weather, new Rect(0f, 0f, 64f, 64f), new(0.5f, 0.5f), 200f);
					Main.Logger.Log($"Updating texture to {weather.name}", FlaggedLoggingLevel.Verbose);
					WeatherIconImage.sprite = sprite;

					//WeatherSprites.Add(weather.name, sprite);
				}
			}
		}
		#endregion
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string GetRemainingTime()
		{
			string weather = GetWeatherString();
			string InProgress = GetInProgress(weather);
			string TotalTime = GetTotal(weather);
			TotalTime = TotalTime.Replace("hrs", "");

			Main.Logger.Log($"InProgress: {InProgress} TotalTime: {TotalTime}", FlaggedLoggingLevel.Debug);
			double Progress = TryConvertToDouble(InProgress);
			double Total = TryConvertToDouble(TotalTime);

			TimeSpan InProgressSpan = TimeSpan.FromHours(Progress);
			TimeSpan TotalSpan = TimeSpan.FromHours(Total);
			//double remaining = TotalSpan.TotalHours - InProgressSpan.TotalHours;

			long remainingticks = TotalSpan.Ticks - InProgressSpan.Ticks;
			TimeSpan remaininghours = TimeSpan.FromTicks(remainingticks);

			//var currentset = GameManager.GetWeatherTransitionComponent().m_CurrentWeatherSet;
			return remaininghours.Hours > 0 ? $"{remaininghours.Hours} Hours {remaininghours.Minutes} Minutes" : $"{remaininghours.Minutes} Minutes";
		}
		/// <summary>
		/// Gets the debug string from Weather Transition, matches the line for the current weather, then returns that value
		/// </summary>
		/// <returns>Regex parsed string for the currently active weather</returns>
		public string GetWeatherString()
		{
			string time = GameManager.GetWeatherTransitionComponent().GetDebugString();

			Match CurrentWeatherString = Regex.Match(time, @">{2}.*(\d.\d\dhrs)$", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
			var GroupMatch = CurrentWeatherString.Groups;
			var FromGroup = GroupMatch[0].Value; // this is the actual line
			return FromGroup;
		}
		/// <summary>
		/// Gets the total amount of time the weather will be active for
		/// </summary>
		/// <param name="weather">The string of the current weather, use <see cref="GetWeatherString"/></param>
		/// <returns>The total amount of time the weather will be active for</returns>
		public string GetTotal(string weather)
		{
			return Regex.Match(weather, @"(\d*.\d\d)\D\D\D", RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant).Groups[0].Value;
		}
		/// <summary>
		/// Gets the total amount of time the weather has been active for
		/// </summary>
		/// <param name="weather">The string of the current weather, use <see cref="GetWeatherString"/></param>
		/// <returns>The total amount of time the weather has been active for</returns>
		public string GetInProgress(string weather)
		{
			string pattern = @"(\d*.\d\d)(?:\/)";
			var match = Regex.Match(weather, pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant).Groups[0].Value;
			return match.Replace("/", "");
		}
		/// <summary>
		/// Attempts to convert a string to a double value
		/// </summary>
		/// <param name="value">The string value to convert to a double. Must be in a proper format</param>
		/// <returns>A <see langword="double"/> converted from the input string</returns>
		public double TryConvertToDouble(string value)
		{
			double result = 0d;
			try
			{
				result = Convert.ToDouble(value);
			}
			catch (FormatException e)
			{
				Main.Logger.Log($"Attempting to convert to double failed for {value}", FlaggedLoggingLevel.Exception, e);
			}
			return result;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public float GetCurrentUnits(float input)
		{
			return Main.SettingsInstance.UnitUse switch
			{
				UnitUse.Metric => WeatherUtilities.ConvertMilesKilometerHour(input),
				UnitUse.Scientific => WeatherUtilities.ConvertMilesMetersSecond(input),
				_ => input
			};
		}
		public string GetCurrentUnitsString()
		{
			return Main.SettingsInstance.UnitUse switch
			{
				UnitUse.Metric => "KM/H",
				UnitUse.Scientific => "M/S",
				_ => "MP/H"
			};
		}
		public int GetNormalizedSpeed(float input)
		{
			return (int)Mathf.Ceil(GetCurrentUnits(input));
		}

		public static Action<WeatherSetStage> SetWeatherStageAction;
		public static void WeatherDataTracker()
		{
			WeatherSetData.OnWeatherStageChanged += SetWeatherStageAction;
			SetWeatherStageAction += OnWeatherChange;
		}
		public static void OnWeatherChange(WeatherSetStage weatherSetStage)
		{
			if (Main.WeatherDataTracking == null) return;
			WeatherDataTrackingIndex index = new()
			{
				m_WeatherType = weatherSetStage.m_WeatherType,
				m_DurationMinMax = weatherSetStage.m_DurationMinMax,
				m_TransitionTimeMinMax = weatherSetStage.m_TransitionTimeMinMax,
				m_CurrentDuration = weatherSetStage.m_CurrentDuration,
				m_CurrentTransitionTime = weatherSetStage.m_CurrentTransitionTime,
				m_ElapsedTime = weatherSetStage.m_ElapsedTime,
				Temperature = GameManager.GetWeatherComponent().GetBaseTemperature()
			};

			Main.WeatherDataTracking.Weather.Add(GameManager.GetTimeOfDayComponent().GetDayNumber().ToString(), index);
			JsonFile.Save<WeatherDataTracking>(Main.WeatherTrackingFile, Main.WeatherDataTracking);
		}
	}
}
#pragma warning restore CA1822
