﻿/*
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using java = biz.ritter.javapi;

namespace biz.ritter.javapi.io
{
    public class FilterOutputStream : OutputStream
    {

        protected OutputStream outJ;

        public FilterOutputStream(java.io.OutputStream streamToFilter)
        {
            this.outJ = streamToFilter;
        }

        public override void write(int b)
        {
            outJ.write(b);
        }

        public override void flush()
        {
            outJ.flush();
        }
        public override void close()
        {
            try
            {
                outJ.flush();
            }
            catch (IOException ignored) { }
            outJ.close();
        }
    }
}
