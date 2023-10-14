using AuroraMonitor.JSON;

namespace AuroraMonitor.Notifications
{
    [RegisterTypeInIl2Cpp]
    public class WeatherNotifications : MonoBehaviour
    {
        public WeatherNotifications(IntPtr intPtr) : base(intPtr) { }
        /// <summary>
        /// Will do all checks then display the notification
        /// </summary>
        /// <param name="force">ignore some checks, will not ignore checks that will throw errors</param>
        /// <remarks>
        /// <para>Checks not ignored:</para>
        /// <para><c>GameManager.GetPlayerManagerComponent() == null</c></para>
        /// <para><c>GameManager.GetPlayerManagerComponent().m_ControlMode == PlayerControlMode.Locked</c></para>
        /// <para><c>WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()) is null</c></para>
        /// <para><c>WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm()) is null</c></para>
        /// </remarks>
        /// <seealso cref="WeatherUtilities.GetCurrentWeatherIcon(UniStormWeatherSystem)"/>
        /// <seealso cref="WeatherUtilities.GetCurrentWeatherLoc(UniStormWeatherSystem)"/>
        /// <seealso cref="DisplayWeatherNotification"/>
        public static void MaybeDisplayWeatherNotification(bool force = false)
        {
            if (GameManager.GetPlayerManagerComponent() == null) return;
            if (GameManager.GetPlayerManagerComponent().m_ControlMode == PlayerControlMode.Locked) return;

            if ( (SceneUtilities.IsValidSceneForWeather(GameManager.m_ActiveScene) && GameManager.GetUniStorm().m_SecondsSinceLastWeatherChange >= Settings.Instance.WeatherNotificationsDelay ) || force)
            {
                if ((Main.MonitorData.Prev != GameManager.GetUniStorm().m_CurrentWeatherStage) || force)
                {
                    if (WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()) is null) return;
                    if (WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm()) is null) return;

                    if (BuildWeatherData())
                    {
                        DisplayWeatherNotification();
                        //UpdateFirstAidPanel();
                    }
                }
            }
        }

        // TODO: Convert to using localizations
        private static void DisplayWeatherNotification()
        {
            int WindSpeed = (int)GetCurrentUnits((int)Math.Ceiling(GameManager.GetWindComponent().GetSpeedMPH()));
            string units = GetCurrentUnitsString(0);
            string message = Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()));

            GearMessageUtilities.AddGearMessage(
                WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())!,
                "Weather Monitor",
                $"{message} | Wind Speed: {WindSpeed}{units}",
                Settings.Instance.WeatherNotificationsTime);
        }

        private static bool BuildWeatherData()
        {
            if (Main.MonitorData.m_WeatherInformation == null) Main.MonitorData.m_WeatherInformation = new();

            float day = GameManager.GetTimeOfDayComponent().GetDayNumber();
            float hour = GameManager.GetTimeOfDayComponent().GetHour();
            float minute = GameManager.GetTimeOfDayComponent().GetMinutes();

            DayInformation dayInformation = new()
            {
                Day         = (int)day,
                Hour        = (int)hour,
                Minute      = (int)minute
            };

            WeatherInformation weatherInformation = new()
            {
                m_DayInformation = dayInformation,
                m_WeatherStage = GameManager.GetUniStorm().m_CurrentWeatherStage,
                WindAngle = GameManager.GetWindComponent().GetWindAngleRelativeToPlayer(),
                WindPlayerMult = GameManager.GetPlayerMovementComponent().GetWindMovementMultiplier(),
                WindSpeed = WeatherUtilities.ConvertMilesKilometerHour(GameManager.GetWindComponent().GetSpeedMPH()),
                Temperature = GameManager.GetUniStorm().m_Temperature
            };

            Main.MonitorData.Prev = GameManager.GetUniStorm().m_CurrentWeatherStage;

            if (weatherInformation.m_DayInformation.Day != GameManager.GetUniStorm().m_DayCounter)
            {
                return false;
            }
            else
            {
                Main.MonitorData.m_WeatherInformation.Add(weatherInformation);

                JsonFile.Save<WeatherMonitorData>(Main.MonitorMainConfig, Main.MonitorData);

                return true;
            }
        }

        //public static void UpdateFirstAidPanel()
        //{
        //    //if (!InterfaceManager.GetPanel<Panel_FirstAid>().enabled) return;
        //    if (!SceneUtilities.IsScenePlayable()) return;

        //    if (UIUtilities.WeatherMonitorWeather != null && UIUtilities.WeatherMonitorWindSpeed != null && UIUtilities.WeatherMonitorWindSpeedUnit != null)
        //    {
        //        UILabel weatherlabel    = UIUtilities.WeatherMonitorWeather;
        //        UILabel speedlabel      = UIUtilities.WeatherMonitorWindSpeed;
        //        UILabel speedunitlabel  = UIUtilities.WeatherMonitorWindSpeedUnit;

        //        GameObject weather      = weatherlabel.gameObject;
        //        GameObject speed        = speedlabel.gameObject;
        //        GameObject speedunit    = speedunitlabel.gameObject;

        //        string CurrentSpeed     = GetNormalizedSpeed(GameManager.GetWindComponent().GetSpeedMPH()).ToString();
        //        string CurrentWeather   = Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()));
        //        string CurrentUnits     = GetCurrentUnitsString(1);

        //        NGUITools.SetActive(weather, false);
        //        UIUtilities.UpdateLabelText(weatherlabel, CurrentSpeed);
        //        NGUITools.SetActive(weather, true);

        //        NGUITools.SetActive(speed, false);
        //        UIUtilities.UpdateLabelText(speedlabel, CurrentWeather);
        //        NGUITools.SetActive(speed, true);

        //        NGUITools.SetActive(speedunit, false);
        //        UIUtilities.UpdateLabelText(speedunitlabel, CurrentUnits);
        //        NGUITools.SetActive(speedunit, true);
        //    }
        //}

        public static string GetDayString()
        {
            float result = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused() / 24f;
            return result.ToString();
        }

        public static float GetCurrentUnits(float input)
        {
            return Settings.Instance.UnitsToUse switch
            {
                Settings.UnitUse.Metric         => WeatherUtilities.ConvertMilesKilometerHour(input),
                Settings.UnitUse.Scientific     => WeatherUtilities.ConvertMilesMetersSecond(input),
                _ => input
            };
        }

        public static string GetCurrentUnitsString(int section)
        {
            if (section == 0)
            {
                return Settings.Instance.UnitsToUse switch
                {
                    Settings.UnitUse.Metric => "KM/H",
                    Settings.UnitUse.Scientific => "M/S",
                    _ => "MP/H"
                };
            }
            else if (section == 1)
            {
                return Settings.Instance.FirstAidScreen_UnitsToUse switch
                {
                    Settings.UnitUse.Metric => "KM/H",
                    Settings.UnitUse.Scientific => "M/S",
                    _ => "MP/H"
                };
            }

            return string.Empty;
        }

        public static int GetNormalizedSpeed(float input)
        {
            float num = GetCurrentUnits(input);
            float num1 = Mathf.Ceil(num);
            return (int)num1;
        }

        //public static void GetPreviousWeather()
        //{
        //    if ( Main.MonitorData.PreviousStages.Count == 0)
        //    {
        //        Logging.LogWarning("PreviousStages is empty");
        //    }

        //    Logging.LogSeperator();

        //    Logging.Log($"Previous Weather:", Color.cyan);

        //    foreach (WeatherStage stage in Main.MonitorData.PreviousStages )
        //    {
        //        Logging.Log($"{stage}");
        //    }

        //    Logging.LogSeperator();
        //}
    }
}
