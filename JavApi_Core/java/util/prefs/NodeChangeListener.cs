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

namespace biz.ritter.javapi.util.prefs
{

    /**
     * This interface is used to handle preference node change events. The
     * implementation of this interface can be installed by the {@code Preferences}
     * instance.
     * 
     * @see Preferences
     * @see NodeChangeEvent
     * 
     * @since 1.4
     */
    public interface NodeChangeListener : EventListener
    {
        /**
         * This method gets called whenever a child node is added to another node.
         * 
         * @param e
         *            the node change event.
         */
        void childAdded(NodeChangeEvent e);

        /**
         * This method gets called whenever a child node is removed from another
         * node.
         * 
         * @param e
         *            the node change event.
         */
        void childRemoved(NodeChangeEvent e);
    }
}