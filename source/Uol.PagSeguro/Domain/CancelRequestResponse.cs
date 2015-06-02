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
using System.Text;
using Uol.PagSeguro.Util;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Represents an cancel request result
    /// </summary>
    public class CancelRequestResponse
    {

        private string response = String.Empty;

        /// <summary>
        /// Initializes a new instance of the CancelRequestResponse class
        /// </summary>
        public CancelRequestResponse()
        {
        }

        /// <summary>
        /// Response
        /// </summary>
        public string Response
        {
            get
            {
                return this.response;
            }
            set
            {
                this.response = value;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetType().Name);
            builder.Append('(');
            builder.Append("Result=").Append(this.response.ToString());
            builder.Append(')');
            return builder.ToString();
        }
    }
}
