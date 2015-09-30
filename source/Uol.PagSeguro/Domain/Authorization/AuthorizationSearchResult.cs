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
using System.Collections.ObjectModel;
using System.Text;

namespace Uol.PagSeguro.Domain.Authorization
{
    /// <summary>
    /// Represents a page of authorization transactions returned by the authorization search service
    /// </summary>
    public class AuthorizationSearchResult
    {
        private List<AuthorizationSummary> authorizations = new List<AuthorizationSummary>();

        /// <summary>
        /// date
        /// </summary>
        public DateTime Date
        {
            get;
            internal set;
        }

        /// <summary>
        /// Authorizations in this page
        /// </summary>
        public IList<AuthorizationSummary> Authorizations
        {
            get
            {
                return this.authorizations;
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
            builder.Append("Date=").Append(this.Date).Append(", ");
            builder.Append("Authorizations=").Append("(");
            int counter = 1;
            foreach (AuthorizationSummary authorization in this.Authorizations)
            {
                if (this.Authorizations.Count == counter) {
                    builder.Append(authorization.Code);
                } else {
                    builder.Append(authorization.Code).Append(", ");
                    counter++;
                }
            }
            builder.Append(')');
            return builder.ToString();
        }
    }
}
