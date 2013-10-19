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
 *  
 *  Copyright © 2011 Sebastian Ritter
 */
using System;

namespace biz.ritter.javapi.awt.peer
{
    public interface FramePeer : WindowPeer
    {
        /// <summary>
        /// Set the title of frame
        /// </summary>
        /// <param name="newTitle">title</param>
        void setTitle(String newTitle);
        /// <summary>
        /// Set window dimension to optimal size
        /// </summary>
        void pack();
        /// <summary>
        /// Set the new windows dimension 
        /// </summary>
        /// <param name="width">width of window</param>
        /// <param name="height">height of window</param>
        void setSize(int width, int height);
    }
}
