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
    public class AuthorizationSearchService
    {

        private const string InitialDateParameterName = "initialDate";
        private const string FinalDateParameterName = "finalDate";
        private const string PageNumberParameterName = "page";
        private const string MaxPageResultsParameterName = "maxPageResults";

        /// <summary>
        /// Finds a authorization with a matching authorization code
        /// </summary>
        /// <param name="credentials">PagSeguro credentials. Required.</param>
        /// <param name="code">Authorization code. Required</param>
        /// <returns>Authorization Summary</returns>
        public static AuthorizationSummary SearchByCode(Credentials credentials, String code)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "AuthorizationSearchService.SearchByCode({0}) - begin", code));

            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByCode(credentials, code))) 
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        AuthorizationSummary authorization = new AuthorizationSummary();
                        AuthorizationSummarySerializer.Read(reader, authorization);
                        return authorization;                           
                    }
                }
            }
            catch (WebException exception)
            {
                throw exception;
            }
            catch (PagSeguroServiceException pse)
            {
                throw pse;
            }
        }

        /// <summary>
        /// Finds a authorization with a matching date interval
        /// </summary>
        /// <param name="credentials">PagSeguro credentials. Required.</param>
        /// <param name="initialDate"></param>
        /// <param name="finalDate">End of date range. Use DateTime.MaxValue to search without an upper boundary.</param>
        /// <param name="pageNumber">Page number, starting with 1. If passed as 0, it will call the web service to get the default page, also page number 1.</param>
        /// <param name="resultsPerPage">Results per page, optional.</param>
        /// <returns></returns>
        public static AuthorizationSearchResult SearchByDate(Credentials credentials, DateTime initialDate, DateTime finalDate, int? pageNumber = null, int? resultsPerPage = null)
        {
            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "AuthorizationSearchService.SearchByDate(initialDate={0} - finalDate={1}) - begin", initialDate, finalDate));

            try
            {
                using(HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(BuildSearchUrlByDate(credentials, initialDate, finalDate, pageNumber, resultsPerPage)))
                {
                    using(XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        AuthorizationSearchResult authorization = new AuthorizationSearchResult();
                        AuthorizationSearchResultSerializer.Read(reader, authorization);
                        return authorization;
                    }
                }
            }
            catch (WebException exception)
            {
                throw exception;
            }
            catch (PagSeguroServiceException pse)
            {
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        internal static String BuildSearchUrlByCode(Credentials credentials, String code)
        {

            QueryStringBuilder builder = new QueryStringBuilder("{URL}{code}?{credentials}");
            builder.ReplaceValue("{URL}", PagSeguroConfiguration.AuthorizarionSearchUri.AbsoluteUri);
            builder.ReplaceValue("{code}", code);
            builder.ReplaceValue("{credentials}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString() );

            return builder.ToString();
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
        internal static String BuildSearchUrlByDate(Credentials credentials, DateTime initialDate, DateTime finalDate, int? pageNumber = null, int? resultsPerPage = null)
        {

            QueryStringBuilder builder = new QueryStringBuilder("{URL}?{credentials}&initialDate={initialDate}{finalDate}{page}{maxPageResults}");
            builder.ReplaceValue("{URL}", PagSeguroConfiguration.AuthorizarionSearchUri.AbsoluteUri);
            builder.ReplaceValue("{initialDate}", PagSeguroUtil.FormatDateXml(initialDate));
            builder.ReplaceValue("{finalDate}", finalDate < DateTime.MaxValue ? "&" + FinalDateParameterName + "=" + PagSeguroUtil.FormatDateXml(finalDate) : "");
            
            if (pageNumber.HasValue) {
                builder.ReplaceValue("{page}", pageNumber > 0 ? "&" + PageNumberParameterName + "=" + pageNumber : "" );
            }
            if (pageNumber.HasValue)
            {
                builder.ReplaceValue("{maxPageResults}", resultsPerPage > 0 ? "&" + MaxPageResultsParameterName + "=" + resultsPerPage : "");
            }

            builder.ReplaceValue("{credentials}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());

            return PagSeguroUtil.RemoveExtraSpaces(builder.ToString());
        }

    }
}
