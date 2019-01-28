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
    /// Represents the response from the web service after a payment request was registered
    /// </summary>
    internal class PreApprovalRequestResponse
    {
        private readonly Uri preApprovalRedirectBaseUri;
        private string code = string.Empty;
        private string status = string.Empty;

        /// <summary>
        /// Initializes a new instance of the PaymentRequestResponse class
        /// </summary>
        /// <param name="preApprovalRedirectBaseUri"></param>
        internal PreApprovalRequestResponse(Uri preApprovalRedirectBaseUri)
        {
            this.preApprovalRedirectBaseUri = preApprovalRedirectBaseUri;
        }

        /// <summary>
        /// PreApproval request code
        /// </summary>
        public string Code
        {
            get => code;
            set => code = value;
        }

        /// <summary>
        /// Registration date
        /// </summary>
        public DateTime RegistrationDate
        {
            get;
            set;
        }

        /// <summary>
        /// PreApproval cancel status
        /// </summary>
        public string Status
        {
            get => status;
            set => status = value;
        }

        /// <summary>
        /// Uri for the pre-approval page in the PagSeguro web site for this preApproval request
        /// </summary>
        public Uri PreApprovalRedirectUri
        {
            get
            {
                QueryStringBuilder builder = new QueryStringBuilder();
                builder.Append("code", Code);
                UriBuilder uriBuilder = new UriBuilder(preApprovalRedirectBaseUri)
                {
                    Query = builder.ToString()
                };
                return uriBuilder.Uri;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append('(');
            builder.Append("Code=").Append(Code).Append(", ");
            builder.Append("RegistrationDate=").Append(RegistrationDate).Append(", ");
            builder.Append("PreApprovalRedirectUri=").Append(PreApprovalRedirectUri.ToString());
            builder.Append(')');
            return builder.ToString();
        }
    }
}