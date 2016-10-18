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
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.XmlParse;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace Uol.PagSeguro.Util
{
    /// <summary>
    /// 
    /// </summary>
    internal static class HttpURLConnectionUtil
    {
        internal const string GetMethod = "GET";
        internal const string PostMethod = "POST";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static PagSeguroServiceException CreatePagSeguroServiceException(System.Exception exception)
        {
            return new PagSeguroServiceException(exception.Message, exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static HttpResponseMessage GetHttpPostConnection(string urlPath, string query)
        {
            return GetHttpURLConnection(HttpURLConnectionUtil.PostMethod, PagSeguroConfiguration.FormUrlEncoded, PagSeguroConfiguration.Encoding, urlPath, query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        internal static HttpResponseMessage GetHttpGetConnection(string urlPath)
        {
            return GetHttpURLConnection(HttpURLConnectionUtil.GetMethod, PagSeguroConfiguration.FormUrlEncoded, PagSeguroConfiguration.Encoding, urlPath, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        /// <param name="urlPath"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private static HttpResponseMessage GetHttpURLConnection(string method, string contentType, string encoding, string urlPath, string query)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMilliseconds(PagSeguroConfiguration.RequestTimeout);
                client.DefaultRequestHeaders.Add("lib-description", ".net:" + PagSeguroConfiguration.LibVersion);
                client.DefaultRequestHeaders.Add("language-engine-description", ".net:" + PagSeguroConfiguration.LanguageEngineDescription);

                // adding module version to header request 
                if (!string.IsNullOrEmpty(PagSeguroConfiguration.ModuleVersion))
                {
                    client.DefaultRequestHeaders.Add("module-description", PagSeguroConfiguration.ModuleVersion);
                }

                // adding cms version to header request 
                if (!string.IsNullOrEmpty(PagSeguroConfiguration.CmsVersion))
                {
                    client.DefaultRequestHeaders.Add("cms-description", PagSeguroConfiguration.CmsVersion);
                }

                if (HttpURLConnectionUtil.PostMethod.Equals(method))
                {
                    return client.PostAsync(urlPath, new StringContent(query, Encoding.GetEncoding(encoding), contentType)).Result;
                }
                else
                {
                    return client.GetAsync(urlPath).Result;
                }
            }
            catch (WebException exception)
            {
                throw exception;
            }
        }

    }
}
