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
    public class Container : Component
    {
        protected internal Dimension addInsets(Dimension size)
        {
            Insets insets = getInsets();
            size.width += insets.left + insets.right;
            size.height += insets.top + insets.bottom;

            return size;
        }
        public new Insets getInsets()
        {
            toolkit.lockAWT();
            try
            {
                return insets();
            }
            finally
            {
                toolkit.unlockAWT();
            }
        }
        /**
         * @deprecated
         */
        [Obsolete]
        public Insets insets()
        {
            toolkit.lockAWT();
            try
            {
                return getNativeInsets();
            }
            finally
            {
                toolkit.unlockAWT();
            }
        }
        internal Rectangle getClient()
        {
            Insets insets = getInsets();
            return new Rectangle(insets.left, insets.top, width - insets.right - insets.left, height
                    - insets.top - insets.bottom);
        }
    }
}