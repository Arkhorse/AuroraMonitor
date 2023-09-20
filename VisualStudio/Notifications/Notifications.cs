namespace AuroraMonitor.Notifications
{
    public static class WeatherNotifications
    {
        public static List<WeatherStage> PreviousStages { get; set; } = new List<WeatherStage>();

        public static void MaybeDisplayWeatherNotification(bool force = false)
        {
            if (GameManager.GetPlayerManagerComponent() == null) return;
            if (GameManager.GetPlayerManagerComponent().m_ControlMode == PlayerControlMode.Locked) return;

            if (WeatherUtilities.IsValidSceneForWeather(GameManager.m_ActiveScene) || force)
            {
                if (WeatherUtilities.Prev != GameManager.GetUniStorm().m_CurrentWeatherStage || force)
                {
                    if (WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()) is null) return;
                    if (WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm()) is null) return;

                    GearMessageUtilities.AddGearMessage(
                        WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())!,
                        "Weather Monitor",
                        $"Weather: {Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()))}",
                        Settings.Instance.WeatherNotificationsTime);

                    WeatherUtilities.UpdateStages(GameManager.GetUniStorm().m_CurrentWeatherStage);

                    if (PreviousStages.Count > 7)
                    {
                        PreviousStages.RemoveAt(0);
                    }

                    PreviousStages.Add(GameManager.GetUniStorm().m_CurrentWeatherStage);
                }
            }
        }

        public static void GetPreviousWeather()
        {
            if (PreviousStages.Count == 0)
            {
                Logger.LogWarning("PreviousStages is empty");
            }

            Logger.LogSeperator();

            Logger.Log($"Previous Weather:", Color.cyan);

            foreach (WeatherStage stage in PreviousStages)
            {
                Logger.Log($"{stage}");
            }

            Logger.LogSeperator();
        }
    }
}
