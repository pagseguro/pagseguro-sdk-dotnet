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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Authorization;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Log;
using Uol.PagSeguro.Parse;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Service
{
    class AuthorizationService
    {

        /// <summary>
        /// Creates a new authorization request
        /// </summary>
        /// <param name="credentials">PagSeguro credentials. Required</param>
        /// <param name="authorizationRequest">PagSeguro AuthorizationRequest</param>
        /// <param name="onlyAuthorizationCode"></param>
        /// <returns></returns>
        public static String CreateAuthorizationRequest(Credentials credentials, AuthorizationRequest authorizationRequest, Boolean onlyAuthorizationCode)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "AuthorizationService.CreateAuthorizationRequest() - begin"));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpPostConnection(
                    PagSeguroConfiguration.AuthorizarionRequestUri.AbsoluteUri, buildAuthorizationRequestUrl(credentials, authorizationRequest))) 
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        AuthorizationResponse authorization = new AuthorizationResponse();
                        AuthorizationSerializer.Read(reader, authorization);

                        if (onlyAuthorizationCode) {
                            return authorization.Code;
                        } else {
                            return BuildAuthorizationURL(authorization.Code);
                        }
                    }
                }
            }
            catch (WebException pse)
            {
                throw pse;
            }
            catch (PagSeguroServiceException pse)
            {
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static String BuildAuthorizationURL(String code)
        {

            QueryStringBuilder builder = new QueryStringBuilder("{URL}?code={code}");
            builder.ReplaceValue("{URL}", PagSeguroConfiguration.AuthorizarionUri.AbsoluteUri);
            builder.ReplaceValue("{code}", code);

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static string buildAuthorizationRequestUrl(Credentials credentials, AuthorizationRequest authorizationRequest)
        {
            QueryStringBuilder builder = new QueryStringBuilder();
            IDictionary<string, string> data = AuthorizationParse.GetData(authorizationRequest);

            builder.
                EncodeCredentialsAsQueryString(credentials);

            foreach (KeyValuePair<string, string> pair in data)
            {
                builder.Append(pair.Key, pair.Value.ToString(CultureInfo.InvariantCulture));
            }
 
            return HttpUtility.UrlDecode(builder.ToString());
        }
    }
}
