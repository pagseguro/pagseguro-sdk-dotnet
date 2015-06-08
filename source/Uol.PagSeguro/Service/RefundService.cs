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
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Log;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Service
{

    /// <summary>
    /// Encapsulates web service calls regarding PagSeguro Cancels
    /// </summary>
    public static class RefundService
    {

        /// <summary>
        /// Request a transaction refund from transaction code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="transactionCode">Transaction Code</param>
        /// <returns><c cref="T:Uol.PagSeguro.CancelRequestResponse">Result</c></returns>
        public static RequestResponse RequestRefund(Credentials credentials, string transactionCode, decimal? refundValue = null)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "RefundService.Register(transactionCode = {0}) - begin", transactionCode));
            
            try {
                using(HttpWebResponse response = HttpURLConnectionUtil.GetHttpPostConnection(
                    PagSeguroConfiguration.RefundUri.AbsoluteUri, BuildRefundURL(credentials, transactionCode, refundValue)))
                {
            
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {

                        RequestResponse refund = new RequestResponse();
                        RefundSerializer.Read(reader, refund);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "RefundService.Register({0}) - end", refund.ToString()));
                        return refund;
                    }
                }
            } catch (WebException exception) {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "RefundService.Register() - error {0}", pse));
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="transactionCode">Transaction Code</param>
        /// <param name="refundValue">Refund Value</param>
        /// <returns></returns>
        private static string BuildRefundURL(Credentials credentials, string transactionCode, decimal? refundValue)
        {
            QueryStringBuilder builder = new QueryStringBuilder();

            builder.EncodeCredentialsAsQueryString(credentials);
            builder.Append("transactionCode", transactionCode);
            if (refundValue.HasValue) {
                builder.Append("refundValue", PagSeguroUtil.DecimalFormat(refundValue.Value));
            }

            return builder.ToString();
        }

     }
}