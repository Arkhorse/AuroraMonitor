using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuroraMonitor.GUI
{
    [RegisterTypeInIl2Cpp(false)]
    public class WindDisplay : MonoBehaviour
    {
        #region Constructors
        public WindDisplay() : base() { }
        public WindDisplay(IntPtr intPtr) : base(intPtr) { }
        #endregion
        #region Variables
        #endregion
        #region Objects
        private GameObject? AttachedObject { get; set; }
        private GameObject? WindDisplayObject { get; set; }
        private GameObject? WindDisplayObjectSprite { get; set; }
        private GameObject? WindDisplayObjectRelativeSprite { get; set; }
        private GameObject? WindDisplayObjectLabel { get; set; }

        private UISprite? WindDisplaySprite { get; set; }
        private UISprite? WindDisplayRelativeSprite { get; set; }
        private UILabel? WindDisplayLabel { get; set; }
        #endregion
        #region Methods
        public void Awake()
        {
            AttachedObject = base.gameObject;

            #region Constuct GameObjects
            WindDisplayObject                                           = new() { name = "WindDisplayObject",               layer = vp_Layer.UI };
            WindDisplayObjectSprite                                     = new() { name = "WindDisplayObjectSprite",         layer = vp_Layer.UI };
            WindDisplayObjectRelativeSprite                             = new() { name = "WindDisplayObjectRelativeSprite", layer = vp_Layer.UI };
            WindDisplayObjectLabel                                      = new() { name = "WindDisplayObjectLabel",          layer = vp_Layer.UI };
            #endregion
            #region Construct SubObjects
            WindDisplayObjectSprite.AddComponent<UISprite>();
            WindDisplayObjectRelativeSprite.AddComponent<UISprite>();
            WindDisplayObjectLabel.AddComponent<UILabel>();
            #endregion
            #region Set Parents
            WindDisplayObject.transform.SetParent(AttachedObject.transform);
            WindDisplayObjectSprite.transform.SetParent(WindDisplayObject.transform);
            WindDisplayObjectRelativeSprite.transform.SetParent(WindDisplayObject.transform);
            WindDisplayObjectLabel.transform.SetParent(WindDisplayObject.transform);
            #endregion
            #region Setup Positions
            WindDisplayObject.transform.localPosition                   = new();
            WindDisplayObjectSprite.transform.localPosition             = new();
            WindDisplayObjectRelativeSprite.transform.localPosition     = new();
            WindDisplayObjectLabel.transform.localPosition              = new();
            #endregion
            #region Assign SubObjects
            WindDisplaySprite                                           = WindDisplayObjectSprite.GetComponent<UISprite>();
            WindDisplayRelativeSprite                                   = WindDisplayObjectRelativeSprite.GetComponent<UISprite>();
            WindDisplayLabel                                            = WindDisplayObjectLabel.GetComponent<UILabel>();
            #endregion
            #region Setup SubObject Names
            WindDisplaySprite.name                                      = "WindDisplaySprite";
            WindDisplayRelativeSprite.name                              = "WindDisplayRelativeSprite";
            WindDisplayLabel.name                                       = "WindDisplayLabel";
            #endregion
            #region Setup SubObjects
            UserInterfaceUtilities.SetupUISprite(WindDisplaySprite, "icoMap_SprayPaint_Direction");
            UserInterfaceUtilities.SetupUISprite(WindDisplayRelativeSprite, "icoMap_SprayPaint_Direction");
            UserInterfaceUtilities.SetupLabel(WindDisplayLabel,
                                              string.Empty,
                                              FontStyle.Normal,
                                              UILabel.Crispness.Always,
                                              NGUIText.Alignment.Center,
                                              UILabel.Overflow.ResizeFreely,
                                              false,
                                              16,
                                              16,
                                              Color.white,
                                              true);
            #endregion
        }

        public void Update()
        {
            if (AttachedObject == null) return;
            if (WindDisplaySprite == null) return;
            if (WindDisplayLabel == null) return;

            NGUITools.SetActive(WindDisplayObject, false);

            // Need to use the negative of the result as otherwise its in the wrong direction
            WindDisplaySprite.transform.eulerAngles = new(0, 0, -GameManager.GetWindComponent().GetWindAngleRelativeToPlayer());

            WindDisplayLabel.text = string.Format("{0} {1}", WeatherUtilities.GetNormalizedSpeed(GameManager.GetWindComponent().GetSpeedMPH()), WeatherUtilities.GetCurrentUnitsString(1));

            NGUITools.SetActive(WindDisplayObject, AttachedObject.activeSelf);
        }
        #endregion
    }
}
