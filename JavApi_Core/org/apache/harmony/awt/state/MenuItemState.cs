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

namespace org.apache.harmony.awt.state
{
    /**
     * @author Pavel Dolgov
     */

    /**
     * State of menu item
     */

    public interface MenuItemState
    {

        String getText();
        java.awt.Rectangle getTextBounds();
        void setTextBounds(int x, int y, int w, int h);

        String getShortcut();
        java.awt.Rectangle getShortcutBounds();
        void setShortcutBounds(int x, int y, int w, int h);

        java.awt.Rectangle getItemBounds();
        void setItemBounds(int x, int y, int w, int h);

        bool isMenu();
        bool isChecked();
        bool isEnabled();

        bool isCheckBox();
        bool isSeparator();

        java.awt.Dimension getMenuSize();
    }
}