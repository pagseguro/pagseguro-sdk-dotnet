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
    /// Encapsulates web service calls regarding PagSeguro pre-approval requests
    /// </summary>
    public static class PreApprovalService
    {

        /// <summary>
        /// CreatePreApproval is the actual implementation of the Register method
        /// This separation serves as test hook to validate the Uri
        /// against the code returned by the service
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="preApproval">PreApproval request information</param>
        /// <returns>The Uri to where the user needs to be redirected to in order to complete the payment process</returns>
        public static Uri CreatePreApproval(Credentials credentials, PreApprovalRequest preApproval)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.Register({0}) - begin", preApproval));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpPostConnection(
                    PagSeguroConfiguration.PreApprovalUri.AbsoluteUri, BuildPreApprovalUrl(credentials, preApproval)))
                {

                    if (HttpStatusCode.OK.Equals(response.StatusCode))
                    {
                        using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            PreApprovalRequestResponse preApprovalResponse = new PreApprovalRequestResponse(PagSeguroConfiguration.PreApprovalRedirectUri);
                            PreApprovalSerializer.Read(reader, preApprovalResponse);
                            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.Register({0}) - end {1}", preApproval, preApprovalResponse.PreApprovalRedirectUri));
                            return preApprovalResponse.PreApprovalRedirectUri;
                        }
                    }
                    else
                    {
                        PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException(response);
                        PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.Register({0}) - error {1}", preApproval, pse));
                        throw pse;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.Register({0}) - error {1}", preApproval, pse));
                throw pse;
            }
        }

        /// <summary>
        /// CancelPreApproval
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="preApprovalCode">PreApproval code</param>
        /// <returns>The PreApprovalRequestResponse wich contains the response</returns>
        public static bool CancelPreApproval(Credentials credentials, string preApprovalCode)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.CancelPreApproval({0}) - begin", preApprovalCode));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildCancelUrl(credentials, preApprovalCode)))
                {

                    if (HttpStatusCode.OK.Equals(response.StatusCode))
                    {
                        using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            PreApprovalRequestResponse paymentResponse = new PreApprovalRequestResponse(PagSeguroConfiguration.PreApprovalCancelUri);
                            PreApprovalSerializer.Read(reader, paymentResponse);
                            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.CancelPreApproval({0}) - end {1}", preApprovalCode, paymentResponse.Status));
                            return paymentResponse.Status.Equals("OK", StringComparison.CurrentCultureIgnoreCase);
                        }
                    }
                    else
                    {
                        PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException(response);
                        PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.CancelPreApproval({0}) - error {1}", preApprovalCode, pse));
                        throw pse;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.CancelPreApproval({0}) - error {1}", preApprovalCode, pse));
                throw pse;
            }
        }

        /// <summary>
        /// ChargePreApproval
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="payment">PreApproval payment request information</param>
        /// <returns>The Uri to where the user needs to be redirected to in order to complete the payment process</returns>
        public static string ChargePreApproval(Credentials credentials, PaymentRequest payment)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.ChargePreApproval({0}) - begin", payment));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpPostConnection(
                    PagSeguroConfiguration.PreApprovalPaymentUri.AbsoluteUri, BuildChargeUrl(credentials, payment)))
                {
                    
                    if (HttpStatusCode.OK.Equals(response.StatusCode))
                    {
                        using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            
                            PaymentRequestResponse chargeResponse = new PaymentRequestResponse(PagSeguroConfiguration.PreApprovalPaymentUri);
                            PaymentSerializer.Read(reader, chargeResponse);
                            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.ChargePreApproval({0}) - end {1}", payment, chargeResponse.PaymentRedirectUri));
                            return chargeResponse.TransactionCode;
                        }
                    }
                    else
                    {
                        PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException(response);
                        PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.ChargePreApproval({0}) - error {1}", payment, pse));
                        throw pse;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalService.ChargePreApproval({0}) - error {1}", payment, pse));
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="preApproval"></param>
        /// <returns></returns>
        internal static string BuildPreApprovalUrl(Credentials credentials, PreApprovalRequest preApproval)
        {
            QueryStringBuilder builder = new QueryStringBuilder();
            IDictionary<string, string> data = PreApprovalParse.GetData(preApproval);

            builder.
                EncodeCredentialsAsQueryString(credentials);
            
            foreach (KeyValuePair<string, string> pair in data)
            {
                builder.Append(pair.Key, pair.Value);
            }

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="preApprovalCode"></param>
        /// <returns></returns>
        private static string BuildCancelUrl(Credentials credentials, string preApprovalCode)
        {
            QueryStringBuilder searchUrlByCode = new QueryStringBuilder("{url}/{preApprovalCode}?{credential}");
            searchUrlByCode.ReplaceValue("{url}", PagSeguroConfiguration.PreApprovalCancelUri.AbsoluteUri);
            searchUrlByCode.ReplaceValue("{preApprovalCode}", HttpUtility.UrlEncode(preApprovalCode));
            searchUrlByCode.ReplaceValue("{credential}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            return searchUrlByCode.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        internal static string BuildChargeUrl(Credentials credentials, PaymentRequest payment)
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
