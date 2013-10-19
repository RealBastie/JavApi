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
 */
using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapi.util.spi
{

    /**
     * The base class for all the locale related service provider interfaces (SPIs).
     * 
     * @since 1.6
     */
    public abstract class LocaleServiceProvider
    {
        /**
         * The constructor
         * 
         */
        protected LocaleServiceProvider()
        {
            // do nothing
        }

        /**
         * Gets all available locales that has localized objects or names from this
         * locale service provider.
         * 
         * @return all available locales that has localized objects or names from
         *         this locale service provider
         */
        public abstract java.util.Locale[] getAvailableLocales();
    }
}