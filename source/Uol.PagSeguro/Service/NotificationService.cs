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
using Uol.PagSeguro.Domain.Authorization;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Log;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Service
{
    /// <summary>
    /// Encapsulates web service calls regarding PagSeguro notifications
    /// </summary>
    public static class NotificationService
    {

        /// <summary>
        /// Returns a transaction from a notification code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="notificationCode">Transaction notification code</param>
        /// <returns><c cref="T:Uol.PagSeguro.Transaction">Transaction</c></returns>
        public static Transaction CheckTransaction(Credentials credentials, string notificationCode)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "NotificationService.CheckTransaction(notificationCode={0}) - begin", notificationCode));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildTransactionNotificationUrl(credentials,notificationCode)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        Transaction transaction = new Transaction();
                        TransactionSerializer.Read(reader, transaction);

                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "NotificationService.CheckTransaction(notificationCode={0}) - end {1}", notificationCode, transaction));
                        return transaction;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(
                String.Format(CultureInfo.InvariantCulture, "NotificationService.CheckTransaction(notificationCode={0}) - error {1}", notificationCode, pse));
                throw pse;
            }
        }

        /// <summary>
        /// Returns a authorization from a notification code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="notificationCode">Authorization notification code</param>
        /// <returns><c cref="T:Uol.PagSeguro.Transaction">Transaction</c></returns>
        public static AuthorizationSummary CheckAuthorization(Credentials credentials, string notificationCode)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "NotificationService.CheckAuthorization(notificationCode={0}) - begin", notificationCode));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildAuthorizationNotificationUrl(credentials, notificationCode)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        AuthorizationSummary authorization = new AuthorizationSummary();
                        AuthorizationSummarySerializer.Read(reader, authorization);

                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "NotificationService.CheckAuthorization(notificationCode={0}) - end {1}", notificationCode, authorization));
                        return authorization;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(
                String.Format(CultureInfo.InvariantCulture, "NotificationService.CheckAuthorization(notificationCode={0}) - error {1}", notificationCode, pse));
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="notificationCode"></param>
        /// <returns></returns>
        private static string BuildTransactionNotificationUrl(Credentials credentials, string notificationCode) 
        {
            QueryStringBuilder transactionNotificationUrl = new QueryStringBuilder("{url}/{notificationCode}?{credential}");
            transactionNotificationUrl.ReplaceValue("{url}", PagSeguroConfiguration.NotificationUri.AbsoluteUri);
            transactionNotificationUrl.ReplaceValue("{notificationCode}", HttpUtility.UrlEncode(notificationCode));
            transactionNotificationUrl.ReplaceValue("{credential}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            return transactionNotificationUrl.ToString();
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="notificationCode"></param>
        /// <returns></returns>
        private static string BuildAuthorizationNotificationUrl(Credentials credentials, string notificationCode)
        {
            QueryStringBuilder builder = new QueryStringBuilder("{url}{notificationCode}?{credential}");
            builder.ReplaceValue("{url}", PagSeguroConfiguration.AuthorizationNotificationUri.AbsoluteUri);
            builder.ReplaceValue("{notificationCode}", HttpUtility.UrlEncode(notificationCode));
            builder.ReplaceValue("{credential}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            return builder.ToString();
        }
    }
}
