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
using System.Globalization;
using System.Net;
using System.Web;
using System.Xml;
using Uol.PagSeguro.NetStandard.Domain;
using Uol.PagSeguro.NetStandard.Exception;
using Uol.PagSeguro.NetStandard.Log;
using Uol.PagSeguro.NetStandard.Resources;
using Uol.PagSeguro.NetStandard.Util;
using Uol.PagSeguro.NetStandard.XmlParse;

namespace Uol.PagSeguro.NetStandard.Service
{

    /// <summary>
    /// Encapsulates web service calls regarding PagSeguro Cancels
    /// </summary>
    public static class CancelService
    {

        /// <summary>
        /// Request a transaction cancellation from transaction code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="transactionCode">Transaction Code</param>
        /// <returns><c cref="T:Uol.PagSeguro.NetStandard.CancelRequestResponse">Result</c></returns>
        public static RequestResponse RequestCancel(Credentials credentials, string transactionCode)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "CancelService.Register(transactionCode = {0}) - begin", transactionCode));
            try {
                using(HttpWebResponse response = HttpURLConnectionUtil.GetHttpPostConnection(
                    PagSeguroConfiguration.CancelUri.AbsoluteUri, BuildCancelURL(credentials, transactionCode)))
                {
            
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {

                        RequestResponse cancel = new RequestResponse();
                        CancelSerializer.Read(reader, cancel);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "CancelService.createRequest({0}) - end", cancel.ToString()));
                        return cancel;
                    }
                }
            } catch (WebException exception) {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "CancelService.createRequest() - error {0}", pse));
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="transactionCode">Transaction Code</param>
        /// <returns></returns>
        private static string BuildCancelURL(Credentials credentials, string transactionCode)
        {
            QueryStringBuilder builder = new QueryStringBuilder();

            builder.EncodeCredentialsAsQueryString(credentials);
            builder.Append("transactionCode", transactionCode);

            return builder.ToString();
        }

     }
}