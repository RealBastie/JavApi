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
using org.apache.harmony.awt.gl.color;

namespace biz.ritter.javapi.awt.color
{
    /**
     * @author Oleg V. Khaschansky
     */
    [Serializable]
    public class ICC_Profile : java.io.Serializable
    {

        // NOTE: Constant field values are noted in 1.5 specification.

        public const int CLASS_INPUT = 0;

        public const int CLASS_DISPLAY = 1;

        public const int CLASS_OUTPUT = 2;

        public const int CLASS_DEVICELINK = 3;

        public const int CLASS_COLORSPACECONVERSION = 4;

        public const int CLASS_ABSTRACT = 5;

        public const int CLASS_NAMEDCOLOR = 6;

        public const int icSigXYZData = 1482250784;

        public const int icSigLabData = 1281450528;

        public const int icSigLuvData = 1282766368;

        public const int icSigYCbCrData = 1497588338;

        public const int icSigYxyData = 1501067552;

        public const int icSigRgbData = 1380401696;

        public const int icSigGrayData = 1196573017;

        public const int icSigHsvData = 1213421088;

        public const int icSigHlsData = 1212961568;

        public const int icSigCmykData = 1129142603;

        public const int icSigCmyData = 1129142560;

        public const int icSigSpace2CLR = 843271250;

        public const int icSigSpace3CLR = 860048466;

        public const int icSigSpace4CLR = 876825682;

        public const int icSigSpace5CLR = 893602898;

        public const int icSigSpace6CLR = 910380114;

        public const int icSigSpace7CLR = 927157330;

        public const int icSigSpace8CLR = 943934546;

        public const int icSigSpace9CLR = 960711762;

        public const int icSigSpaceACLR = 1094929490;

        public const int icSigSpaceBCLR = 1111706706;

        public const int icSigSpaceCCLR = 1128483922;

        public const int icSigSpaceDCLR = 1145261138;

        public const int icSigSpaceECLR = 1162038354;

        public const int icSigSpaceFCLR = 1178815570;

        public const int icSigInputClass = 1935896178;

        public const int icSigDisplayClass = 1835955314;

        public const int icSigOutputClass = 1886549106;

        public const int icSigLinkClass = 1818848875;

        public const int icSigAbstractClass = 1633842036;

        public const int icSigColorantOrderTag = 1668051567;

        public const int icSigColorantTableTag = 1668051572;

        public const int icSigColorSpaceClass = 1936744803;

        public const int icSigNamedColorClass = 1852662636;

        public const int icPerceptual = 0;

        public const int icRelativeColorimetric = 1;

        public const int icSaturation = 2;

        public const int icAbsoluteColorimetric = 3;

        public const int icSigHead = 1751474532;

        public const int icSigAToB0Tag = 1093812784;

        public const int icSigAToB1Tag = 1093812785;

        public const int icSigAToB2Tag = 1093812786;

        public const int icSigBlueColorantTag = 1649957210;

        public const int icSigBlueMatrixColumnTag = 1649957210;

        public const int icSigBlueTRCTag = 1649693251;

        public const int icSigBToA0Tag = 1110589744;

        public const int icSigBToA1Tag = 1110589745;

        public const int icSigBToA2Tag = 1110589746;

        public const int icSigCalibrationDateTimeTag = 1667329140;

        public const int icSigCharTargetTag = 1952543335;

        public const int icSigCopyrightTag = 1668313716;

        public const int icSigCrdInfoTag = 1668441193;

        public const int icSigDeviceMfgDescTag = 1684893284;

        public const int icSigDeviceModelDescTag = 1684890724;

        public const int icSigDeviceSettingsTag = 1684371059;

        public const int icSigGamutTag = 1734438260;

        public const int icSigGrayTRCTag = 1800688195;

        public const int icSigGreenColorantTag = 1733843290;

        public const int icSigGreenMatrixColumnTag = 1733843290;

        public const int icSigGreenTRCTag = 1733579331;

        public const int icSigLuminanceTag = 1819635049;

        public const int icSigMeasurementTag = 1835360627;

        public const int icSigMediaBlackPointTag = 1651208308;

        public const int icSigMediaWhitePointTag = 2004119668;

        public const int icSigNamedColor2Tag = 1852009522;

        public const int icSigOutputResponseTag = 1919251312;

        public const int icSigPreview0Tag = 1886545200;

        public const int icSigPreview1Tag = 1886545201;

        public const int icSigPreview2Tag = 1886545202;

