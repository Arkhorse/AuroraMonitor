using UnityEngine.UI;

namespace AuroraMonitor.Panel
{
    [RegisterTypeInIl2Cpp]
    public class FirstAidPanel_Addons : MonoBehaviour
    {
        public FirstAidPanel_Addons(IntPtr intPtr) : base(intPtr) { }

        public static int WeatherMonitorXOffset = 329;

        #region Objects
        // WeatherMonitorAddon
        // WeatherMonitorAddonObject

        internal GameObject? AttachedObject { get; set; }
        internal GameObject? WeatherMonitorAddonObject { get; set; }
        internal GameObject? WeatherMonitorAddonObjectRectTransform{ get; set; }
        internal GameObject? WeatherMonitorAddonObjectCanvas { get; set; }
        internal GameObject? WeatherMonitorAddonObjectHeader { get; set; }
        internal GameObject? WeatherMonitorAddonObjectWeather { get; set; }
        internal GameObject? WeatherMonitorAddonObjectWindSpeed { get; set; }
        internal GameObject? WeatherMonitorAddonObjectAurora { get; set; }
        internal GameObject? WeatherMonitorAddonObjectAuroraLoading { get; set; }
        internal GameObject? WeatherMonitorAddonObjectAuroraDissipating { get; set; }
        #endregion
        #region SubObjects
        internal Canvas? WeatherMonitorAddonCanvas { get; set; }
        //internal RectTransform? WeatherMonitorAddonRectTransform { get; set; }

        //internal Image? WeatherMonitorAddonImage { get; set; }

        internal UILabel? WeatherMonitorAddonHeaderLabel { get; set; }
        internal UILabel? WeatherMonitorAddonWeatherLabel { get; set; }
        internal UILabel? WeatherMonitorAddonWindSpeedLabel { get; set; }
        internal UILabel? WeatherMonitorAddonAuroraLabel { get; set; }
        internal UILabel? WeatherMonitorAddonAuroraLoadingLabel { get; set; }
        internal UILabel? WeatherMonitorAddonObjectAuroraDissipatingLabel { get; set; }
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
        #endregion

        internal float WeatherMonitorAddonAuroraValue { get; set; }


