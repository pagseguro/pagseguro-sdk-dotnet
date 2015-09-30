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
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Util;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Identifies a PagSeguro application
    /// </summary>
    public class ApplicationCredentials : Credentials
    {
        private const string AppIdParameterName = "appId";
        private const string AppKeyParameterName = "appKey";
        private const string AuthorizatinCodeParameterName = "authorizationCode";

        /// <summary>
        /// Initializes a new instance of the ApplicationCredentials class
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <remarks>
        /// A PagSeguro application is identified by an app identifier and a app key
        /// </remarks>
        public ApplicationCredentials(string appId, string appKey, string authorizationCode = null)
        {
            try
            {
                this.AttributeDictionary[AppIdParameterName] = appId;
                this.AttributeDictionary[AppKeyParameterName] = appKey;
            }
            catch (PagSeguroServiceException)
            {
                throw new PagSeguroServiceException("Application Credentials not set correctly.");
            }

            this.AttributeDictionary[AuthorizatinCodeParameterName] = !string.IsNullOrEmpty(authorizationCode) ? authorizationCode.ToString() : string.Empty;
        }

        /// <summary>
        /// Primary appId associated with this account
        /// </summary>
        public string AppId
        {
            get
            {
                return this.AttributeDictionary[AppIdParameterName];
            }
        }

        /// <summary>
        /// PagSeguro app key
        /// </summary>
        public string AppKey
        {
            get
            {
                return this.AttributeDictionary[AppKeyParameterName];
            }
        }

        /// <summary>
        /// PagSeguro authorization code
        /// </summary>
        public string AuthorizationCode
        {
            get
            {
                return this.AttributeDictionary[AuthorizatinCodeParameterName];
            }
            set
            {
                this.AttributeDictionary[AuthorizatinCodeParameterName] = value;
            }
        }
    }
}
