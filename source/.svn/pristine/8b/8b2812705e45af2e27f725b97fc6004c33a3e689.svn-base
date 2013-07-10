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
    /// Identifies a PagSeguro account
    /// </summary>
    public class AccountCredentials : Credentials
    {
        private const string EmailParameterName = "email";
        private const string TokenParameterName = "token";

        /// <summary>
        /// Initializes a new instance of the AccountCredentials class
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <remarks>
        /// A PagSeguro account is identified by an email and a security token
        /// </remarks>
        public  AccountCredentials(string email, string token)
        {
            this.AttributeDictionary[EmailParameterName] = email;
            this.AttributeDictionary[TokenParameterName] = token;
        }

        /// <summary>
        /// Primary email associated with this account
        /// </summary>
        public string Email
        {
            get
            {
                return this.AttributeDictionary[EmailParameterName];
            }
        }

        /// <summary>
        /// PagSeguro token
        /// </summary>
        public string Token
        {
            get
            {
                return this.AttributeDictionary[TokenParameterName];
            }
        }
    }
}
