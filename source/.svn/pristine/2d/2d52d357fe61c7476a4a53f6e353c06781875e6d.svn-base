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
    /// Represents a ParameterItem
    /// </summary>
    public class ParameterItem
    {
        /// <summary>
        /// key wich represents a ParameterItem
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Value of a ParameterItem
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// group wich ParameterItem is inserted
        /// </summary>
        public int? Group
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the ParameterItem class
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public ParameterItem(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the ParameterItem class
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="group"></param>
        public ParameterItem(string key, string value, int? group) : this(key, value)
        {
            this.Group = group;
        }
    }
}
