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
using System.Globalization;
using System.Net;
using System.Xml;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Log;
using Uol.PagSeguro.Parse;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.XmlParse;
using System.Web;

namespace Uol.PagSeguro.Service
{
    /// <summary>
    /// Encapsulates web service calls regarding PagSeguro payment requests
    /// </summary>
    public static class PaymentService
    {

        /// <summary>
        /// createCheckoutRequest is the actual implementation of the Register method
        /// This separation serves as test hook to validate the Uri
        /// against the code returned by the service
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="payment">Payment request information</param>
        /// <returns>The Uri to where the user needs to be redirected to in order to complete the payment process</returns>
        public static Uri CreateCheckoutRequest(Credentials credentials, PaymentRequest payment)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PaymentService.Register({0}) - begin", payment));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpPostConnection(
                    PagSeguroConfiguration.PaymentUri.AbsoluteUri, BuildCheckoutUrl(credentials, payment)))
                {

                    if (HttpStatusCode.OK.Equals(response.StatusCode))
                    {
                        using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            PaymentRequestResponse paymentResponse = new PaymentRequestResponse(PagSeguroConfiguration.PaymentRedirectUri);
                            PaymentSerializer.Read(reader, paymentResponse);
                            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PaymentService.Register({0}) - end {1}", payment, paymentResponse.PaymentRedirectUri));
                            return paymentResponse.PaymentRedirectUri;
                        }
                    }
                    else
                    {
                        PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException(response);
                        PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PaymentService.Register({0}) - error {1}", payment, pse));
                        throw pse;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PaymentService.Register({0}) - error {1}", payment, pse));
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        internal static string BuildCheckoutUrl(Credentials credentials, PaymentRequest payment)
        {
            QueryStringBuilder builder = new QueryStringBuilder();
            IDictionary<string, string> data = PaymentParse.GetData(payment);

            builder.
                EncodeCredentialsAsQueryString(credentials);
            
            foreach (KeyValuePair<string, string> pair in data)
            {
                builder.Append(pair.Key, pair.Value);
            }

            return builder.ToString();
        }
    }
}
