namespace AuroraMonitor.WeatherNotificationPanel.WeatherIcons
{
    public class WeatherIconsLoader
    {
        public static AssetBundle? WeatherAssetBundle { get; } = Utilities.AssetBundleUtilities.LoadBundle("AuroraMonitor.WeatherNotificationPanel.weathericons");
        public static Dictionary<string, GameObject> WeatherPrefabs { get; set; } = new();
        public static bool IsLoaded { get; set; } = false;
        public static GameObject? GearMessageObject { get; set; }

        public static void LoadWeatherIcons()
        {
            Logger.Log($"LoadWeatherIcons");

            if (WeatherPrefabs.Count > 0)
            {
                Logger.Log($"WeatherPrefabs.Count > 0");
                WeatherPrefabs.Clear();
            }

            if (WeatherAssetBundle is null) return;

            GameObject DenseFog = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_DenseFog"));
            GameObject LightSnow = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_LightSnow"));
            GameObject HeavySnow = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_HeavySnow"));
            GameObject PartlyCloudy = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_PartlyCloudy"));
            GameObject ClearDay = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_ClearDay"));
            GameObject ClearNight = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_ClearNight"));
            GameObject Cloudy = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_Cloudy"));
            GameObject LightFog = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_LightFog"));
            GameObject Blizzard = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_Blizzard"));
            GameObject ClearAurora = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_ClearAurora"));
            GameObject ElectrostaticFog = UnityEngine.Object.Instantiate(WeatherAssetBundle.LoadAsset<GameObject>("ico_ElectrostaticFog"));

            //Texture2D DenseFog             = WeatherAssetBundle.LoadAsset<Texture2D>("DenseFog");
            //Texture2D LightSnow            = WeatherAssetBundle.LoadAsset<Texture2D>("LightSnow");
            //Texture2D HeavySnow            = WeatherAssetBundle.LoadAsset<Texture2D>("HeavySnow");
            //Texture2D PartlyCloudy         = WeatherAssetBundle.LoadAsset<Texture2D>("PartlyCloudy");
            //Texture2D ClearDay             = WeatherAssetBundle.LoadAsset<Texture2D>("ClearDay");
            //Texture2D ClearNight           = WeatherAssetBundle.LoadAsset<Texture2D>("ClearNight");
            //Texture2D Cloudy               = WeatherAssetBundle.LoadAsset<Texture2D>("Cloudy");
            //Texture2D LightFog             = WeatherAssetBundle.LoadAsset<Texture2D>("LightFog");
            //Texture2D Blizzard             = WeatherAssetBundle.LoadAsset<Texture2D>("Blizzard");
            //Texture2D ClearAurora          = WeatherAssetBundle.LoadAsset<Texture2D>("ClearAurora");
            //Texture2D ElectrostaticFog     = WeatherAssetBundle.LoadAsset<Texture2D>("ElectrostaticFog");

            WeatherPrefabs.Add("DenseFog", DenseFog);
            WeatherPrefabs.Add("LightSnow", LightSnow);
            WeatherPrefabs.Add("HeavySnow", HeavySnow);
            WeatherPrefabs.Add("PartlyCloudy", PartlyCloudy);
            WeatherPrefabs.Add("ClearDay", ClearDay);
            WeatherPrefabs.Add("ClearNight", ClearNight);
            WeatherPrefabs.Add("Cloudy", Cloudy);
            WeatherPrefabs.Add("LightFog", LightFog);
            WeatherPrefabs.Add("Blizzard", Blizzard);
            WeatherPrefabs.Add("ClearAurora", ClearAurora);
            WeatherPrefabs.Add("ElectrostaticFog", ElectrostaticFog);

            Logger.Log("All assets added");

            GearMessageObject = InterfaceManager.GetPanel<Panel_HUD>().m_Widget_GearMessage.gameObject;

            IsLoaded = true;
        }

        public static GameObject GetIconPrefab(string ID)
        {
            return WeatherPrefabs[ID];
        }
    }
}
