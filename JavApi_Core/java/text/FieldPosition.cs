﻿/*
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
using System.Text;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.text
{

    /**
     * Identifies fields in formatted strings. If a {@code FieldPosition} is passed
     * to the format method with such a parameter, then the indices will be set to
     * the start and end indices of the field in the formatted string.
     * <p>
     * A {@code FieldPosition} can be created by using the integer constants in the
     * various format classes (for example {@code NumberFormat.INTEGER_FIELD}) or
     * one of the fields of type {@code Format.Field}.
     * <p>
     * If more than one field information is needed, the method
     * {@link NumberFormat#formatToCharacterIterator(Object)} should be used.
     */
    public class FieldPosition {

        private int myField, beginIndex, endIndex;

        private FormatNS.Field myAttribute;

        /**
         * Constructs a new {@code FieldPosition} for the specified field.
         * 
         * @param field
         *            the field to identify.
         */
        public FieldPosition(int field) {
            myField = field;
        }

        /**
         * Constructs a new {@code FieldPosition} for the specified {@code Field}
         * attribute.
         * 
         * @param attribute
         *            the field attribute to identify.
         */
        public FieldPosition(FormatNS.Field attribute) {
            myAttribute = attribute;
            myField = -1;
        }

        /**
         * Constructs a new {@code FieldPosition} for the specified {@code Field}
         * attribute and field id.
         * 
         * @param attribute
         *            the field attribute to identify.
         * @param field
         *            the field to identify.
         */
        public FieldPosition(FormatNS.Field attribute, int field) {
            myAttribute = attribute;
            myField = field;
        }

        void clear() {
            beginIndex = endIndex = 0;
        }

        /**
         * Compares the specified object to this field position and indicates if
         * they are equal. In order to be equal, {@code object} must be an instance
         * of {@code FieldPosition} with the same field, begin index and end index.
         * 
         * @param object
         *            the object to compare with this object.
         * @return {@code true} if the specified object is equal to this field
         *         position; {@code false} otherwise.
         * @see #hashCode
         */
        
        public override bool Equals(Object obj) {
            if (!(obj is FieldPosition)) {
                return false;
            }
            FieldPosition pos = (FieldPosition) obj;
            return myField == pos.myField && myAttribute == pos.myAttribute
                    && beginIndex == pos.beginIndex && endIndex == pos.endIndex;
        }

        /**
         * Returns the index of the beginning of the field.
         * 
         * @return the first index of the field.
         */
        public int getBeginIndex() {
            return beginIndex;
        }

        /**
         * Returns the index one past the end of the field.
         * 
         * @return one past the index of the last character in the field.
         */
        public int getEndIndex() {
            return endIndex;
        }

        /**
         * Returns the field which is being identified.
         * 
         * @return the field constant.
         */
        public int getField() {
            return myField;
        }

        /**
         * Returns the attribute which is being identified.
         * 
         * @return the field.
         */
        public FormatNS.Field getFieldAttribute() {
            return myAttribute;
        }

        
        public override int GetHashCode() {
            int attributeHash = (myAttribute == null) ? 0 : myAttribute.GetHashCode();
            return attributeHash + myField * 10 + beginIndex * 100 + endIndex;
        }

        /**
         * Sets the index of the beginning of the field.
         * 
         * @param index
         *            the index of the first character in the field.
         */
        public void setBeginIndex(int index) {
            beginIndex = index;
        }

        /**
         * Sets the index of the end of the field.
         * 
         * @param index
         *            one past the index of the last character in the field.
         */
        public void setEndIndex(int index) {
            endIndex = index;
        }

        /**
         * Returns the string representation of this field position.
         * 
         * @return the string representation of this field position.
         */
        
        public override String ToString() {
            return this.GetType().Name + "[attribute=" + myAttribute + ", field=" //$NON-NLS-1$ //$NON-NLS-2$
                    + myField + ", beginIndex=" + beginIndex + ", endIndex=" //$NON-NLS-1$ //$NON-NLS-2$
                    + endIndex + "]"; //$NON-NLS-1$
        }
    }
}
