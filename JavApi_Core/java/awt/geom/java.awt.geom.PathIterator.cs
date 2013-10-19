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
using java = biz.ritter.javapi;
using System;

namespace biz.ritter.javapi.awt.geom
{
    /**
     * @author Denis M. Kishenko
     */

    public interface PathIterator
    {

        int getWindingRule();

        bool isDone();

        void next();

        int currentSegment(float[] coords);

        int currentSegment(double[] coords);

    }
    public sealed class PathIteratorConstants
    {
        public const int WIND_EVEN_ODD = 0;
        public const int WIND_NON_ZERO = 1;

        public const int SEG_MOVETO = 0;
        public const int SEG_LINETO = 1;
        public const int SEG_QUADTO = 2;
        public const int SEG_CUBICTO = 3;
        public const int SEG_CLOSE = 4;

    }

}