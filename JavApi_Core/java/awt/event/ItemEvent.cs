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

namespace biz.ritter.javapi.awt.eventj
{
    /**
     * @author Michael Danilov
     */
    [Serializable]
    public class ItemEvent : AWTEvent
    {

        private const long serialVersionUID = -608708132447206933L;

        public const int ITEM_FIRST = 701;

        public const int ITEM_LAST = 701;

        public const int ITEM_STATE_CHANGED = 701;

        public const int SELECTED = 1;

        public const int DESELECTED = 2;

        private Object item;
        private int stateChange;

        public ItemEvent(ItemSelectable source, int id, Object item, int stateChange)
            : base(source, id)
        {
            this.item = item;
            this.stateChange = stateChange;
        }

        public Object getItem()
        {
            return item;
        }

        public int getStateChange()
        {
            return stateChange;
        }

        public ItemSelectable getItemSelectable()
        {
            return (ItemSelectable)source;
        }


        public String paramString()
        {
            /* The format is based on 1.5 release behavior 
             * which can be revealed by the following code:
             * 
             * Checkbox c = new Checkbox("Checkbox", true);
             * ItemEvent e = new ItemEvent(c, ItemEvent.ITEM_STATE_CHANGED, 
             *                             c, ItemEvent.SELECTED);
             * System.out.println(e);
             */

            String stateString = null;

            switch (stateChange)
            {
                case SELECTED:
                    stateString = "SELECTED"; //$NON-NLS-1$
                    break;
                case DESELECTED:
                    stateString = "DESELECTED"; //$NON-NLS-1$
                    break;
                default:
                    stateString = "unknown type"; //$NON-NLS-1$
                    break;
            }

            return ((id == ITEM_STATE_CHANGED ? "ITEM_STATE_CHANGED" : "unknown type") + //$NON-NLS-1$ //$NON-NLS-2$
                    ",item=" + item + ",stateChange=" + stateString); //$NON-NLS-1$ //$NON-NLS-2$
        }

    }
}