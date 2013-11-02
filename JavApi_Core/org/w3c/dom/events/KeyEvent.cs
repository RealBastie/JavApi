/*
 * Copyright (c) 1999 World Wide Web Consortium,
 * (Massachusetts Institute of Technology, Institut National de Recherche
 *  en Informatique et en Automatique, Keio University).
 * All Rights Reserved. http://www.w3.org/Consortium/Legal/
 */
using System;
using org.w3c.dom.views;

namespace org.w3c.dom.events
{

    /**
     * The <code>KeyEvent</code> interface provides specific contextual  
     * information associated with Key events. 
     * @since DOM Level 2
     */
    public interface KeyEvent : UIEvent
    {

        /**
         *  <code>ctrlKey</code> indicates whether the 'ctrl' key was depressed 
         * during the firing of the event. 
         */
        bool getCtrlKey();
        /**
         *  <code>shiftKey</code> indicates whether the 'shift' key was depressed 
         * during the firing of the event. 
         */
        bool getShiftKey();
        /**
         *  <code>altKey</code> indicates whether the 'alt' key was depressed during 
         * the firing of the event.  On some platforms this key may map to an 
         * alternative key name. 
         */
        bool getAltKey();
        /**
         *  <code>metaKey</code> indicates whether the 'meta' key was depressed 
         * during the firing of the event.  On some platforms this key may map to 
         * an alternative key name. 
         */
        bool getMetaKey();
        /**
         *  The value of <code>keyCode</code> holds the virtual key code value of 
         * the key which was depressed if the event is a key event.  Otherwise, the 
         * value is zero. 
         */
        int getKeyCode();
        /**
         *  <code>charCode</code> holds the value of the Unicode character 
         * associated with the depressed key if the event is a key event.  
         * Otherwise, the value is zero. 
         */
        int getCharCode();
        /**
         * 
         * @param typeArg Specifies the event type.
         * @param canBubbleArg Specifies whether or not the event can bubble.
         * @param cancelableArg Specifies whether or not the event's default  action 
         *   can be prevent.
         * @param ctrlKeyArg Specifies whether or not control key was depressed 
         *   during the  <code>Event</code>.
         * @param altKeyArg Specifies whether or not alt key was depressed during 
         *   the  <code>Event</code>.
         * @param shiftKeyArg Specifies whether or not shift key was depressed 
         *   during the  <code>Event</code>.
         * @param metaKeyArg Specifies whether or not meta key was depressed during 
         *   the  <code>Event</code>.
         * @param keyCodeArg Specifies the <code>Event</code>'s <code>keyCode</code>
         * @param charCodeArg Specifies the <code>Event</code>'s 
         *   <code>charCode</code>
         * @param viewArg Specifies the <code>Event</code>'s 
         *   <code>AbstractView</code>.
         */
        void initKeyEvent(String typeArg,
                                               bool canBubbleArg,
                                               bool cancelableArg,
                                               bool ctrlKeyArg,
                                               bool altKeyArg,
                                               bool shiftKeyArg,
                                               bool metaKeyArg,
                                               int keyCodeArg,
                                               int charCodeArg,
                                               AbstractView viewArg);
    }

