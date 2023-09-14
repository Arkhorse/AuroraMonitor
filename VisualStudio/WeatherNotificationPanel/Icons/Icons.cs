namespace AuroraMonitor.WeatherNotificationPanel.WeatherIcons
{
    public class WeatherIconsHolder
    {
        public string ID { get; set; }
        public GameObject Prefab { get; set; }

        public WeatherIconsHolder(string ID, GameObject Prefab)
        {
            this.ID = ID;
            this.Prefab = Prefab;
        }
    }
}
