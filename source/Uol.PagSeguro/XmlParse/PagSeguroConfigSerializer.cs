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
//   limitation

using System;
using System.Xml;
using Uol.PagSeguro.Domain;

namespace Uol.PagSeguro.XmlParse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PagSeguroConfigSerializer
    {
        private const string PagSeguroConfig = "PagSeguroConfiguration";

        private const string Urls = "Urls";
        internal const string Payment = "Payment";
        internal const string PaymentRedirect = "PaymentRedirect";
        internal const string Notification = "Notification";
        internal const string Search = "Search";
        internal const string PreApproval = "PreApproval";
        internal const string PreApprovalRedirect = "PreApprovalRedirect";
        internal const string PreApprovalNotification = "PreApprovalNotification";
        internal const string PreApprovalSearch = "PreApprovalSearch";
        internal const string PreApprovalCancel = "PreApprovalCancel";
        internal const string PreApprovalPayment = "PreApprovalPayment";

        private const string Credential = "Credential";
        internal const string Email = "Email";
        internal const string Token = "Token";
        internal const string SandboxEmail = "SandboxEmail";
        internal const string SandboxToken = "SandboxToken";

        private const string Configuration = "Configuration";
        internal const string LibVersion = "LibVersion";
        internal const string FormUrlEncoded = "FormUrlEncoded";
        internal const string Encoding = "Encoding";
        internal const string RequestTimeout = "RequestTimeout";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="urlToSearch"></param>
        /// <returns></returns>
        internal static string GetWebserviceUrl(XmlDocument xml, string urlToSearch)
        {
            string url = GetDataConfiguration(xml,urlToSearch);
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(" WebService URL not set for " + urlToSearch + " environment.");
            }
            return url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static string GetDataConfiguration(XmlDocument xml, string data)
        {
            XmlNodeList element = xml.GetElementsByTagName(data);
            string result = element.Item(0).InnerText;
            if (string.IsNullOrEmpty(result))
            {
                throw new ArgumentException(" Resources key " + data + " not found.");
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static AccountCredentials GetAccountCredentials(XmlDocument xml, bool sandbox)
        {

            AccountCredentials credential = null;

            string email;
            string token;

            if (sandbox)
            {
                email = GetDataConfiguration(xml, PagSeguroConfigSerializer.SandboxEmail);
                token = GetDataConfiguration(xml, PagSeguroConfigSerializer.SandboxToken);
            }
            else {
                email = GetDataConfiguration(xml, PagSeguroConfigSerializer.Email);
                token = GetDataConfiguration(xml, PagSeguroConfigSerializer.Token);
            }

            try
            {
                credential = new AccountCredentials(email, token);
            }
            catch (System.Exception)
            {
                throw new ArgumentException("To use credentials from config.properties file you must "
                + "configure the properties credential email and credential token.");
            }
            return credential;
        }
    }
}