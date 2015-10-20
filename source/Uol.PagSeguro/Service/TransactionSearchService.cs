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
    /// Encapsulates web service calls to search for PagSeguro transactions
    /// </summary>
    public static class TransactionSearchService
    {
        private const string InitialDateParameterName = "initialDate";
        private const string FinalDateParameterName = "finalDate";
        private const string PageNumberParameterName = "page";
        private const string MaxPageResultsParameterName = "maxPageResults";

        /// <summary>
        /// Finds a transaction with a matching transaction code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="transactionCode">Transaction code</param>
        /// <returns cref="T:Uol.PagSeguro.Transaction"><c>Transaction</c></returns>
        public static Transaction SearchByCode(Credentials credentials, string transactionCode)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByCode(transactionCode={0}) - begin", transactionCode));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByCode(credentials, transactionCode)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        Transaction transaction = new Transaction();
                        TransactionSerializer.Read(reader, transaction);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByCode(transactionCode={0}) - end {1}", transactionCode, transaction));
                        return transaction;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByCode(transactionCode={0}) - error {1}", transactionCode, pse));
                throw pse;
            }
        }

        /// <summary>
        /// Finds a transaction with a matching reference code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="reference">Reference</param>
        /// <returns></returns>
        public static TransactionSearchResult SearchByReference(Credentials credentials, string reference)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByReference(reference={0}) - begin", reference));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByReference(credentials, reference)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        TransactionSearchResult result = new TransactionSearchResult();
                        TransactionSearchResultSerializer.Read(reader, result);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByReference(reference={0}) - end", reference));
                        return result;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByReference(reference={0}) - error {1}", reference, pse));
                throw pse;
            }
        }

        /// <summary>
        /// Finds a transaction with a matching date interval
        /// </summary>
        /// <param name="credentials">PagSeguro credentials. Required.</param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate">End of date range. Use DateTime.MaxValue to search without an upper boundary.</param>
        /// <param name="pageNumber">Page number, starting with 1. If passed as 0, it will call the web service to get the default page, also page number 1.</param>
        /// <param name="resultsPerPage">Results per page, optional.</param>
        /// <returns></returns>
        public static TransactionSearchResult SearchByDate(Credentials credentials, DateTime initialDate, DateTime finalDate, int? pageNumber = null, int? resultsPerPage = null)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByDate(initialDate={0}, finalDate={1}) - begin", initialDate, finalDate));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByDate(credentials, initialDate, finalDate, pageNumber, resultsPerPage)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        TransactionSearchResult result = new TransactionSearchResult();
                        TransactionSearchResultSerializer.Read(reader, result);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByDate(initialDate={0}, finalDate={1}) - end {2}", initialDate, finalDate, result));
                        return result;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchByDate(initialDate={0}, finalDate={1}) - error {2}", initialDate, finalDate, pse));
                throw pse;
            }
        }

        /// <summary>
        /// Finds abandoned transactions
        /// </summary>
        /// <param name="credentials">PagSeguro credentials. Required.</param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate">End of date range. Use DateTime.MaxValue to search without an upper boundary.</param>
        /// <param name="pageNumber">Page number, starting with 1. If passed as 0, it will call the web service to get the default page, also page number 1.</param>
        /// <param name="resultsPerPage">Results per page, optional.</param>
        /// <returns></returns>
        public static TransactionSearchResult SearchAbandoned(Credentials credentials, DateTime initialDate, DateTime finalDate, int? pageNumber = null, int? resultsPerPage = null)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchAbandoned(initialDate={0}, finalDate={1}) - begin", initialDate, finalDate));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlAbandoned(credentials, initialDate, finalDate, pageNumber, resultsPerPage)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        TransactionSearchResult result = new TransactionSearchResult();
                        TransactionSearchResultSerializer.Read(reader, result);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchAbandoned(initialDate={0}, finalDate={1}) - end {2}", initialDate, finalDate, result));
                        return result;
                    }
                }
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "TransactionSearchService.SearchAbandoned(initialDate={0}, finalDate={1}) - error {2}", initialDate, finalDate, pse));
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        private static string BuildSearchUrlByCode(Credentials credentials, string transactionCode)
        {
            QueryStringBuilder searchUrlByCode;

            searchUrlByCode = new QueryStringBuilder("{url}/{transactionCode}?{credential}");
            searchUrlByCode.ReplaceValue("{url}", PagSeguroConfiguration.SearchUri.AbsoluteUri);
            searchUrlByCode.ReplaceValue("{transactionCode}", HttpUtility.UrlEncode(transactionCode));
            searchUrlByCode.ReplaceValue("{credential}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            return searchUrlByCode.ToString();
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
            QueryStringBuilder searchUrlByCode = new QueryStringBuilder("{url}/?{credential}&initialDate={initialDate}{finalDate}");

            searchUrlByCode.ReplaceValue("{url}", PagSeguroConfiguration.SearchUri.AbsoluteUri);
            searchUrlByCode.ReplaceValue("{initialDate}", PagSeguroUtil.FormatDateXml(initialDate));
            searchUrlByCode.ReplaceValue("{finalDate}", finalDate < DateTime.MaxValue ? "&" + FinalDateParameterName + "=" + PagSeguroUtil.FormatDateXml(finalDate) : "");

            if (pageNumber.HasValue)
                searchUrlByCode.AppendToQuery("{page}").ReplaceValue("{page}", pageNumber > 0 ? "&" + PageNumberParameterName + "=" + pageNumber : "");

            if (resultsPerPage.HasValue)
                searchUrlByCode.AppendToQuery("{maxPageResults}").ReplaceValue("{maxPageResults}", resultsPerPage > 0 ? "&" + MaxPageResultsParameterName + "=" + resultsPerPage : "");

            searchUrlByCode.ReplaceValue("{credential}", credentials != null ? new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString() : "");

            return PagSeguroUtil.RemoveExtraSpaces(searchUrlByCode.ToString());
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
        private static string BuildSearchUrlAbandoned(Credentials credentials, DateTime initialDate, DateTime finalDate, int? pageNumber, int? resultsPerPage)
        {
            QueryStringBuilder searchUrlAbandoned = new QueryStringBuilder("{url}/abandoned?initialDate={initialDate}{finalDate}{credential}");

            searchUrlAbandoned.ReplaceValue("{url}", PagSeguroConfiguration.SearchAbandonedUri.AbsoluteUri);
            searchUrlAbandoned.ReplaceValue("{initialDate}", PagSeguroUtil.FormatDateXml(initialDate));
            searchUrlAbandoned.ReplaceValue("{finalDate}", finalDate < DateTime.MaxValue ? "&" + FinalDateParameterName + "=" + PagSeguroUtil.FormatDateXml(finalDate) : "");
            
            if (pageNumber.HasValue)
                searchUrlAbandoned.AppendToQuery("{page}").ReplaceValue("{page}", pageNumber > 0 ? "&" + PageNumberParameterName + "=" + pageNumber : "");
            
            if (resultsPerPage.HasValue)
                searchUrlAbandoned.AppendToQuery("{maxPageResults}").ReplaceValue("{maxPageResults}", resultsPerPage > 0 ? "&" + MaxPageResultsParameterName + "=" + resultsPerPage : "");
            
            searchUrlAbandoned.ReplaceValue("{credential}", credentials != null ? new QueryStringBuilder().AppendToQuery("&").EncodeCredentialsAsQueryString(credentials).ToString() : "");

            return PagSeguroUtil.RemoveExtraSpaces(searchUrlAbandoned.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        private static string BuildSearchUrlByReference(Credentials credentials, string reference)
        {
            QueryStringBuilder searchUrlByReference;

            searchUrlByReference = new QueryStringBuilder("{url}?{credentials}&reference={reference}");
            searchUrlByReference.ReplaceValue("{url}", PagSeguroConfiguration.SearchUri.AbsoluteUri);
            searchUrlByReference.ReplaceValue("{reference}", HttpUtility.UrlEncode(reference));
            searchUrlByReference.ReplaceValue("{credentials}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());

            return searchUrlByReference.ToString();
        }

     }
}