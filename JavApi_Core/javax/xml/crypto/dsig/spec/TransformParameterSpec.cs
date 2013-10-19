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
/*
 * Copyright 2005 Sun Microsystems, Inc. All rights reserved.
 */

using System;
using java = biz.ritter.javapi;

namespace biz.ritter.javapix.xml.crypto
{

/**
 * A specification of algorithm parameters for a {@link Transform} 
 * algorithm. The purpose of this interface is to group (and provide type 
 * safety for) all transform parameter specifications. All transform parameter 
 * specifications must implement this interface.
 *
 * @author Sean Mullan
 * @author JSR 105 Expert Group
 * @see Transform
 */
public interface TransformParameterSpec : java.security.spec.AlgorithmParameterSpec {}
}
