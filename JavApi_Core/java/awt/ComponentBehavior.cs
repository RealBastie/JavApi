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
     * @author Dmitry A. Durnev
     */
    //import org.apache.harmony.awt.wtk.NativeWindow;

    /**
     * The interface of the helper object that encapsulates the difference
     * between lightweight and heavyweight components. 
     */
    internal interface ComponentBehavior
    {

        void addNotify();

        void setBounds(int x, int y, int w, int h, int bMask);

        void setVisible(bool b);

        Graphics getGraphics(int translationX, int translationY, int width, int height);

        //NativeWindow getNativeWindow();

        bool isLightweight();

        void onMove(int x, int y);

        bool isOpaque();

        bool isDisplayable();

        void setEnabled(bool value);

        void removeNotify();

        void setZOrder(int newIndex, int oldIndex);

        bool setFocus(bool focus, Component opposite);
    }
}
