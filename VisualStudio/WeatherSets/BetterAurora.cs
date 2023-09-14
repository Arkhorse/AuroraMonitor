using Il2Cpp;

namespace AuroraMonitor.WeatherSets
{
    public class BetterAurora : MonoBehaviour
    {
        public BetterAurora() : base() { }
        public BetterAurora(IntPtr intPtr) : base(intPtr) { }
        private static Weather GetWeather { get; }              = GameManager.GetWeatherComponent();
        private static UniStormWeatherSystem unistorm { get; }  = GameManager.GetUniStorm();
        private static Wind wind { get; }                       = GameManager.GetWindComponent();
        public void Activate()
        {
            wind.StartPhaseImmediate(WindDirection.North, WindStrength.Calm);
            unistorm.m_Temperature = (int)AuroraSettings.Instance.BetterAuroraTemperature;
            this.enabled = true;
        }
    }
}