        public void Awake()
        {
            AttachedObject = base.gameObject;

            WeatherMonitorAddonObject                   = new GameObject() { name = "WeatherMonitorAddonObject", layer = vp_Layer.UI };
            DontDestroyOnLoad(WeatherMonitorAddonObject);

            //WeatherMonitorAddonObjectRectTransform      = new() { name = "WeatherMonitorAddonObjectRectTransform", layer = vp_Layer.UI };
            //WeatherMonitorAddonObjectCanvas             = new() { name = "WeatherMonitorAddonObjectCanvas", layer = vp_Layer.UI };
            WeatherMonitorAddonObjectHeader             = new() { name = "WeatherMonitorAddonObjectHeader", layer = vp_Layer.UI };
            WeatherMonitorAddonObjectWeather            = new() { name = "WeatherMonitorAddonObjectWeather", layer = vp_Layer.UI };
            WeatherMonitorAddonObjectWindSpeed          = new() { name = "WeatherMonitorAddonObjectWindSpeed", layer = vp_Layer.UI };
            WeatherMonitorAddonObjectAurora             = new() { name = "WeatherMonitorAddonObjectAurora", layer = vp_Layer.UI };
            WeatherMonitorAddonObjectAuroraLoading      = new() { name = "WeatherMonitorAddonObjectAuroraLoading", layer = vp_Layer.UI };
            WeatherMonitorAddonObjectAuroraDissipating  = new() { name = "WeatherMonitorAddonObjectAuroraDissipating", layer = vp_Layer.UI };

            AmbigiusFont                                = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.ambigiousFont;
            BitmapFont                                  = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.bitmapFont;
            Font                                        = InterfaceManager.GetPanel<Panel_FirstAid>().m_AirTempLabel.font;

            WeatherMonitorAddonObject.transform.SetParent(AttachedObject.transform);

            //WeatherMonitorAddonObjectCanvas.AddComponent<Canvas>();
            //WeatherMonitorAddonObjectRectTransform.AddComponent<RectTransform>();
            //WeatherMonitorAddonObjectRectTransform.AddComponent<Image>();
            WeatherMonitorAddonObjectHeader.AddComponent<UILabel>();
            WeatherMonitorAddonObjectWeather.AddComponent<UILabel>();
            WeatherMonitorAddonObjectWindSpeed.AddComponent<UILabel>();
            WeatherMonitorAddonObjectAurora.AddComponent<UILabel>();
            WeatherMonitorAddonObjectAuroraLoading.AddComponent<UILabel>();
            WeatherMonitorAddonObjectAuroraDissipating.AddComponent<UILabel>();

            //WeatherMonitorAddonObjectCanvas.transform.SetParent(WeatherMonitorAddonObject.transform);
            //WeatherMonitorAddonObjectRectTransform.transform.SetParent(WeatherMonitorAddonObjectCanvas.transform);



            //WeatherMonitorAddonCanvas                       = WeatherMonitorAddonObjectCanvas.GetComponent<Canvas>();
            //WeatherMonitorAddonCanvas.renderMode            = RenderMode.ScreenSpaceOverlay;
            //WeatherMonitorAddonCanvas.pixelPerfect          = true;


            //WeatherMonitorAddonRectTransform                = WeatherMonitorAddonObjectRectTransform.GetComponent<RectTransform>();

            //WeatherMonitorAddonImage                        = WeatherMonitorAddonObjectRectTransform.GetComponent<Image>();
            //WeatherMonitorAddonImage.color                  = new Color(1f, 1f, 1f, 0.75f);
            //WeatherMonitorAddonImage.transform.SetParent(WeatherMonitorAddonObjectRectTransform.transform);

            WeatherMonitorAddonObjectHeader.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectWeather.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectWindSpeed.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectAurora.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectAuroraLoading.transform.SetParent(AttachedObject.transform);
            WeatherMonitorAddonObjectAuroraDissipating.transform.SetParent(AttachedObject.transform);

            WeatherMonitorAddonHeaderLabel                          = WeatherMonitorAddonObjectHeader.GetComponent<UILabel>();
            WeatherMonitorAddonWeatherLabel                         = WeatherMonitorAddonObjectWeather.GetComponent<UILabel>();
            WeatherMonitorAddonWindSpeedLabel                       = WeatherMonitorAddonObjectWindSpeed.GetComponent<UILabel>();
            WeatherMonitorAddonAuroraLabel                          = WeatherMonitorAddonObjectAurora.GetComponent<UILabel>();
            WeatherMonitorAddonAuroraLoadingLabel                   = WeatherMonitorAddonObjectAuroraLoading.GetComponent<UILabel>();
            WeatherMonitorAddonObjectAuroraDissipatingLabel         = WeatherMonitorAddonObjectAuroraDissipating.GetComponent<UILabel>();


            WeatherMonitorAddonHeaderLabel.name                     = "WeatherMonitorAddonHeaderLabel";
            WeatherMonitorAddonWeatherLabel.name                    = "WeatherMonitorAddonWeatherLabel";
            WeatherMonitorAddonWindSpeedLabel.name                  = "WeatherMonitorAddonWindSpeedLabel";
            WeatherMonitorAddonAuroraLabel.name                     = "WeatherMonitorAddonAuroraLabel";
            WeatherMonitorAddonAuroraLoadingLabel.name              = "WeatherMonitorAddonAuroraLoadingLabel";
            WeatherMonitorAddonObjectAuroraDissipatingLabel.name    = "WeatherMonitorAddonObjectAuroraDissipatingLabel";

            //WeatherMonitorAddonRectTransform.localScale     = new Vector3(1f, 1f, 1f);

            //WeatherMonitorAddonObjectCanvas.transform.localPosition                           = WeatherMonitorAddonBasePosition;

            //WeatherMonitorAddonObjectAurora.transform.localPosition                     = new Vector3(0, 0, 0);
            //WeatherMonitorAddonObjectHeader.transform.localPosition                     = new Vector3(0,-30,0);
            //WeatherMonitorAddonObjectWeather.transform.localPosition                    = new Vector3(0, -60, 0);
            //WeatherMonitorAddonObjectWindSpeed.transform.localPosition                  = new Vector3(0, -90, 0);

            WeatherMonitorAddonObjectAurora.transform.localPosition = WeatherMonitorAddonAuroraPosition;
            WeatherMonitorAddonObjectAuroraLoading.transform.localPosition = WeatherMonitorAddonAuroraPosition;
            WeatherMonitorAddonObjectAuroraDissipating.transform.localPosition = WeatherMonitorAddonAuroraPosition;

            WeatherMonitorAddonObjectHeader.transform.localPosition = WeatherMonitorAddonHeaderPosition;
            WeatherMonitorAddonObjectWeather.transform.localPosition = WeatherMonitorAddonWeatherPosition;
            WeatherMonitorAddonObjectWindSpeed.transform.localPosition = WeatherMonitorAddonWindSpeedPosition;


            SetupLabel(WeatherMonitorAddonHeaderLabel, "Weather Monitor",                           FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.white, true);
            SetupLabel(WeatherMonitorAddonWeatherLabel, string.Empty,                                   FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.white, false);
            SetupLabel(WeatherMonitorAddonWindSpeedLabel, string.Empty,                                 FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.white, false);
            SetupLabel(WeatherMonitorAddonAuroraLabel, "Aurora Full Active",                        FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.red, true);
            SetupLabel(WeatherMonitorAddonAuroraLoadingLabel, "Aurora Loading {0}",                 FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.red, true);
            SetupLabel(WeatherMonitorAddonObjectAuroraDissipatingLabel, "Aurora Dissipating {0}",   FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Center, UILabel.Overflow.ResizeFreely, false, 16, 16, Color.yellow, true);
            //SetupSprite(WeatherMonitorSpriteSprite);

            //WeatherMonitorAddonCanvas.enabled = true;
        }

