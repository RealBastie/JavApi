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

namespace org.apache.harmony.security.fortress
{

    /**
     *
     * This interface provides access to package visible api in java.security
     * @author Boris V. Kuznetsov
     */
    public interface SecurityAccess
    {
        /**
         * Access to Security.renumProviders()
         *
         */
        void renumProviders();

        /**
         * Access to Service.getAliases()
         * @param s
         * @return
         */
        java.util.Iterator<String> getAliases(java.security.Provider.Service s);

        /**
         * Access to Provider.getService(String type)
         * @param p
         * @param type
         * @return
         */
        java.security.Provider.Service getService(java.security.Provider p, String type);
    }
}