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

using System.Collections.Generic;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.Domain.Authorization;
using System;

namespace Uol.PagSeguro.Parse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AuthorizationParse
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationRequest">PagSeguro AuthorizationRequest</param>
        /// <returns></returns>
        public static IDictionary<string, string> GetData(AuthorizationRequest authorizationRequest)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            // Reference
            if (authorizationRequest.Reference != null)
            {
                data["reference"] = authorizationRequest.Reference;
            }

            // RedirectURL
            if (authorizationRequest.RedirectURL != null)
            {
                data["redirectURL"] = authorizationRequest.RedirectURL;
            }

            // NotificationURL
            if (authorizationRequest.NotificationURL != null)
            {
                data["notificationURL"] = authorizationRequest.NotificationURL;
            }

            // Permissions
            if (authorizationRequest.Permissions != null)
            {
                data["permissions"] = string.Join(",", authorizationRequest.Permissions.ToArray());
            }

            return data;
        }
    }
}