    public sealed class KeyEventConstants
    {
        // VirtualKeyCode
        public const int CHAR_UNDEFINED = 0x0FFFF;
        public const int DOM_VK_0 = 0x30;
        public const int DOM_VK_1 = 0x31;
        public const int DOM_VK_2 = 0x32;
        public const int DOM_VK_3 = 0x33;
        public const int DOM_VK_4 = 0x34;
        public const int DOM_VK_5 = 0x35;
        public const int DOM_VK_6 = 0x36;
        public const int DOM_VK_7 = 0x37;
        public const int DOM_VK_8 = 0x38;
        public const int DOM_VK_9 = 0x39;
        public const int DOM_VK_A = 0x41;
        public const int DOM_VK_ACCEPT = 0x1E;
        public const int DOM_VK_ADD = 0x6B;
        public const int DOM_VK_AGAIN = 0xFFC9;
        public const int DOM_VK_ALL_CANDIDATES = 0x0100;
        public const int DOM_VK_ALPHANUMERIC = 0x00F0;
        public const int DOM_VK_ALT = 0x12;
        public const int DOM_VK_ALT_GRAPH = 0xFF7E;
        public const int DOM_VK_AMPERSAND = 0x96;
        public const int DOM_VK_ASTERISK = 0x97;
        public const int DOM_VK_AT = 0x0200;
        public const int DOM_VK_B = 0x42;
        public const int DOM_VK_BACK_QUOTE = 0xC0;
        public const int DOM_VK_BACK_SLASH = 0x5C;
        public const int DOM_VK_BACK_SPACE = 0x08;
        public const int DOM_VK_BRACELEFT = 0xA1;
        public const int DOM_VK_BRACERIGHT = 0xA2;
        public const int DOM_VK_C = 0x43;
        public const int DOM_VK_CANCEL = 0x03;
        public const int DOM_VK_CAPS_LOCK = 0x14;
        public const int DOM_VK_CIRCUMFLEX = 0x0202;
        public const int DOM_VK_CLEAR = 0x0C;
        public const int DOM_VK_CLOSE_BRACKET = 0x5D;
        public const int DOM_VK_CODE_INPUT = 0x0102;
        public const int DOM_VK_COLON = 0x0201;
        public const int DOM_VK_COMMA = 0x2C;
        public const int DOM_VK_COMPOSE = 0xFF20;
        public const int DOM_VK_CONTROL = 0x11;
        public const int DOM_VK_CONVERT = 0x1C;
        public const int DOM_VK_COPY = 0xFFCD;
        public const int DOM_VK_CUT = 0xFFD1;
        public const int DOM_VK_D = 0x44;
        public const int DOM_VK_DEAD_ABOVEDOT = 0x86;
        public const int DOM_VK_DEAD_ABOVERING = 0x88;
        public const int DOM_VK_DEAD_ACUTE = 0x81;
        public const int DOM_VK_DEAD_BREVE = 0x85;
        public const int DOM_VK_DEAD_CARON = 0x8A;
        public const int DOM_VK_DEAD_CEDILLA = 0x8B;
        public const int DOM_VK_DEAD_CIRCUMFLEX = 0x82;
        public const int DOM_VK_DEAD_DIAERESIS = 0x87;
        public const int DOM_VK_DEAD_DOUBLEACUTE = 0x89;
        public const int DOM_VK_DEAD_GRAVE = 0x80;
        public const int DOM_VK_DEAD_IOTA = 0x8D;
        public const int DOM_VK_DEAD_MACRON = 0x84;
        public const int DOM_VK_DEAD_OGONEK = 0x8C;
        public const int DOM_VK_DEAD_SEMIVOICED_SOUND = 0x8F;
        public const int DOM_VK_DEAD_TILDE = 0x83;
        public const int DOM_VK_DEAD_VOICED_SOUND = 0x8E;
        public const int DOM_VK_DECIMAL = 0x6E;
        public const int DOM_VK_DELETE = 0x7F;
        public const int DOM_VK_DIVIDE = 0x6F;
        public const int DOM_VK_DOLLAR = 0x0203;
        public const int DOM_VK_DOWN = 0x28;
        public const int DOM_VK_E = 0x45;
        public const int DOM_VK_END = 0x23;
        public const int DOM_VK_ENTER = 0x0D;
        public const int DOM_VK_EQUALS = 0x3D;
        public const int DOM_VK_ESCAPE = 0x1B;
        public const int DOM_VK_EURO_SIGN = 0x0204;
        public const int DOM_VK_EXCLAMATION_MARK = 0x0205;
        public const int DOM_VK_F = 0x46;
        public const int DOM_VK_F1 = 0x70;
        public const int DOM_VK_F10 = 0x79;
        public const int DOM_VK_F11 = 0x7A;
        public const int DOM_VK_F12 = 0x7B;
        public const int DOM_VK_F13 = 0xF000;
        public const int DOM_VK_F14 = 0xF001;
        public const int DOM_VK_F15 = 0xF002;
        public const int DOM_VK_F16 = 0xF003;
        public const int DOM_VK_F17 = 0xF004;
        public const int DOM_VK_F18 = 0xF005;
        public const int DOM_VK_F19 = 0xF006;
        public const int DOM_VK_F2 = 0x71;
        public const int DOM_VK_F20 = 0xF007;
        public const int DOM_VK_F21 = 0xF008;
        public const int DOM_VK_F22 = 0xF009;
        public const int DOM_VK_F23 = 0xF00A;
        public const int DOM_VK_F24 = 0xF00B;
        public const int DOM_VK_F3 = 0x72;
        public const int DOM_VK_F4 = 0x73;
        public const int DOM_VK_F5 = 0x74;
        public const int DOM_VK_F6 = 0x75;
        public const int DOM_VK_F7 = 0x76;
        public const int DOM_VK_F8 = 0x77;
        public const int DOM_VK_F9 = 0x78;
        public const int DOM_VK_FINAL = 0x18;
        public const int DOM_VK_FIND = 0xFFD0;
        public const int DOM_VK_FULL_WIDTH = 0x00F3;
        public const int DOM_VK_G = 0x47;
        public const int DOM_VK_GREATER = 0xA0;
        public const int DOM_VK_H = 0x48;
        public const int DOM_VK_HALF_WIDTH = 0x00F4;
        public const int DOM_VK_HELP = 0x9C;
        public const int DOM_VK_HIRAGANA = 0x00F2;
        public const int DOM_VK_HOME = 0x24;
        public const int DOM_VK_I = 0x49;
        public const int DOM_VK_INSERT = 0x9B;
        public const int DOM_VK_INVERTED_EXCLAMATION_MARK = 0x0206;
        public const int DOM_VK_J = 0x4A;
        public const int DOM_VK_JAPANESE_HIRAGANA = 0x0104;
        public const int DOM_VK_JAPANESE_KATAKANA = 0x0103;
        public const int DOM_VK_JAPANESE_ROMAN = 0x0105;
        public const int DOM_VK_K = 0x4B;
        public const int DOM_VK_KANA = 0x15;
        public const int DOM_VK_KANJI = 0x19;
        public const int DOM_VK_KATAKANA = 0x00F1;
        public const int DOM_VK_KP_DOWN = 0xE1;
        public const int DOM_VK_KP_LEFT = 0xE2;
        public const int DOM_VK_KP_RIGHT = 0xE3;
        public const int DOM_VK_KP_UP = 0xE0;
        public const int DOM_VK_L = 0x4C;
        public const int DOM_VK_LEFT = 0x25;
        public const int DOM_VK_LEFT_PARENTHESIS = 0x0207;
        public const int DOM_VK_LESS = 0x99;
        public const int DOM_VK_M = 0x4D;
        public const int DOM_VK_META = 0x9D;
        public const int DOM_VK_MINUS = 0x2D;
        public const int DOM_VK_MODECHANGE = 0x1F;
        public const int DOM_VK_MULTIPLY = 0x6A;
        public const int DOM_VK_N = 0x4E;
        public const int DOM_VK_NONCONVERT = 0x1D;
        public const int DOM_VK_NUM_LOCK = 0x90;
        public const int DOM_VK_NUMBER_SIGN = 0x0208;
        public const int DOM_VK_NUMPAD0 = 0x60;
        public const int DOM_VK_NUMPAD1 = 0x61;
        public const int DOM_VK_NUMPAD2 = 0x62;
        public const int DOM_VK_NUMPAD3 = 0x63;
        public const int DOM_VK_NUMPAD4 = 0x64;
        public const int DOM_VK_NUMPAD5 = 0x65;
        public const int DOM_VK_NUMPAD6 = 0x66;
        public const int DOM_VK_NUMPAD7 = 0x67;
        public const int DOM_VK_NUMPAD8 = 0x68;
        public const int DOM_VK_NUMPAD9 = 0x69;
        public const int DOM_VK_O = 0x4F;
        public const int DOM_VK_OPEN_BRACKET = 0x5B;
        public const int DOM_VK_P = 0x50;
        public const int DOM_VK_PAGE_DOWN = 0x22;
        public const int DOM_VK_PAGE_UP = 0x21;
        public const int DOM_VK_PASTE = 0xFFCF;
        public const int DOM_VK_PAUSE = 0x13;
        public const int DOM_VK_PERIOD = 0x2E;
        public const int DOM_VK_PLUS = 0x0209;
        public const int DOM_VK_PREVIOUS_CANDIDATE = 0x0101;
        public const int DOM_VK_PRINTSCREEN = 0x9A;
        public const int DOM_VK_PROPS = 0xFFCA;
        public const int DOM_VK_Q = 0x51;
        public const int DOM_VK_QUOTE = 0xDE;
        public const int DOM_VK_QUOTEDBL = 0x98;
        public const int DOM_VK_R = 0x52;
        public const int DOM_VK_RIGHT = 0x27;
        public const int DOM_VK_RIGHT_PARENTHESIS = 0x020A;
        public const int DOM_VK_ROMAN_CHARACTERS = 0x00F5;
        public const int DOM_VK_S = 0x53;
        public const int DOM_VK_SCROLL_LOCK = 0x91;
        public const int DOM_VK_SEMICOLON = 0x3B;
        public const int DOM_VK_SEPARATER = 0x6C;
        public const int DOM_VK_SHIFT = 0x10;
        public const int DOM_VK_SLASH = 0x2F;
        public const int DOM_VK_SPACE = 0x20;
        public const int DOM_VK_STOP = 0xFFC8;
        public const int DOM_VK_SUBTRACT = 0x6D;
        public const int DOM_VK_T = 0x54;
        public const int DOM_VK_TAB = 0x09;
        public const int DOM_VK_U = 0x55;
        public const int DOM_VK_UNDEFINED = 0x0;
        public const int DOM_VK_UNDERSCORE = 0x020B;
        public const int DOM_VK_UNDO = 0xFFCB;
        public const int DOM_VK_UP = 0x26;
        public const int DOM_VK_V = 0x56;
        public const int DOM_VK_W = 0x57;
        public const int DOM_VK_X = 0x58;
        public const int DOM_VK_Y = 0x59;
        public const int DOM_VK_Z = 0x5A;

    }
}