        public const int icSigProfileDescriptionTag = 1684370275;

        public const int icSigProfileSequenceDescTag = 1886610801;

        public const int icSigPs2CRD0Tag = 1886610480;

        public const int icSigPs2CRD1Tag = 1886610481;

        public const int icSigPs2CRD2Tag = 1886610482;

        public const int icSigPs2CRD3Tag = 1886610483;

        public const int icSigPs2CSATag = 1886597747;

        public const int icSigPs2RenderingIntentTag = 1886597737;

        public const int icSigRedColorantTag = 1918392666;

        public const int icSigRedMatrixColumnTag = 1918392666;

        public const int icSigRedTRCTag = 1918128707;

        public const int icSigScreeningDescTag = 1935897188;

        public const int icSigScreeningTag = 1935897198;

        public const int icSigTechnologyTag = 1952801640;

        public const int icSigUcrBgTag = 1650877472;

        public const int icSigViewingCondDescTag = 1987405156;

        public const int icSigViewingConditionsTag = 1986618743;

        public const int icSigChromaticAdaptationTag = 1667785060;

        public const int icSigChromaticityTag = 1667789421;

        public const int icHdrSize = 0;

        public const int icHdrCmmId = 4;

        public const int icHdrVersion = 8;

        public const int icHdrDeviceClass = 12;

        public const int icHdrColorSpace = 16;

        public const int icHdrPcs = 20;

        public const int icHdrDate = 24;

        public const int icHdrMagic = 36;

        public const int icHdrPlatform = 40;

        public const int icHdrProfileID = 84;

        public const int icHdrFlags = 44;

        public const int icHdrManufacturer = 48;

        public const int icHdrModel = 52;

        public const int icHdrAttributes = 56;

        public const int icHdrRenderingIntent = 64;

        public const int icHdrIlluminant = 68;

        public const int icHdrCreator = 80;

        public const int icICCAbsoluteColorimetric = 3;

        public const int icMediaRelativeColorimetric = 1;

        public const int icTagType = 0;

        public const int icTagReserved = 4;

        public const int icCurveCount = 8;

        public const int icCurveData = 12;

        public const int icXYZNumberX = 8;

        /**
         * Size of a profile header
         */
        private const int headerSize = 128;

        /**
         * header magic number
         */
        private const int headerMagicNumber = 0x61637370;

        // Cache of predefined profiles
        private static ICC_Profile sRGBProfile;
        private static ICC_Profile xyzProfile;
        private static ICC_Profile grayProfile;
        private static ICC_Profile pyccProfile;
        private static ICC_Profile linearRGBProfile;

        /**
         *  Handle to the current profile
         */
        [NonSerialized]
        private long profileHandle = 0;

        /**
         * If handle is used by another class
         * this object is not responsible for closing profile
         */
        [NonSerialized]
        private bool handleStolen = false;

        /**
         * Cached header data
         */
        [NonSerialized]
        private byte[] headerData = null;

        /**
         * Serialization support
         */
        [NonSerialized]
        private ICC_Profile openedProfileObject;

        public virtual int getColorSpaceType()
        {
            return csFromSignature(getIntFromHeader(icHdrColorSpace));
        }
        /**
         * Converts ICC color space signature to the java predefined
         * color space type
         * @param signature
         * @return
         */
        private int csFromSignature(int signature)
        {
            switch (signature)
            {
                case icSigRgbData:
                    return ColorSpace.TYPE_RGB;
                case icSigXYZData:
                    return ColorSpace.TYPE_XYZ;
                case icSigCmykData:
                    return ColorSpace.TYPE_CMYK;
                case icSigLabData:
                    return ColorSpace.TYPE_Lab;
                case icSigGrayData:
                    return ColorSpace.TYPE_GRAY;
                case icSigHlsData:
                    return ColorSpace.TYPE_HLS;
                case icSigLuvData:
                    return ColorSpace.TYPE_Luv;
                case icSigYCbCrData:
                    return ColorSpace.TYPE_YCbCr;
                case icSigYxyData:
                    return ColorSpace.TYPE_Yxy;
                case icSigHsvData:
                    return ColorSpace.TYPE_HSV;
                case icSigCmyData:
                    return ColorSpace.TYPE_CMY;
                case icSigSpace2CLR:
                    return ColorSpace.TYPE_2CLR;
                case icSigSpace3CLR:
                    return ColorSpace.TYPE_3CLR;
                case icSigSpace4CLR:
                    return ColorSpace.TYPE_4CLR;
                case icSigSpace5CLR:
                    return ColorSpace.TYPE_5CLR;
                case icSigSpace6CLR:
                    return ColorSpace.TYPE_6CLR;
                case icSigSpace7CLR:
                    return ColorSpace.TYPE_7CLR;
                case icSigSpace8CLR:
                    return ColorSpace.TYPE_8CLR;
                case icSigSpace9CLR:
                    return ColorSpace.TYPE_9CLR;
                case icSigSpaceACLR:
                    return ColorSpace.TYPE_ACLR;
                case icSigSpaceBCLR:
                    return ColorSpace.TYPE_BCLR;
                case icSigSpaceCCLR:
                    return ColorSpace.TYPE_CCLR;
                case icSigSpaceDCLR:
                    return ColorSpace.TYPE_DCLR;
                case icSigSpaceECLR:
                    return ColorSpace.TYPE_ECLR;
                case icSigSpaceFCLR:
                    return ColorSpace.TYPE_FCLR;
                default:
                    break;
            }

            // awt.165=Color space doesn't comply with ICC specification
            throw new java.lang.IllegalArgumentException("Color space doesn't comply with ICC specification"); //$NON-NLS-1$
        }

