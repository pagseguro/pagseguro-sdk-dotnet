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
        // ReSharper disable once UnusedMember.Local
        private const string PagSeguroConfig = "PagSeguroConfiguration";

        // ReSharper disable once UnusedMember.Local
        private const string Urls = "Urls";
        internal const string Payment = "Payment";
        internal const string PaymentRedirect = "PaymentRedirect";
        internal const string Notification = "Notification";
        internal const string Search = "Search";
        internal const string SearchAbandoned = "SearchAbandoned";
        internal const string Cancel = "Cancel";
        internal const string Refund = "Refund";
        internal const string PreApproval = "PreApprovalRequest";
        internal const string PreApprovalRedirect = "PreApprovalRedirect";
        internal const string PreApprovalNotification = "PreApprovalNotification";
        internal const string PreApprovalSearch = "PreApprovalSearch";
        internal const string PreApprovalCancel = "PreApprovalCancel";
        internal const string PreApprovalPayment = "PreApprovalPayment";
        internal const string Session = "Session";
        internal const string Transactions = "Transactions";
        internal const string Installment = "Installment";
        internal const string AuthorizationRequest = "AuthorizationRequest";
        internal const string Authorization = "AuthorizationURL";
        internal const string AuthorizationSearch = "AuthorizationSearch";
        internal const string AuthorizationNotification = "AuthorizationNotification";

        // ReSharper disable once UnusedMember.Local
        private const string Credential = "Credential";
        internal const string Email = "Email";
        internal const string Token = "Token";
        internal const string SandboxEmail = "SandboxEmail";
        internal const string SandboxToken = "SandboxToken";
        internal const string AppId = "AppId";
        internal const string AppKey = "AppKey";
        internal const string SandboxAppId = "SandboxAppId";
        internal const string SandboxAppKey = "SandboxAppKey";

        // ReSharper disable once UnusedMember.Local
        private const string Configuration = "Configuration";
        internal const string LibVersion = "LibVersion";
        internal const string FormUrlEncoded = "FormUrlEncoded";
        internal const string Encoding = "Encoding";
        internal const string RequestTimeout = "RequestTimeout";

        internal static string GetWebserviceUrl(XmlDocument xml, string urlToSearch)
        {
            var url = GetDataConfiguration(xml, urlToSearch);
            if (string.IsNullOrEmpty(url))
                throw new ArgumentException(" WebService URL not set for " + urlToSearch + " environment.");

            return url;
        }

        internal static string GetDataConfiguration(XmlDocument xml, string data)
        {
            var element = xml.GetElementsByTagName(data);
            var result = element.Item(0)?.InnerText;
            if (string.IsNullOrEmpty(result))
                throw new ArgumentException(" Resources key " + data + " not found.");

            return result;
        }

        internal static AccountCredentials GetAccountCredentials(XmlDocument xml, bool sandbox)
        {
            AccountCredentials credential;

            string email;
            string token;

            if (sandbox)
            {
                email = GetDataConfiguration(xml, SandboxEmail);
                token = GetDataConfiguration(xml, SandboxToken);
            }
            else
            {
                email = GetDataConfiguration(xml, Email);
                token = GetDataConfiguration(xml, Token);
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

        internal static ApplicationCredentials GetApplicationCredentials(XmlDocument xml, bool sandbox)
        {
            string appId;
            string appKey;          

            if (sandbox)
            {
                appId = GetDataConfiguration(xml, SandboxAppId);
                appKey = GetDataConfiguration(xml, SandboxAppKey);
            }
            else
            {
                appId = GetDataConfiguration(xml, AppId);
                appKey = GetDataConfiguration(xml, AppKey);
            }

            try
            {
                var credential = new ApplicationCredentials(appId, appKey);
                return credential;
            }
            catch (System.Exception)
            {
                throw new ArgumentException("To use credentials from config.properties file you must "
                + "configure the properties credential appId and credential appKey.");
            }
        }
    }
}