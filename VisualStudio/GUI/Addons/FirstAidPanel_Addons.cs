namespace AuroraMonitor.GUI.Addons
{
    [RegisterTypeInIl2Cpp]
    public class FirstAidPanel_Addons : MonoBehaviour
    {
        public FirstAidPanel_Addons(IntPtr intPtr) : base(intPtr) { }

        #region Variables
        public static int WeatherMonitorXOffset { get; }                = 329;
        public float WeatherMonitorAddonAuroraValue { get; set; }       = 0f;
        public float WeatherMonitorAddonAuroraMinimum { get; set; }     = 0.01f;
        public Vector3 WindDirection { get; set; }
        #endregion
        #region Objects
        // WeatherMonitorAddon
        // WeatherMonitorAddonObject

        internal GameObject? AttachedObject { get; set; }
        internal GameObject? WeatherMonitorAddonObject { get; set; }
        internal GameObject? WeatherMonitorAddonObjectHeader { get; set; }
        internal GameObject? WeatherMonitorAddonObjectWeather { get; set; }
        internal GameObject? WeatherMonitorAddonObjectWindSpeed { get; set; }
        internal GameObject? WeatherMonitorAddonObjectAurora { get; set; }
        internal GameObject? WeatherMonitorAddonObjectAuroraLoading { get; set; }
        internal GameObject? WeatherMonitorAddonObjectAuroraDissipating { get; set; }
        internal GameObject? WeatherMonitorAddonObjectWeatherSprite { get; set; }
        internal GameObject? WeatherMonitorAddonObjectWindSprite { get; set; }
        #endregion
        #region SubObjects
        internal UILabel? WeatherMonitorAddonHeaderLabel { get; set; }
        internal UILabel? WeatherMonitorAddonWeatherLabel { get; set; }
        internal UILabel? WeatherMonitorAddonWindSpeedLabel { get; set; }
        internal UILabel? WeatherMonitorAddonAuroraLabel { get; set; }
        internal UILabel? WeatherMonitorAddonAuroraLoadingLabel { get; set; }
        internal UILabel? WeatherMonitorAddonObjectAuroraDissipatingLabel { get; set; }
        internal UISprite? WeatherMonitorAddonWeatherSprite { get; set; }
        internal UISprite? WeatherMonitorAddonWindSprite { get; set; }
        #endregion
        #region Fonts
        UnityEngine.Object? AmbigiusFont { get; set; }
        UIFont? BitmapFont { get; set; }
        UIFont? Font { get; set; }
        #endregion
        #region Positions
        internal Vector3 WeatherMonitorAddonBasePosition { get; set; }          = new(WeatherMonitorXOffset, -130, 0);
        internal Vector3 WeatherMonitorAddonHeaderPosition { get; set; }        = new(WeatherMonitorXOffset, -130, 0);
        internal Vector3 WeatherMonitorAddonAuroraPosition { get; set; }        = new(WeatherMonitorXOffset, -160, 0);
        internal Vector3 WeatherMonitorAddonWeatherPosition { get; set; }       = new(WeatherMonitorXOffset, -190, 0);
        internal Vector3 WeatherMonitorAddonWindSpeedPosition { get; set; }     = new(WeatherMonitorXOffset, -220, 0);
        internal Vector3 WeatherMonitorAddonWeatherSpritePosition { get; set; } = new(240, -160, 0);
        internal Vector3 WeatherMonitorAddonWindSpritePosition { get; set; }    = new(440, -160,0);
        #endregion

        internal UIAtlas? BaseAtlas { get; set; }

        public void Awake()
        {
            AttachedObject = base.gameObject;

            WeatherMonitorAddonObject                   = new GameObject() { name = "WeatherMonitorAddonObject",                layer = vp_Layer.UI };
            DontDestroyOnLoad(WeatherMonitorAddonObject);

            WeatherMonitorAddonObjectHeader             = new() { name = "WeatherMonitorAddonObjectHeader",             layer = vp_Layer.UI };
            WeatherMonitorAddonObjectWeather            = new() { name = "WeatherMonitorAddonObjectWeather",            layer = vp_Layer.UI };
            WeatherMonitorAddonObjectWindSpeed          = new() { name = "WeatherMonitorAddonObjectWindSpeed",          layer = vp_Layer.UI };
            WeatherMonitorAddonObjectAurora             = new() { name = "WeatherMonitorAddonObjectAurora",             layer = vp_Layer.UI };
            WeatherMonitorAddonObjectAuroraLoading      = new() { name = "WeatherMonitorAddonObjectAuroraLoading",      layer = vp_Layer.UI };
            WeatherMonitorAddonObjectAuroraDissipating  = new() { name = "WeatherMonitorAddonObjectAuroraDissipating",  layer = vp_Layer.UI };
            WeatherMonitorAddonObjectWeatherSprite      = new() { name = "WeatherMonitorAddonObjectWeatherSprite",      layer = vp_Layer.UI };
            WeatherMonitorAddonObjectWindSprite         = new() { name = "WeatherMonitorAddonObjectWindSprite",         layer = vp_Layer.UI };

            AmbigiusFont                                = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.ambigiousFont;
            BitmapFont                                  = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.bitmapFont;
            Font                                        = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.font;
            BaseAtlas                                   = InterfaceManager.GetPanel<Panel_HUD>().m_AltFireGamepadButtonSprite.atlas;

            WeatherMonitorAddonObject.transform.SetParent(AttachedObject.transform);

            WeatherMonitorAddonObjectHeader.AddComponent<UILabel>();
            WeatherMonitorAddonObjectWeather.AddComponent<UILabel>();
            WeatherMonitorAddonObjectWindSpeed.AddComponent<UILabel>();
            WeatherMonitorAddonObjectAurora.AddComponent<UILabel>();
            WeatherMonitorAddonObjectAuroraLoading.AddComponent<UILabel>();
            WeatherMonitorAddonObjectAuroraDissipating.AddComponent<UILabel>();
            WeatherMonitorAddonObjectWeatherSprite.AddComponent<UISprite>();
            WeatherMonitorAddonObjectWindSprite.AddComponent<UISprite>();


            WeatherMonitorAddonObjectHeader.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectWeather.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectWindSpeed.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectAurora.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectAuroraLoading.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectAuroraDissipating.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectWeatherSprite.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectWindSprite.transform.SetParent(AttachedObject.transform);

            WeatherMonitorAddonHeaderLabel                          = WeatherMonitorAddonObjectHeader.GetComponent<UILabel>();
            WeatherMonitorAddonWeatherLabel                         = WeatherMonitorAddonObjectWeather.GetComponent<UILabel>();
            WeatherMonitorAddonWindSpeedLabel                       = WeatherMonitorAddonObjectWindSpeed.GetComponent<UILabel>();
            WeatherMonitorAddonAuroraLabel                          = WeatherMonitorAddonObjectAurora.GetComponent<UILabel>();
            WeatherMonitorAddonAuroraLoadingLabel                   = WeatherMonitorAddonObjectAuroraLoading.GetComponent<UILabel>();
            WeatherMonitorAddonObjectAuroraDissipatingLabel         = WeatherMonitorAddonObjectAuroraDissipating.GetComponent<UILabel>();
            WeatherMonitorAddonWeatherSprite                        = WeatherMonitorAddonObjectWeatherSprite.GetComponent<UISprite>();
            WeatherMonitorAddonWindSprite                           = WeatherMonitorAddonObjectWindSprite.GetComponent<UISprite>();

            WeatherMonitorAddonHeaderLabel.name                     = "WeatherMonitorAddonHeaderLabel";
            WeatherMonitorAddonWeatherLabel.name                    = "WeatherMonitorAddonWeatherLabel";
            WeatherMonitorAddonWindSpeedLabel.name                  = "WeatherMonitorAddonWindSpeedLabel";
            WeatherMonitorAddonAuroraLabel.name                     = "WeatherMonitorAddonAuroraLabel";
            WeatherMonitorAddonAuroraLoadingLabel.name              = "WeatherMonitorAddonAuroraLoadingLabel";
            WeatherMonitorAddonObjectAuroraDissipatingLabel.name    = "WeatherMonitorAddonObjectAuroraDissipatingLabel";
            WeatherMonitorAddonWeatherSprite.name                   = "WeatherMonitorAddonWeatherSprite";
            WeatherMonitorAddonWindSprite.name                      = "WeatherMonitorAddonWindSprite";

            WeatherMonitorAddonObjectAurora.transform.localPosition             = WeatherMonitorAddonAuroraPosition;
            WeatherMonitorAddonObjectAuroraLoading.transform.localPosition      = WeatherMonitorAddonAuroraPosition;
            WeatherMonitorAddonObjectAuroraDissipating.transform.localPosition  = WeatherMonitorAddonAuroraPosition;

            WeatherMonitorAddonObjectHeader.transform.localPosition             = WeatherMonitorAddonHeaderPosition;
            WeatherMonitorAddonObjectWeather.transform.localPosition            = WeatherMonitorAddonWeatherPosition;
            WeatherMonitorAddonObjectWindSpeed.transform.localPosition          = WeatherMonitorAddonWindSpeedPosition;
            WeatherMonitorAddonObjectWeatherSprite.transform.localPosition      = WeatherMonitorAddonWeatherSpritePosition;
            WeatherMonitorAddonObjectWindSprite.transform.localPosition         = WeatherMonitorAddonWindSpritePosition;


            UserInterfaceUtilities.SetupLabel(WeatherMonitorAddonHeaderLabel, "Weather Monitor",                           FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.white, true);
            UserInterfaceUtilities.SetupLabel(WeatherMonitorAddonWeatherLabel, string.Empty,                                   FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.white, false);
            UserInterfaceUtilities.SetupLabel(WeatherMonitorAddonWindSpeedLabel, string.Empty,                                 FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.white, false);
            UserInterfaceUtilities.SetupLabel(WeatherMonitorAddonAuroraLabel, "Aurora Full Active",                        FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.red, true);
            UserInterfaceUtilities.SetupLabel(WeatherMonitorAddonAuroraLoadingLabel, "Aurora Loading {0}",                 FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.red, true);
            UserInterfaceUtilities.SetupLabel(WeatherMonitorAddonObjectAuroraDissipatingLabel, "Aurora Dissipating {0}",   FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.yellow, true);

            UserInterfaceUtilities.SetupUISprite(WeatherMonitorAddonWeatherSprite, "ico_Warning");
            UserInterfaceUtilities.SetupUISprite(WeatherMonitorAddonWindSprite, "icoMap_SprayPaint_Direction");
        }

        public void Update()
        {
            if (AttachedObject == null) return;
            if (WeatherMonitorAddonWeatherLabel == null) return;
            if (WeatherMonitorAddonWindSpeedLabel == null) return;
            if (WeatherMonitorAddonAuroraLoadingLabel == null) return;
            if (WeatherMonitorAddonObjectAuroraDissipatingLabel == null) return;
            if (WeatherMonitorAddonObjectWeatherSprite == null) return;
            if (WeatherMonitorAddonObjectWindSprite == null) return;

            Enable(false);
            if (!InterfaceManager.GetPanel<Panel_FirstAid>().enabled) return;

            WindDirection = WeatherUtilities.GetWindDirection();
            WeatherMonitorAddonAuroraValue = GameManager.GetAuroraManager().GetNormalizedAlpha();

            if (WeatherMonitorAddonWeatherSprite != null)
            {
                string? weather = WeatherUtilities.GetCurrentWeatherIcon(GameManager.GetUniStorm());
                if (weather != null)
                {
                    WeatherMonitorAddonWeatherSprite.SetAtlasSprite(WeatherMonitorAddonWeatherSprite.atlas.GetSprite(weather));
                }
            }

            WeatherMonitorAddonWeatherLabel.text                    = Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()));
            WeatherMonitorAddonWindSpeedLabel.text                  = string.Format("{0} {1}", WeatherUtilities.GetNormalizedSpeed(GameManager.GetWindComponent().GetSpeedMPH()), WeatherUtilities.GetCurrentUnitsString(1));
            WeatherMonitorAddonAuroraLoadingLabel.text              = string.Format("Aurora Loading {0:P2}", GameManager.GetAuroraManager().GetNormalizedAlpha());
            WeatherMonitorAddonObjectAuroraDissipatingLabel.text    = string.Format("Aurora Dissipating {0:P2}", GameManager.GetAuroraManager().GetNormalizedAlpha());

            if (WeatherMonitorAddonWeatherSprite != null)
            {
                WeatherMonitorAddonWeatherSprite.height = 64;
                WeatherMonitorAddonWeatherSprite.width = 64;
                HandleAuroraAlert();
            }

            if (WeatherMonitorAddonWindSprite != null)
            {
                WeatherMonitorAddonWindSprite.height    = 64;
                WeatherMonitorAddonWindSprite.width     = 64;
                HandleWindDirection(WeatherMonitorAddonWindSprite);
            }

            Enable(CanEnable());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        public void Enable(bool enabled)
        {
            NGUITools.SetActive(WeatherMonitorAddonObjectHeader, enabled);
            NGUITools.SetActive(WeatherMonitorAddonObjectWeather, enabled);
            NGUITools.SetActive(WeatherMonitorAddonObjectWindSpeed, enabled);
            NGUITools.SetActive(WeatherMonitorAddonObjectWeatherSprite, enabled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanEnable()
        {
            if (Main.SettingsInstance.FirstAidScreen_Enabled)
            {
                if (AttachedObject != null && AttachedObject.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }

        public void HandleAuroraAlert()
        {
            if (AttachedObject == null) return;

            NGUITools.SetActive(WeatherMonitorAddonObjectAuroraLoading, false);
            NGUITools.SetActive(WeatherMonitorAddonObjectAuroraDissipating, false);
            NGUITools.SetActive(WeatherMonitorAddonObjectAurora, false);

            if (WeatherUtilities.IsAuroraFullyActive(GameManager.GetAuroraManager()))
            {
                NGUITools.SetActive(WeatherMonitorAddonObjectAuroraLoading, false);
                NGUITools.SetActive(WeatherMonitorAddonObjectAuroraDissipating, false);

                NGUITools.SetActive(WeatherMonitorAddonObjectAurora, AttachedObject.activeSelf);
            }
            else if (AuroraChanging())
            {
                NGUITools.SetActive(WeatherMonitorAddonObjectAurora, false);
                NGUITools.SetActive(WeatherMonitorAddonObjectAuroraLoading, AttachedObject.activeSelf);
            }
        }

        // Works
        public void HandleWindDirection(UISprite windDirectionSprite)
        {
            if (AttachedObject == null) return;
            NGUITools.SetActive(WeatherMonitorAddonObjectWindSprite, AttachedObject.activeSelf);

            // Need to use the negative of the result as otherwise its in the wrong direction
            windDirectionSprite.transform.eulerAngles = new(0, 0, -GameManager.GetWindComponent().GetWindAngleRelativeToPlayer());
        }

        /// <summary>
        /// Gets if the Aurora is either loading or dissipating
        /// </summary>
        public bool AuroraChanging()
        {
            bool min = GameManager.GetAuroraManager().GetNormalizedAlpha() > WeatherMonitorAddonAuroraMinimum;
            bool max = GameManager.GetAuroraManager().GetNormalizedAlpha() < GameManager.GetAuroraManager().m_FullyActiveValue;
            return min && max;
        }
    }
}
