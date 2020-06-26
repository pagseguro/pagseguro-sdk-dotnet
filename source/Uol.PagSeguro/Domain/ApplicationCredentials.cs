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

using Uol.PagSeguro.Exception;

namespace Uol.PagSeguro.Domain
{
    /// <inheritdoc />
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
        /// <param name="isSandbox"></param>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <param name="authorizationCode"></param>
        /// <remarks>
        /// A PagSeguro application is identified by an app identifier and a app key
        /// </remarks>
        public ApplicationCredentials(bool isSandbox, string appId, string appKey, string authorizationCode = null)
        {
            try
            {
                IsSandbox = isSandbox;
                AttributeDictionary[AppIdParameterName] = appId;
                AttributeDictionary[AppKeyParameterName] = appKey;
            }
            catch (PagSeguroServiceException)
            {
                throw new PagSeguroServiceException("Application Credentials not set correctly.");
            }

            AttributeDictionary[AuthorizatinCodeParameterName] = !string.IsNullOrEmpty(authorizationCode) ? authorizationCode : string.Empty;
        }

        /// <summary>
        /// Primary appId associated with this account
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public string AppId
        {
            get => AttributeDictionary[AppIdParameterName];
            set => AttributeDictionary[AppIdParameterName] = value;
        }

        /// <summary>
        /// PagSeguro app key
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public string AppKey
        {
            get => AttributeDictionary[AppKeyParameterName];
            set => AttributeDictionary[AppKeyParameterName] = value;
        }

        /// <summary>
        /// PagSeguro authorization code
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public string AuthorizationCode
        {
            get => AttributeDictionary[AuthorizatinCodeParameterName];
            set => AttributeDictionary[AuthorizatinCodeParameterName] = value;
        }
    }
}
