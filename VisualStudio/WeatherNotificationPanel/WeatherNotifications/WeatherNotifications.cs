namespace AuroraMonitor.Utilities
{
    public class WeatherNotifications
    {
        public static void MaybeDisplayWeatherNotification(bool force = false)
        {
            string scene = GameManager.m_ActiveScene;

            if (WeatherUtilities.IsValidSceneForWeather(scene) || force)
            {
                if (WeatherUtilities.Prev != GameManager.GetUniStorm().m_CurrentWeatherStage || force)
                {
                    string loc = Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()));

                    if (string.IsNullOrEmpty(loc))
                    {
                        Logger.LogError($"Something went wrong: {loc}");
                        return;
                    }

                    //GearMessage.AddMessage(icon, "Aurora Monitor", $"Weather: {loc}", Settings.Instance.WeatherNotificationsTime);

                    GearMessageUtilities.AddGearMessage((string)WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm()), "Weather Monitor", $"Weather: {loc}", WeatherSettings.Instance.WeatherNotificationsTime);

                    UpdateStages(GameManager.GetUniStorm().GetWeatherStage());
                    Logger.Log($"Weather Notification: Icon: {WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm())}, Loc: {loc}, Time: {WeatherSettings.Instance.WeatherNotificationsTime}");

                }
            }
        }

        public static void UpdateStages(WeatherStage current)
        {
            WeatherUtilities.Prev = current;
        }

        private static string? GetToxicFogIcon()
        {
            return WeatherSettings.Instance.fogImages switch
            {
                WeatherSettings.ToxicFogImages.l1 => "ico_toxicFog_L1",
                WeatherSettings.ToxicFogImages.l2 => "ico_toxicFog_L2",
                WeatherSettings.ToxicFogImages.l3 => "ico_toxicFog_L3",
                _ => null
            };
        }

        public static void AddGearMessage(GameObject icon, string header, string message, float time)
        {
            GearMessage.GearMessageInfo info = new(icon.name, header, message)
            {
                m_DisplayTime = time
            };

            GearMessage.AddMessageToQueue(InterfaceManager.GetPanel<Panel_HUD>(), info, false);
        }
    }
}
