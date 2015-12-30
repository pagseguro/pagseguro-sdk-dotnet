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
    /// Encapsulates web service calls regarding PagSeguro to search pre-approvals
    /// </summary>
    public static class PreApprovalSearchService
    {
        private const string InitialDateParameterName = "initialDate";
        private const string FinalDateParameterName = "finalDate";
        private const string PageNumberParameterName = "page";
        private const string MaxPageResultsParameterName = "maxPageResults";

        /// <summary>
        /// Finds a pre-approval with a matching pre-approval code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="preApprovalCode">Pre-Approval code</param>
        /// <returns cref="T:Uol.PagSeguro.Transaction"><c>Transaction</c></returns>
        public static PreApprovalTransaction SearchByCode(Credentials credentials, string preApprovalCode)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByCode(preApprovalCode={0}) - begin", preApprovalCode));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByCode(credentials, preApprovalCode)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        PreApprovalTransaction preApproval = new PreApprovalTransaction();
                        PreApprovalTransactionSerializer.Read(reader, preApproval);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByCode(preApprovalCode={0}) - end {1}", preApprovalCode, preApproval));
                        return preApproval;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByCode(preApprovalCode={0}) - error {1}", preApprovalCode, pse));
                throw pse;
            }
        }

        /// <summary>
        /// Finds a pre-approval with a matching notification code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="notificationCode">Notification code</param>
        /// <returns cref="T:Uol.PagSeguro.Transaction"><c>Transaction</c></returns>
        public static PreApprovalTransaction SearchByNofication(Credentials credentials, string notificationCode)
        {
            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByNotification(notificationCode={0}) - begin", notificationCode));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByNotification(credentials, notificationCode)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        PreApprovalTransaction preApproval = new PreApprovalTransaction();
                        PreApprovalTransactionSerializer.Read(reader, preApproval);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByNotification(notificationCode={0}) - end {1}", notificationCode, preApproval));
                        return preApproval;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByNotification(notificationCode={0}) - error {1}", notificationCode, pse));
                throw pse;
            }
        }

        /// <summary>
        /// Finds a pre-approval with a matching day interval
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="interval">Day interval</param>
        /// <returns cref="T:Uol.PagSeguro.PreApprovalSearchResult"><c>PreApprovalSearchResult</c></returns>
        public static PreApprovalSearchResult SearchByInterval(Credentials credentials, int interval)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByInterval(Days={0}) - begin", interval));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByInterval(credentials, interval)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        PreApprovalSearchResult preApproval = new PreApprovalSearchResult();
                        PreApprovalSearchResultSerializer.Read(reader, preApproval);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByInterval(Days={0}) - end {1}", interval, preApproval));
                        return preApproval;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByInterval(Days={0}) - error {1}", interval, pse));
                throw pse;
            }
        }

        /// <summary>
        /// Finds a pre-approval with a matching date interval
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate">End of date range. Use DateTime.MaxValue to search without an upper boundary.</param>
        /// <param name="pageNumber">Page number, starting with 1. If passed as 0, it will call the web service to get the default page, also page number 1.</param>
        /// <param name="resultsPerPage">Results per page, optional.</param>
        /// <returns></returns>
        public static PreApprovalSearchResult SearchByDate(Credentials credentials, DateTime initialDate, DateTime finalDate, int? pageNumber = null, int? resultsPerPage = null)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByDate(initialDate={0} - finalDate={1}) - begin", initialDate, finalDate));
            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByDate(credentials, initialDate, finalDate, pageNumber, resultsPerPage)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        PreApprovalSearchResult preApproval = new PreApprovalSearchResult();
                        PreApprovalSearchResultSerializer.Read(reader, preApproval);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByDate(initialDate={0} - finalDate={1}) - end {2}", initialDate, finalDate, preApproval));
                        return preApproval;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByDate(initialDate={0} - finalDate={1}) - error {2}", initialDate, finalDate, pse));
                throw pse;
            }
        }

        /// <summary>
        /// Finds a pre-approval with a matching pre-approval reference
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="reference">PagSeguro Pre-Approval Reference</param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate">End of date range. Use DateTime.MaxValue to search without an upper boundary.</param>
        /// <param name="pageNumber">Page number, starting with 1. If passed as 0, it will call the web service to get the default page, also page number 1.</param>
        /// <param name="resultsPerPage">Results per page, optional.</param>
        /// <returns></returns>
        public static PreApprovalSearchResult SearchByReference(Credentials credentials, String reference, DateTime initialDate, DateTime finalDate, int? pageNumber = null, int? resultsPerPage = null)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByReference(reference={0}) - begin", reference));
            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByReference(credentials, reference, initialDate, finalDate, pageNumber, resultsPerPage)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        PreApprovalSearchResult preApproval = new PreApprovalSearchResult();
                        PreApprovalSearchResultSerializer.Read(reader, preApproval);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByReference(reference={0}) - end {1}", reference, preApproval));
                        return preApproval;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "PreApprovalSearchService.SearchByReference(reference={0}) - error {1}", reference, pse));
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="preApprovalCode"></param>
        /// <returns></returns>
        private static string BuildSearchUrlByCode(Credentials credentials, string preApprovalCode)
        {
            QueryStringBuilder searchUrlByCode;

            searchUrlByCode = new QueryStringBuilder("{url}/{preApprovalCode}?{credential}");
            searchUrlByCode.ReplaceValue("{url}", PagSeguroConfiguration.PreApprovalSearchUri.AbsoluteUri);
            searchUrlByCode.ReplaceValue("{preApprovalCode}", HttpUtility.UrlEncode(preApprovalCode));

            searchUrlByCode.ReplaceValue("{credential}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            return searchUrlByCode.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        private static string BuildSearchUrlByInterval(Credentials credentials, int interval)
        {
            QueryStringBuilder searchUrlByInterval;

            searchUrlByInterval = new QueryStringBuilder("{url}/notifications?{credential}&interval={interval}");
            searchUrlByInterval.ReplaceValue("{url}", PagSeguroConfiguration.PreApprovalSearchUri.AbsoluteUri);
            searchUrlByInterval.ReplaceValue("{interval}", HttpUtility.UrlEncode(interval.ToString()));
            searchUrlByInterval.ReplaceValue("{credential}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            return searchUrlByInterval.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        private static string BuildSearchUrlByDate(Credentials credentials, DateTime initialDate, DateTime finalDate, int? pageNumber, int? resultsPerPage)
        {
            QueryStringBuilder builder = new QueryStringBuilder("{url}?initialDate={initialDate}{finalDate}{page}{maxPageResults}{credential}");

            builder.ReplaceValue("{url}", PagSeguroConfiguration.PreApprovalSearchUri.AbsoluteUri);
            builder.ReplaceValue("{initialDate}", PagSeguroUtil.FormatDateXml(initialDate));
            builder.ReplaceValue("{finalDate}", finalDate < DateTime.MaxValue ? "&" + FinalDateParameterName + "=" + PagSeguroUtil.FormatDateXml(finalDate) : "");
            if (pageNumber.HasValue)
            {
                builder.ReplaceValue("{page}", pageNumber > 0 ? "&" + PageNumberParameterName + "=" + pageNumber : "");
            }
            if (resultsPerPage.HasValue)
            {
                builder.ReplaceValue("{maxPageResults}", resultsPerPage > 0 ? "&" + MaxPageResultsParameterName + "=" + resultsPerPage : "");
            }
            builder.ReplaceValue("{credential}", credentials != null ? new QueryStringBuilder().AppendToQuery("&").EncodeCredentialsAsQueryString(credentials).ToString() : "");

            return PagSeguroUtil.RemoveExtraSpaces(builder.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="reference"></param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        private static string BuildSearchUrlByReference(Credentials credentials, String reference, DateTime initialDate, DateTime finalDate, int? pageNumber, int? resultsPerPage)
        {
            QueryStringBuilder builder = new QueryStringBuilder("{url}?reference={reference}&initialDate={initialDate}{finalDate}{page}{maxPageResults}{credential}");

            builder.ReplaceValue("{url}", PagSeguroConfiguration.PreApprovalSearchUri.AbsoluteUri);
            builder.ReplaceValue("{reference}", HttpUtility.UrlEncode(reference));
            builder.ReplaceValue("{initialDate}", PagSeguroUtil.FormatDateXml(initialDate));
            builder.ReplaceValue("{finalDate}", finalDate < DateTime.MaxValue ? "&" + FinalDateParameterName + "=" + PagSeguroUtil.FormatDateXml(finalDate) : "");
            if (pageNumber.HasValue)
            {
                builder.ReplaceValue("{page}", pageNumber > 0 ? "&" + PageNumberParameterName + "=" + pageNumber : "");
            }
            if (resultsPerPage.HasValue)
            {
                builder.ReplaceValue("{maxPageResults}", resultsPerPage > 0 ? "&" + MaxPageResultsParameterName + "=" + resultsPerPage : "");
            }
            builder.ReplaceValue("{credential}", credentials != null ? new QueryStringBuilder().AppendToQuery("&").EncodeCredentialsAsQueryString(credentials).ToString() : "");

            return PagSeguroUtil.RemoveExtraSpaces(builder.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="notificationCode"></param>
        /// <returns></returns>
        private static string BuildSearchUrlByNotification(Credentials credentials, string notificationCode)
        {
            QueryStringBuilder searchUrlByNotification;

            searchUrlByNotification = new QueryStringBuilder("{url}/notifications/{notificationCode}?{credential}");
            searchUrlByNotification.ReplaceValue("{url}", PagSeguroConfiguration.PreApprovalSearchUri.AbsoluteUri);
            searchUrlByNotification.ReplaceValue("{notificationCode}", HttpUtility.UrlEncode(notificationCode));

            searchUrlByNotification.ReplaceValue("{credential}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            return searchUrlByNotification.ToString();
        }
    }
}