        /**
         * Reads integer from the profile header at the specified position
         * @param idx - offset in bytes from the beginning of the header
         * @return
         */
        private int getIntFromHeader(int idx)
        {
            if (headerData == null)
            {
                throw new java.lang.UnsupportedOperationException("getData not yet implemented");
                //headerData = getData(icSigHead);
            }

            return ((headerData[idx] & 0xFF) << 24) |
                      ((headerData[idx + 1] & 0xFF) << 16) |
                      ((headerData[idx + 2] & 0xFF) << 8) |
                      ((headerData[idx + 3] & 0xFF));
        }
        public virtual int getNumComponents()
        {
            switch (getIntFromHeader(icHdrColorSpace))
            {
                // The most common cases go first to increase speed
                case icSigRgbData:
                case icSigXYZData:
                case icSigLabData:
                    return 3;
                case icSigCmykData:
                    return 4;
                // Then all other
                case icSigGrayData:
                    return 1;
                case icSigSpace2CLR:
                    return 2;
                case icSigYCbCrData:
                case icSigLuvData:
                case icSigYxyData:
                case icSigHlsData:
                case icSigHsvData:
                case icSigCmyData:
                case icSigSpace3CLR:
                    return 3;
                case icSigSpace4CLR:
                    return 4;
                case icSigSpace5CLR:
                    return 5;
                case icSigSpace6CLR:
                    return 6;
                case icSigSpace7CLR:
                    return 7;
                case icSigSpace8CLR:
                    return 8;
                case icSigSpace9CLR:
                    return 9;
                case icSigSpaceACLR:
                    return 10;
                case icSigSpaceBCLR:
                    return 11;
                case icSigSpaceCCLR:
                    return 12;
                case icSigSpaceDCLR:
                    return 13;
                case icSigSpaceECLR:
                    return 14;
                case icSigSpaceFCLR:
                    return 15;
                default:
                    break;
            }

            // awt.160=Color space doesn't comply with ICC specification
            throw new ProfileDataException("Color space doesn't comply with ICC specification" //$NON-NLS-1$
            );
        }

        public virtual int getProfileClass()
        {
            int deviceClassSignature = getIntFromHeader(icHdrDeviceClass);

            switch (deviceClassSignature)
            {
                case icSigColorSpaceClass:
                    return CLASS_COLORSPACECONVERSION;
                case icSigDisplayClass:
                    return CLASS_DISPLAY;
                case icSigOutputClass:
                    return CLASS_OUTPUT;
                case icSigInputClass:
                    return CLASS_INPUT;
                case icSigLinkClass:
                    return CLASS_DEVICELINK;
                case icSigAbstractClass:
                    return CLASS_ABSTRACT;
                case icSigNamedColorClass:
                    return CLASS_NAMEDCOLOR;
                default:
                    break;
            }

            // Not an ICC profile class
            // awt.15F=Profile class does not comply with ICC specification
            throw new java.lang.IllegalArgumentException("Profile class does not comply with ICC specification"); //$NON-NLS-1$

        }

        public static ICC_Profile getInstance(int cspace)
        {
            //!++ TODO implement
            throw new java.lang.UnsupportedOperationException("Not yet implemented!");
        }
    }
}