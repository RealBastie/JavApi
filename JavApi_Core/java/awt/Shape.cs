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

namespace biz.ritter.javapi.awt
{

    /**
     * Shape
     * @author Alexey A. Petrenko
     */
    public interface Shape
    {
         bool contains(double x, double y);

         bool contains(double x, double y, double w, double h);

         bool contains(java.awt.geom.Point2D point);

         bool contains(java.awt.geom.Rectangle2D r);

         Rectangle getBounds();

         java.awt.geom.Rectangle2D getBounds2D();

         java.awt.geom.PathIterator getPathIterator(java.awt.geom.AffineTransform at);

         java.awt.geom.PathIterator getPathIterator(java.awt.geom.AffineTransform at, double flatness);

         bool intersects(double x, double y, double w, double h);

         bool intersects(java.awt.geom.Rectangle2D r);
    }
}