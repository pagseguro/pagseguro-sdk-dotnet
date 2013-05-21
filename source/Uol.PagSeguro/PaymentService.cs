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
using System.Text;
using System.Net;
using System.Xml;
using Uol.PagSeguro.Serialization;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace Uol.PagSeguro
{
    /// <summary>
    /// Encapsulates web service calls regarding PagSeguro payment requests
    /// </summary>
    public static class PaymentService
    {
        /// <summary>
        /// Requests a payment
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="payment">Payment request information</param>
        /// <returns>The Uri to where the user needs to be redirected to in order to complete the payment process</returns>
        public static Uri Register(Credentials credentials, PaymentRequest payment)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");
            if (payment == null)
                throw new ArgumentNullException("payment");

            PaymentRequestResponse response = RegisterCore(credentials, payment);
            return response.PaymentRedirectUri;
        }

        // RegisterCore is the actual implementation of the Register method 
        // This separation serves as test hook to validate the Uri 
        // against the code returned by the service
        internal static PaymentRequestResponse RegisterCore(Credentials credentials, PaymentRequest payment)
        {
            UriBuilder uriBuilder = new UriBuilder(PagSeguroConfiguration.PaymentUri);
            uriBuilder.Query = ServiceHelper.EncodeCredentialsAsQueryString(credentials);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
            request.Method = ServiceHelper.PostMethod;
            request.ContentType = ServiceHelper.XmlEncoded;
            request.Timeout = PagSeguroConfiguration.RequestTimeout;

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PaymentService.Register({0}) - begin", payment));

            using (Stream requestStream = request.GetRequestStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;
                using (XmlWriter writer = XmlWriter.Create(requestStream, settings))
                {
                    PaymentRequestSerializer.Write(writer, payment);
                    writer.Close();
                    requestStream.Close();

                    try
                    {
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                                {
                                    PaymentRequestResponse paymentResponse = new PaymentRequestResponse(PagSeguroConfiguration.PaymentRedirectUri);
                                    PaymentRequestResponseSerializer.Read(reader, paymentResponse);
                                    PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PaymentService.Register({0}) - end {1}", payment, paymentResponse.PaymentRedirectUri));
                                    return paymentResponse;
                                }
                            }
                            else
                            {
                                PagSeguroServiceException pse = ServiceHelper.CreatePagSeguroServiceException(response);
                                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PaymentService.Register({0}) - error {1}", payment, pse));
                                throw pse;
                            }
                        }
                    }
                    catch (WebException exception)
                    {
                        PagSeguroServiceException pse = ServiceHelper.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                        PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PaymentService.Register({0}) - error {1}", payment, pse));
                        throw pse;
                    }
                }
            }
        }
    }
}
