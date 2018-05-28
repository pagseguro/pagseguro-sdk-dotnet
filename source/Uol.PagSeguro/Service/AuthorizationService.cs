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

using System.Globalization;
using System.Net;
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
    internal class AuthorizationService
    {

        /// <summary>
        /// Creates a new authorization request
        /// </summary>
        /// <param name="credentials">PagSeguro credentials. Required</param>
        /// <param name="authorizationRequest">PagSeguro AuthorizationRequest</param>
        /// <param name="onlyAuthorizationCode"></param>
        /// <returns></returns>
        public static string CreateAuthorizationRequest(Credentials credentials, AuthorizationRequest authorizationRequest, bool onlyAuthorizationCode)
        {
            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "AuthorizationService.CreateAuthorizationRequest() - begin"));

            try
            {
                using (var response = HttpUrlConnectionUtil.GetHttpPostConnection(
                    PagSeguroUris.GetAuthorizarionRequestUri(credentials).AbsoluteUri, BuildAuthorizationRequestUrl(credentials, authorizationRequest))) 
                {
                    using (var reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        var authorization = new AuthorizationResponse();
                        AuthorizationSerializer.Read(reader, authorization);

                        return onlyAuthorizationCode ? authorization.Code : BuildAuthorizationUrl(credentials, authorization.Code);
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

        internal static string BuildAuthorizationUrl(Credentials credentials, string code)
        {
            var builder = new QueryStringBuilder("{URL}?code={code}");

            builder.ReplaceValue("{URL}", PagSeguroUris.GetAuthorizarionUri(credentials).AbsoluteUri);
            builder.ReplaceValue("{code}", code);

            return builder.ToString();
        }

        internal static string BuildAuthorizationRequestUrl(Credentials credentials, AuthorizationRequest authorizationRequest)
        {
            var builder = new QueryStringBuilder();
            var data = AuthorizationParse.GetData(authorizationRequest);

            builder.EncodeCredentialsAsQueryString(credentials);

            foreach (var pair in data)
                builder.Append(pair.Key, pair.Value.ToString(CultureInfo.InvariantCulture));
 
            return HttpUtility.UrlDecode(builder.ToString());
        }
    }
}
