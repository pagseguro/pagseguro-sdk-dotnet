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
    /// Represents a metadata item
    /// </summary>
    public class MetaDataItem
    {
        private string _value;

        /// <summary>
        /// key wich represents a metadata item
        /// </summary>
        public string Key 
        { 
            get; 
            set; 
        }
        
        /// <summary>
        /// Value of a metadata item
        /// </summary>
        public string Value 
        { 
            get { return this._value; } 
            set 
            {
                this._value = NormalizeParameter(value); 
            } 
        }
        
        /// <summary>
        /// group wich metadata item is inserted
        /// </summary>
        public int? Group 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Initializes a new instance of the MetaDataItem class
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public MetaDataItem(string key, string value)
        {
            this.Key = key;
            this.Value = this.NormalizeParameter(value);
        }

        /// <summary>
        /// Initializes a new instance of the MetaDataItem class
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="group"></param>
        public MetaDataItem(string key, string value, int? group) : this(key, value)
        {
            this.Group = group;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        private string NormalizeParameter(string parameterValue)
        {
            parameterValue = parameterValue.Trim().ToLower();

            switch (MetaDataItemKeys.GetItemDescriptionByKey(this.Key))
            {
                case "CPF do passageiro":
                    parameterValue = PagSeguroUtil.GetOnlyNumbers(parameterValue);
                    break;
                case "Tempo no jogo em dias":
                    parameterValue = PagSeguroUtil.GetOnlyNumbers(parameterValue);
                    break;
                case "Celular de recarga":
                    parameterValue = PagSeguroUtil.GetOnlyNumbers(parameterValue);
                    break;
                default :
                    break;
            }

            return parameterValue;
        }
    }
}
