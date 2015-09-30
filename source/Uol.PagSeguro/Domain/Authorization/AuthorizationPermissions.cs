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
using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain.Authorization
{
    /// <summary>
    /// Authorization permissions
    /// </summary>
    public class AuthorizationPermissions
    {

        /// <summary>
        /// Code
        /// </summary>
        public String Code
        {
            get;
            internal set;
        }

        /// <summary>
        /// Status
        /// </summary>
        public String Status
        {
            get;
            internal set;
        }

        /// <summary>
        /// Last update
        /// </summary>
        public DateTime LastUpdate
        {
            get;
            internal set;
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
            builder.Append("Code=").Append(this.Code).Append(", ");
            builder.Append("Status=").Append(this.Status).Append(", ");
            builder.Append("LastUpdate=").Append(this.LastUpdate);
            builder.Append(')');
            return builder.ToString();
        }

    }
}
