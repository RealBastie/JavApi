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

using org.apache.harmony.misc;

namespace biz.ritter.javapi.awt
{
    /**
     * @author Dmitry A. Durnev
     */
    [Serializable]
    public class Insets : java.lang.Cloneable, java.io.Serializable
    {

        private const long serialVersionUID = -2272572637695466749L;

        public int top;

        public int left;

        public int bottom;

        public int right;

        public Insets(int top, int left, int bottom, int right)
        {
            setValues(top, left, bottom, right);
        }

        public override int GetHashCode()
        {
            int hashCode = HashCode.EMPTY_HASH_CODE;
            hashCode = HashCode.combine(hashCode, top);
            hashCode = HashCode.combine(hashCode, left);
            hashCode = HashCode.combine(hashCode, bottom);
            hashCode = HashCode.combine(hashCode, right);
            return hashCode;
        }

        public Object clone()
        {
            return new Insets(top, left, bottom, right);
        }

        public override bool Equals(Object o)
        {
            if (o == this)
            {
                return true;
            }
            if (o is Insets)
            {
                Insets i = (Insets)o;
                return ((i.left == left) && (i.bottom == bottom) &&
                        (i.right == right) && (i.top == top));
            }
            return false;
        }

        public override String ToString()
        {
            /* The format is based on 1.5 release behavior 
             * which can be revealed by the following code:
             * System.out.println(new Insets(1, 2, 3, 4));
             */

            return (this.getClass().getName() +
                    "[left=" + left + ",top=" + top + //$NON-NLS-1$ //$NON-NLS-2$
                    ",right=" + right + ",bottom=" + bottom + "]"); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
        }

        public void set(int top, int left, int bottom, int right)
        {
            setValues(top, left, bottom, right);
        }

        private void setValues(int top, int left, int bottom, int right)
        {
            this.top = top;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
        }
    }
}