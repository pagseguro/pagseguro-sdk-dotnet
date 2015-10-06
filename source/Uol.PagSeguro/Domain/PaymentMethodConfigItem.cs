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


namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Payment method config
    /// </summary>
    public class PaymentMethodConfigItem
    {
        /// <summary>
        /// Payment method config value
        /// </summary>
        public double Value
        {
            get;
            set;
        }

        /// <summary>
        /// Payment method config key
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Payment method config key
        /// </summary>
        public string Group
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the PaymentMethodConfig class
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="group"></param>
        public PaymentMethodConfigItem(string key, double value, string group)
        {
            this.Key = key;
            this.Value = value;
            this.Group = group;
        }
    }
}
