using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.U2D;

namespace AuroraMonitor.Utilities
{
    public class UserInterfaceUtilities
    {
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
        public static void SetupLabel(
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
            label.ambigiousFont             = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
            label.bitmapFont                = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
            label.font                      = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);

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

        public static void SetupUISprite(UISprite sprite, string spriteName)
        {
            UIAtlas baseAtlas           = InterfaceManager.GetPanel<Panel_HUD>().m_AltFireGamepadButtonSprite.atlas;
            UISpriteData spriteData     = baseAtlas.GetSprite(spriteName);

            sprite.atlas                = baseAtlas;
            sprite.spriteName           = spriteName;
            sprite.mSprite              = spriteData;
            sprite.mSpriteSet           = true;
            //sprite.alpha                = 1f;
            //sprite.color                = Color.white;
            //sprite.MakePixelPerfect();
            //sprite.enabled              = true;
        }
    }
}
