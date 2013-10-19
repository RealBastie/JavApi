/*
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at 
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.awt
{
/**
 * @author Michael Danilov, Dmitry A. Durnev
 */
    [Serializable]
public sealed class ComponentOrientation : java.io.Serializable {
    private const long serialVersionUID = -4113291392143563828L;

    public static readonly ComponentOrientation LEFT_TO_RIGHT = new ComponentOrientation(true, true);

    public static readonly ComponentOrientation RIGHT_TO_LEFT = new ComponentOrientation(true, false);

    public static readonly ComponentOrientation UNKNOWN = new ComponentOrientation(true, true);

    private static readonly java.util.Set<String> rlLangs = new java.util.HashSet<String>(); //RIGHT_TO_LEFT languages

    private readonly bool horizontal;

    private readonly bool left2right;

    static ComponentOrientation(){
        rlLangs.add("ar"); //$NON-NLS-1$
        rlLangs.add("fa"); //$NON-NLS-1$
        rlLangs.add("iw"); //$NON-NLS-1$
        rlLangs.add("ur"); //$NON-NLS-1$
    }

    /**
     * @deprecated
     */
    [Obsolete]
    public static ComponentOrientation getOrientation(java.util.ResourceBundle bdl) {
        Object obj = null;
        try {
            obj = bdl.getObject("Orientation"); //$NON-NLS-1$
        }
        catch (java.util.MissingResourceException mre) {
            obj = null;
        }
        if (obj is ComponentOrientation) {
            return (ComponentOrientation) obj;
        }
        java.util.Locale locale = bdl.getLocale();
        if (locale == null) {
            locale = java.util.Locale.getDefault();
        }
        return getOrientation(locale);
    }

    public static ComponentOrientation getOrientation(java.util.Locale locale) {
        String lang = locale.getLanguage();
        return rlLangs.contains(lang) ? RIGHT_TO_LEFT : LEFT_TO_RIGHT;
    }

    private ComponentOrientation(bool hor, bool l2r) {
        horizontal = hor;
        left2right = l2r;
    }

    public bool isHorizontal() {
        return horizontal;
    }

    public bool isLeftToRight() {
        return left2right;
    }

}
}
