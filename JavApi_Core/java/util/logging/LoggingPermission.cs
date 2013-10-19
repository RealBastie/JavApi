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

namespace biz.ritter.javapi.util.logging
{
/**
 * The permission required to control the logging when run with a
 * {@code SecurityManager}.
 */
public sealed class LoggingPermission : java.security.BasicPermission, java.security.Guard,
        java.io.Serializable {

    // for serialization compatibility with J2SE 1.4.2
    private const long serialVersionUID = 63564341580231582L;

    /**
     * Constructs a {@code LoggingPermission} object required to control the
     * logging. The {@code SecurityManager} checks the permissions.
     * <p>
     * {@code LoggingPermission} objects are created by the security policy code
     * and depends on the security policy file, therefore programmers shouldn't
     * normally use them directly.
     * </p>
     * 
     * @param name
     *            currently must be "control".
     * @param actions
     *            currently must be either {@code null} or the empty string.
     * @throws IllegalArgumentException
     *             if name null or different from {@code string} control.
     */
    public LoggingPermission(String name, String actions) :
        base(name, actions){
        if (!"control".equals(name)) { //$NON-NLS-1$
            // logging.6=Name must be "control".
            throw new java.lang.IllegalArgumentException("Name must be \"control\"."); //$NON-NLS-1$
        }
        if (null != actions && !"".equals(actions)) { //$NON-NLS-1$
            // logging.7=Actions must be either null or the empty string.
            throw new java.lang.IllegalArgumentException("Actions must be either null or the empty string."); //$NON-NLS-1$
        }
    }

}
}