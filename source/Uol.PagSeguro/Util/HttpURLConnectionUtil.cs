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
        internal static PagSeguroServiceException CreatePagSeguroServiceException(HttpWebResponse response)
        {
            if (response == null)
                throw new PagSeguroServiceException("response answered with null value");

            if (response.StatusCode == HttpStatusCode.OK)
                throw new ArgumentException("response.StatusCode must be different than HttpStatusCode.OK", "response");


            using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        List<ServiceError> errors = new List<ServiceError>();
                        try
                        {
                            ErrorsSerializer.Read(reader, errors);
                        }
                        catch (XmlException e)
                        {
                            return new PagSeguroServiceException(response.StatusCode, e);
                        }

                        return new PagSeguroServiceException(response.StatusCode, errors);

                    default:
                        return new PagSeguroServiceException(response.StatusCode);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static HttpWebResponse GetHttpPostConnection(string urlPath, string query)
        {
            string contentType = PagSeguroConfiguration.FormUrlEncoded + "; charset= " + PagSeguroConfiguration.Encoding;
            return GetHttpURLConnection(HttpURLConnectionUtil.PostMethod, contentType, urlPath, query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        internal static HttpWebResponse GetHttpGetConnection(string urlPath)
        {
            string contentType = PagSeguroConfiguration.FormUrlEncoded + "; charset= " + PagSeguroConfiguration.Encoding;
            return GetHttpURLConnection(HttpURLConnectionUtil.GetMethod, contentType, urlPath, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        /// <param name="urlPath"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private static HttpWebResponse GetHttpURLConnection(string method, string contentType, string urlPath, string query)
        {
            HttpWebRequest request;
            try
            {
                request = (HttpWebRequest) WebRequest.Create(urlPath);

                request.ContentType = contentType;
                request.Method = method;
                request.Timeout = PagSeguroConfiguration.RequestTimeout;
                request.Headers.Add("lib-description", ".net:" + PagSeguroConfiguration.LibVersion);
                request.Headers.Add("language-engine-description", ".net:" + PagSeguroConfiguration.LanguageEngineDescription);

                // adding module version to header request 
                if (!string.IsNullOrEmpty(PagSeguroConfiguration.ModuleVersion))
                {
                    request.Headers.Add("module-description", PagSeguroConfiguration.ModuleVersion);
                }

                // adding cms version to header request 
                if (!string.IsNullOrEmpty(PagSeguroConfiguration.CmsVersion))
                {
                    request.Headers.Add("cms-description", PagSeguroConfiguration.CmsVersion);
                }

                if (HttpURLConnectionUtil.PostMethod.Equals(method))
                {

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        byte[] byteArray = Encoding.UTF8.GetBytes(query);
                        requestStream.Write(byteArray, 0, byteArray.Length);
                        requestStream.Close();
                    }
                }
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException exception)
            {
                throw exception;
            }
        }

    }
}
