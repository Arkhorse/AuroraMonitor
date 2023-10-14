namespace AuroraMonitor.Panel
{
    [RegisterTypeInIl2Cpp]
    public class FirstAidPanel_Addons : MonoBehaviour
    {
        public FirstAidPanel_Addons(IntPtr intPtr) : base(intPtr) { }

        public static int WeatherMonitorXOffset = 329;

        #region Objects
        internal GameObject? AttachedObject { get; set; }
        internal GameObject? WeatherMonitorHeader { get; set; }
        internal GameObject? WeatherMonitorWeather { get; set; }
        internal GameObject? WeatherMonitorWindSpeed { get; set; }
        internal GameObject? WeatherMonitorAurora { get; set; }

        internal UILabel? WeatherMonitorHeaderLabel { get; set; }
        internal UILabel? WeatherMonitorWeatherLabel { get; set; }
        internal UILabel? WeatherMonitorWindSpeedLabel { get; set; }
        internal UILabel? WeatherMonitorAuroraLabel { get; set; }

        internal Vector3 WeatherMonitorHeaderPosition { get; set; }                 = new(WeatherMonitorXOffset, -130, 0);
        internal Vector3 WeatherMonitorAuroraPosition { get; set; }                 = new(WeatherMonitorXOffset, -160, 0);
        internal Vector3 WeatherMonitorWeatherPosition{ get; set; }                 = new(WeatherMonitorXOffset, -190, 0);
        internal Vector3 WeatherMonitorWindSpeedPosition { get; set; }              = new(WeatherMonitorXOffset, -220, 0);
        
        #endregion

        public void Awake()
        {
            AttachedObject = base.gameObject;

            WeatherMonitorHeader                                    = new()
            {
                name                                                = "WeatherMonitorHeader"
            };
            WeatherMonitorWeather                                   = new()
            {
                name                                                = "WeatherMonitorWeather"
            };
            WeatherMonitorWindSpeed                                 = new()
            {
                name                                                = "WeatherMonitorWindSpeed"
            };
            WeatherMonitorAurora                                    = new()
            {
                name                                                = "WeatherMonitorAurora"
            };


            WeatherMonitorHeader.AddComponent<UILabel>();
            WeatherMonitorWeather.AddComponent<UILabel>();
            WeatherMonitorWindSpeed.AddComponent<UILabel>();
            WeatherMonitorAurora.AddComponent<UILabel>();

            WeatherMonitorHeader.transform.SetParent(AttachedObject.transform);
            WeatherMonitorWeather.transform.SetParent(AttachedObject.transform);
            WeatherMonitorWindSpeed.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAurora.transform.SetParent(AttachedObject.transform);

            WeatherMonitorHeaderLabel                                       = WeatherMonitorHeader.GetComponent<UILabel>();
            WeatherMonitorWeatherLabel                                      = WeatherMonitorWeather.GetComponent<UILabel>();
            WeatherMonitorWindSpeedLabel                                    = WeatherMonitorWindSpeed.GetComponent<UILabel>();
            WeatherMonitorAuroraLabel                                       = WeatherMonitorAurora.GetComponent<UILabel>();

            WeatherMonitorHeader.transform.localPosition                    = WeatherMonitorHeaderPosition;
            WeatherMonitorWeather.transform.localPosition                   = WeatherMonitorWeatherPosition;
            WeatherMonitorWindSpeed.transform.localPosition                 = WeatherMonitorWindSpeedPosition;
            WeatherMonitorAurora.transform.localPosition                    = WeatherMonitorAuroraPosition;

            WeatherMonitorHeaderLabel.name                                  = "WeatherMonitorHeaderLabel";
            WeatherMonitorWeatherLabel.name                                 = "WeatherMonitorWeatherLabel";
            WeatherMonitorWindSpeedLabel.name                               = "WeatherMonitorWindSpeedLabel";
            WeatherMonitorAuroraLabel.name                                  = "WeatherMonitorAuroraLabel";

            SetupLabel(WeatherMonitorHeaderLabel);
            SetupLabel(WeatherMonitorWeatherLabel);
            SetupLabel(WeatherMonitorWindSpeedLabel);
            SetupLabel(WeatherMonitorAuroraLabel);

            //SetupSprite(WeatherMonitorSpriteSprite);
        }

        public void Update()
        {
            if (AttachedObject == null) return;
            if (WeatherMonitorHeaderLabel == null) return;
            if (WeatherMonitorWeatherLabel == null) return;
            if (WeatherMonitorWindSpeedLabel == null) return;
            //if (WeatherMonitorSpriteSprite == null) return;
            if (WeatherMonitorAuroraLabel == null) return;

            Enable(false);

            WeatherMonitorHeaderLabel.text          = "Weather Monitor";
            WeatherMonitorHeaderLabel.capsLock      = true;

            WeatherMonitorWeatherLabel.text         = Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()));
            WeatherMonitorWindSpeedLabel.text       = string.Format("{0} {1}", WeatherUtilities.GetNormalizedSpeed(GameManager.GetWindComponent().GetSpeedMPH()).ToString(), WeatherUtilities.GetCurrentUnitsString(1));

            WeatherMonitorAuroraLabel.text          = "Aurora Fully Active";
            WeatherMonitorAuroraLabel.color         = Color.red;

            Enable(CanEnable());
            HandleAuroraAlert();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        public void Enable(bool enabled)
        {
            NGUITools.SetActive(WeatherMonitorHeader, enabled);
            NGUITools.SetActive(WeatherMonitorWeather, enabled);
            NGUITools.SetActive(WeatherMonitorWindSpeed, enabled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanEnable()
        {
            if (Settings.Instance.FirstAidScreen_Enabled)
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

            if (GameManager.GetAuroraManager().m_IsElectrolizerActive)
            {
                NGUITools.SetActive(WeatherMonitorAurora, AttachedObject.activeSelf);
            }
            else
            {
                NGUITools.SetActive(WeatherMonitorAurora, false);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite"></param>
        public void SetupSprite(UISprite sprite)
        {
            sprite.atlas                        = InterfaceManager.GetPanel<Panel_HUD>().m_Sprite_GearMessageIcon.atlas;
            sprite.alpha                        = 1f;
            sprite.color                        = Color.white;
            sprite.mSpriteSet                   = true;

            sprite.gameObject.layer             = vp_Layer.UI;
            sprite.width                        = 16;
            sprite.height                       = 16;

            sprite.mSprite                      = sprite.atlas.GetSprite(sprite.spriteName);
            //sprite.material = InterfaceManager.GetPanel<Panel_HUD>().m_Sprite_GearMessageIcon.material;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        public void SetupLabel(UILabel label)
        {
            if (AttachedObject != null)
            {
                label.ambigiousFont             = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.ambigiousFont;
                label.bitmapFont                = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.bitmapFont;
                label.font                      = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.font;
                label.fontStyle                 = FontStyle.Normal;
                label.fontSize                  = 16;
                
                label.alignment                 = NGUIText.Alignment.Justified;
                label.overflowMethod            = UILabel.Overflow.ResizeFreely;
                label.multiLine                 = false;
                label.depth                     = 16;

                label.keepCrispWhenShrunk       = UILabel.Crispness.Always;

                label.gameObject.layer          = vp_Layer.UI;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="font"></param>
        /// <param name="fontStyle"></param>
        /// <param name="crispness"></param>
        /// <param name="alignment"></param>
        /// <param name="overflow"></param>
        /// <param name="mulitLine"></param>
        /// <param name="depth"></param>
        /// <param name="fontSize"></param>
        /// <param name="parent"></param>
        public void SetupLabel(
            UILabel label,
            UIFont font,
            FontStyle fontStyle,
            UILabel.Crispness crispness,
            NGUIText.Alignment alignment,
            UILabel.Overflow overflow,
            bool mulitLine,
            int depth,
            int fontSize,
            Transform parent)
        {
            label.ambigiousFont         = font;
            label.bitmapFont            = font;
            label.font                  = font;
            label.fontStyle             = fontStyle;
            label.keepCrispWhenShrunk   = crispness;
            label.alignment             = alignment;
            label.overflowMethod        = overflow;
            label.multiLine             = mulitLine;
            label.depth                 = depth;
            label.fontSize              = fontSize;
            label.gameObject.transform.SetParent(parent);

            label.gameObject.layer      = vp_Layer.UI;
        }
    }
}
