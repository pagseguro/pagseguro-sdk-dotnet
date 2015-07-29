// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using Uol.PagSeguro.Util;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Represents a HolderDocument
    /// </summary>
    public class HolderDocument
    {
        /// <summary>
        /// Holder document type
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Holder document value
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
    	public HolderDocument(){

    	}

        /// <summary>
        /// Initializes a new instance of the SenderDocumet class
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public HolderDocument(string type, string value)
        {
            this.Type = type;
            this.Value = PagSeguroUtil.GetOnlyNumbers(value);
    	}

        /// <summary>
        /// Gets toString class
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "SenderDocument [type=" + this.Type + ", value=" + this.Value + "]";
        }

    }
}
