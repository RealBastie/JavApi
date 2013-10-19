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

using org.apache.harmony.awt;

namespace biz.ritter.javapi.awt
{

    sealed internal class AWTListenerList<T> : ListenerList<T>
    {
        private const long serialVersionUID = -2622077171532840953L;

        private readonly Component owner;

        internal AWTListenerList() :
            base()
        {
            this.owner = null;
        }

        internal AWTListenerList(Component owner) :
            base()
        {
            this.owner = owner;
        }

        public void addUserListener(T listener)
        {
            base.addUserListener(listener);

            if (owner != null)
            {
                owner.deprecatedEventHandler = false;
            }
        }
    }
}