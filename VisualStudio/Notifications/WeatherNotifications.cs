namespace AuroraMonitor.Notifications
{
    public static class WeatherNotifications
    {
        public static void MaybeDisplayWeatherNotification(bool force = false)
        {
            if (GameManager.GetPlayerManagerComponent() == null) return;
            if (GameManager.GetPlayerManagerComponent().m_ControlMode == PlayerControlMode.Locked) return;

            if (WeatherUtilities.IsValidSceneForWeather(GameManager.m_ActiveScene) || force)
            {
                if ((Main.MonitorData.Prev != GameManager.GetUniStorm().m_CurrentWeatherStage) || force)
                {
                    if (WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()) is null) return;
                    if (WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm()) is null) return;

                    DisplayWeatherNotification();
                    BuildWeatherData();
                }
            }
        }

        private static void DisplayWeatherNotification()
        {
            int WindSpeed = (int)Math.Ceiling(GameManager.GetWindComponent().GetSpeedMPH());
            GearMessageUtilities.AddGearMessage(
                WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())!,
                "Weather Monitor",
                $"{Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()))} | Wind Speed: {WindSpeed} MPH",
                Settings.Instance.WeatherNotificationsTime);
        }

        private static void BuildWeatherData()
        {
            Main.MonitorData.Prev = GameManager.GetUniStorm().m_CurrentWeatherStage;
            Main.MonitorData.m_WeatherInformation.Add(
                new WeatherInformation()
                {
                    DayInformation = $"{GameManager.GetUniStorm().m_DayCounter}",
                    m_WeatherStage = GameManager.GetUniStorm().m_CurrentWeatherStage,
                    WindAngle = GameManager.GetWindComponent().GetWindAngleRelativeToPlayer(),
                    WindPlayerMult = GameManager.GetPlayerMovementComponent().GetWindMovementMultiplier(),
                    WindSpeed = GameManager.GetWindComponent().GetSpeedMPH()
                }
                );
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