        public void Update()
        {
            if (AttachedObject == null) return;
            if (WeatherMonitorAddonWeatherLabel == null) return;
            if (WeatherMonitorAddonWindSpeedLabel == null) return;
            if (WeatherMonitorAddonAuroraLoadingLabel == null) return;
            if (WeatherMonitorAddonObjectAuroraDissipatingLabel == null) return;

            Enable(false);

            WeatherMonitorAddonAuroraValue = GameManager.GetAuroraManager().GetNormalizedAlpha();

            WeatherMonitorAddonWeatherLabel.text                    = Localization.Get(WeatherUtilities.GetCurrentWeatherLoc(GameManager.GetUniStorm()));
            WeatherMonitorAddonWindSpeedLabel.text                  = string.Format("{0} {1}", WeatherUtilities.GetNormalizedSpeed(GameManager.GetWindComponent().GetSpeedMPH()), WeatherUtilities.GetCurrentUnitsString(1));
            WeatherMonitorAddonAuroraLoadingLabel.text              = string.Format("Aurora Loading {0:P2}", GameManager.GetAuroraManager().GetNormalizedAlpha());
            WeatherMonitorAddonObjectAuroraDissipatingLabel.text    = string.Format("Aurora Dissipating {0:P2}", GameManager.GetAuroraManager().GetNormalizedAlpha());

            Enable(CanEnable());
            HandleAuroraAlert();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        public void Enable(bool enabled)
        {
            //WeatherMonitorAddonCanvas.enabled = enabled;

            NGUITools.SetActive(WeatherMonitorAddonObjectHeader, enabled);
            NGUITools.SetActive(WeatherMonitorAddonObjectWeather, enabled);
            NGUITools.SetActive(WeatherMonitorAddonObjectWindSpeed, enabled);
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

            NGUITools.SetActive(WeatherMonitorAddonObjectAuroraLoading, false);
            NGUITools.SetActive(WeatherMonitorAddonObjectAuroraDissipating, false);
            NGUITools.SetActive(WeatherMonitorAddonObjectAurora, false);

            if (WeatherUtilities.IsAuroraFullyActive(GameManager.GetAuroraManager()))
            {
                NGUITools.SetActive(WeatherMonitorAddonObjectAuroraLoading, false);
                NGUITools.SetActive(WeatherMonitorAddonObjectAuroraDissipating, false);

                NGUITools.SetActive(WeatherMonitorAddonObjectAurora, AttachedObject.activeSelf);
            }
            else if (GameManager.GetAuroraManager().GetNormalizedAlpha() > 0.15 && GameManager.GetAuroraManager().GetNormalizedAlpha() < 0.95)
            {
                NGUITools.SetActive(WeatherMonitorAddonObjectAurora, false);

                if (WeatherMonitorAddonAuroraValue < GameManager.GetAuroraManager().GetNormalizedAlpha())
                {
                    NGUITools.SetActive(WeatherMonitorAddonObjectAuroraLoading, AttachedObject.activeSelf);
                }
                else if (WeatherMonitorAddonAuroraValue > GameManager.GetAuroraManager().GetNormalizedAlpha())
                {
                    NGUITools.SetActive(WeatherMonitorAddonObjectAuroraDissipating, AttachedObject.activeSelf);
                }
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
            string text,
            FontStyle fontStyle,
            UILabel.Crispness crispness,
            NGUIText.Alignment alignment,
            UILabel.Overflow overflow,
            bool mulitLine,
            int depth,
            int fontSize,
            Color color,
            bool capsLock)
        {
            label.text                      = text;
            label.ambigiousFont             = AmbigiusFont;
            label.bitmapFont                = BitmapFont;
            label.font                      = Font;

            label.fontStyle                 = fontStyle;
            label.keepCrispWhenShrunk       = crispness;
            label.alignment                 = alignment;
            label.overflowMethod            = overflow;
            label.multiLine                 = mulitLine;
            label.depth                     = depth;
            label.fontSize                  = fontSize;
            label.color                     = color;
            label.capsLock                  = capsLock;
        }
    }
}
