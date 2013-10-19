using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt.font
{
    public class TextAttribute : java.text.AttributedCharacterIteratorNS.Attribute
    {
        private static readonly long serialVersionUID = 7744112784117861702L;

        // set of available text attributes
        private static readonly java.util.Map<String, TextAttribute> attrMap = new java.util.HashMap<String, TextAttribute>();

        protected TextAttribute(String name)
            : base(name)
        {
            attrMap.put(name, this);
        }

        protected override Object readResolve()
        {// throws InvalidObjectException {
            TextAttribute result = attrMap.get(this.getName());
            if (result != null)
            {
                return result;
            }
            // awt.194=Unknown attribute name
            throw new java.io.InvalidObjectException("Unknown attribute name"); //$NON-NLS-1$
        }

        public static readonly TextAttribute BACKGROUND = new TextAttribute("background"); //$NON-NLS-1$

        public static readonly TextAttribute BIDI_EMBEDDING = new TextAttribute("bidi_embedding"); //$NON-NLS-1$

        public static readonly TextAttribute CHAR_REPLACEMENT = new TextAttribute("char_replacement"); //$NON-NLS-1$

        public static readonly TextAttribute FAMILY = new TextAttribute("family"); //$NON-NLS-1$

        public static readonly TextAttribute FONT = new TextAttribute("font"); //$NON-NLS-1$

        public static readonly TextAttribute FOREGROUND = new TextAttribute("foreground"); //$NON-NLS-1$

        public static readonly TextAttribute INPUT_METHOD_HIGHLIGHT = new TextAttribute(
                "input method highlight"); //$NON-NLS-1$

        public static readonly TextAttribute INPUT_METHOD_UNDERLINE = new TextAttribute(
                "input method underline"); //$NON-NLS-1$

        public static readonly TextAttribute JUSTIFICATION = new TextAttribute("justification"); //$NON-NLS-1$

        public static readonly java.lang.Float JUSTIFICATION_FULL = new java.lang.Float(1.0f);

        public static readonly java.lang.Float JUSTIFICATION_NONE = new java.lang.Float(0.0f);

        public static readonly TextAttribute NUMERIC_SHAPING = new TextAttribute("numeric_shaping"); //$NON-NLS-1$

        public static readonly TextAttribute POSTURE = new TextAttribute("posture"); //$NON-NLS-1$

        public static readonly java.lang.Float POSTURE_REGULAR = new java.lang.Float(0.0f);

        public static readonly java.lang.Float POSTURE_OBLIQUE = new java.lang.Float(0.20f);

        public static readonly TextAttribute RUN_DIRECTION = new TextAttribute("run_direction"); //$NON-NLS-1$

        public static readonly java.lang.Boolean RUN_DIRECTION_LTR = new java.lang.Boolean(false);

        public static readonly java.lang.Boolean RUN_DIRECTION_RTL = new java.lang.Boolean(true);

        public static readonly TextAttribute SIZE = new TextAttribute("size"); //$NON-NLS-1$

        public static readonly TextAttribute STRIKETHROUGH = new TextAttribute("strikethrough"); //$NON-NLS-1$

        public static readonly java.lang.Boolean STRIKETHROUGH_ON = new java.lang.Boolean(true);

        public static readonly TextAttribute SUPERSCRIPT = new TextAttribute("superscript"); //$NON-NLS-1$

        public static readonly java.lang.Integer SUPERSCRIPT_SUB = new java.lang.Integer(-1);

        public static readonly java.lang.Integer SUPERSCRIPT_SUPER = new java.lang.Integer(1);

        public static readonly TextAttribute SWAP_COLORS = new TextAttribute("swap_colors"); //$NON-NLS-1$

        public static readonly java.lang.Boolean SWAP_COLORS_ON = new java.lang.Boolean(true);

        public static readonly TextAttribute TRANSFORM = new TextAttribute("transform"); //$NON-NLS-1$

        public static readonly TextAttribute UNDERLINE = new TextAttribute("underline"); //$NON-NLS-1$

        public static readonly java.lang.Integer UNDERLINE_ON = new java.lang.Integer(0);

        public static readonly java.lang.Integer UNDERLINE_LOW_ONE_PIXEL = new java.lang.Integer(1);

        public static readonly java.lang.Integer UNDERLINE_LOW_TWO_PIXEL = new java.lang.Integer(2);

        public static readonly java.lang.Integer UNDERLINE_LOW_DOTTED = new java.lang.Integer(3);

        public static readonly java.lang.Integer UNDERLINE_LOW_GRAY = new java.lang.Integer(4);

        public static readonly java.lang.Integer UNDERLINE_LOW_DASHED = new java.lang.Integer(5);

        public static readonly TextAttribute WEIGHT = new TextAttribute("weight"); //$NON-NLS-1$

        public static readonly java.lang.Float WEIGHT_EXTRA_LIGHT = new java.lang.Float(0.5f);

        public static readonly java.lang.Float WEIGHT_LIGHT = new java.lang.Float(0.75f);

        public static readonly java.lang.Float WEIGHT_DEMILIGHT = new java.lang.Float(0.875f);

        public static readonly java.lang.Float WEIGHT_REGULAR = new java.lang.Float(1.0f);

        public static readonly java.lang.Float WEIGHT_SEMIBOLD = new java.lang.Float(1.25f);

        public static readonly java.lang.Float WEIGHT_MEDIUM = new java.lang.Float(1.5f);

        public static readonly java.lang.Float WEIGHT_DEMIBOLD = new java.lang.Float(1.75f);

        public static readonly java.lang.Float WEIGHT_BOLD = new java.lang.Float(2.0f);

        public static readonly java.lang.Float WEIGHT_HEAVY = new java.lang.Float(2.25f);

        public static readonly java.lang.Float WEIGHT_EXTRABOLD = new java.lang.Float(2.5f);

        public static readonly java.lang.Float WEIGHT_ULTRABOLD = new java.lang.Float(2.75f);

        public static readonly TextAttribute WIDTH = new TextAttribute("width"); //$NON-NLS-1$

        public static readonly java.lang.Float WIDTH_CONDENSED = new java.lang.Float(0.75f);

        public static readonly java.lang.Float WIDTH_SEMI_CONDENSED = new java.lang.Float(0.875f);

        public static readonly java.lang.Float WIDTH_REGULAR = new java.lang.Float(1.0f);

        public static readonly java.lang.Float WIDTH_SEMI_EXTENDED = new java.lang.Float(1.25f);

        public static readonly java.lang.Float WIDTH_EXTENDED = new java.lang.Float(1.5f);

    }

}