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

namespace biz.ritter.javapi.awt.image
{

    public sealed class ImageObserverConstants
    {

        public const int WIDTH = 1;

        public const int HEIGHT = 2;

        public const int PROPERTIES = 4;

        public const int SOMEBITS = 8;

        public const int FRAMEBITS = 16;

        public const int ALLBITS = 32;

        public const int ERROR = 64;

        public const int ABORT = 128;
    }

    public interface ImageObserver
    {
        bool imageUpdate(Image img, int infoflags, int x, int y,
                int width, int height);

    }

}