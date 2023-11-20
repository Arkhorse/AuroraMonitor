using AuroraMonitor.JSON;
using AuroraMonitor.Utilities.Logger;

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
            if (!Main.IsPlayerAvailable(GameManager.GetPlayerManagerComponent())) return;

            if ( (SceneUtilities.IsValidSceneForWeather(GameManager.m_ActiveScene, Main.SettingsInstance.WeatherNotificationsIndoors) && GameManager.GetUniStorm().m_SecondsSinceLastWeatherChange >= Main.SettingsInstance.WeatherNotificationsDelay ) || force)
            {
                if (Main.MonitorData != null && (Main.MonitorData.Prev != GameManager.GetUniStorm().m_CurrentWeatherStage) || force)
                {
                    if (WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()) is null) return;
                    if (WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm()) is null) return;

                    if (BuildWeatherData())
                    {
                        DisplayWeatherNotification();
                        SaveWeatherData();
                    }
                }
            }
        }

        // TODO: Convert to using localizations
        private static void DisplayWeatherNotification()
        {
            string message = Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()));

            GearMessageUtilities.AddGearMessage(
                WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())!,
                "Weather Monitor",
                $"{message}",
                Main.SettingsInstance.WeatherNotificationsTime);

            Main.Logger.Log(FlaggedLoggingLevel.Debug, "Weather notification displayed");
        }

        private static bool BuildWeatherData()
        {
            if (Main.MonitorData == null) return false;

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
                m_DayInformation    = dayInformation,
                m_WeatherStage      = GameManager.GetUniStorm().m_CurrentWeatherStage,
                WindAngle           = GameManager.GetWindComponent().GetWindAngleRelativeToPlayer(),
                WindPlayerMult      = GameManager.GetPlayerMovementComponent().GetWindMovementMultiplier(),
                WindSpeed           = WeatherUtilities.ConvertMilesKilometerHour(GameManager.GetWindComponent().GetSpeedMPH()),
                Temperature         = GameManager.GetWeatherComponent().GetBaseTemperature()
            };

            Main.MonitorData.Prev = GameManager.GetUniStorm().m_CurrentWeatherStage;

            if (weatherInformation.m_DayInformation.Day != GameManager.GetUniStorm().m_DayCounter)
            {
                return false;
            }
            else
            {
                Main.Logger.Log(FlaggedLoggingLevel.Debug, "New weather data added to database");
                Main.MonitorData.m_WeatherInformation.Add(weatherInformation);
                return true;
            }
        }

        private static void SaveWeatherData()
        {
            JsonFile.Save<WeatherMonitorData>(Main.MonitorMainConfig, Main.MonitorData);
        }
    }
}
