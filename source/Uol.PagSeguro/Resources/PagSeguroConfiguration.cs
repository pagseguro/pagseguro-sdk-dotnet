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
using System.Xml;
using Uol.PagSeguro.Configuration;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public static class PagSeguroConfiguration
    {
        //PagSeguro .NET Library Tests

        /// <summary>
        /// 
        /// </summary>
        public static AccountCredentials GetAccountCredentials(bool isSandbox, string email = null, string token = null)
        {
            var appConfig = GetAppConfig(isSandbox, email, token);
            if (appConfig == null)
                return PagSeguroConfigSerializer.GetAccountCredentials(LoadXmlConfig(), isSandbox);

            return new AccountCredentials(
                appConfig.GetCredentialEmail(isSandbox),
                appConfig.GetCredentialToken(isSandbox));
        }

        /// <summary>
        /// 
        /// </summary>
        public static ApplicationCredentials GetApplicationCredentials(bool isSandbox, string appId = null, string appKey = null)
        {
            var appConfig = GetAppConfig(isSandbox, null, null, appId, appKey);
            if (appConfig == null)
                return PagSeguroConfigSerializer.GetApplicationCredentials(LoadXmlConfig(), isSandbox);

            return new ApplicationCredentials(
                appConfig.GetCredentialAppId(isSandbox),
                appConfig.GetCredentialAppKey(isSandbox));
        }

        /// <summary>
        /// 
        /// </summary>
        public static string UrlXmlConfiguration { get; set; } = $"{GetBasePath()}/PagSeguroConfig.xml";

        /// <summary>
        /// 
        /// </summary>
        public static string ModuleVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string CmsVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string LanguageEngineDescription => Environment.Version.ToString();

        /// <summary>
        /// 
        /// </summary>
        public static Uri NotificationUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Notification));

        /// <summary>
        /// 
        /// </summary>
        public static Uri PaymentUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Payment));

        /// <summary>
        /// 
        /// </summary>
        public static Uri PaymentRedirectUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.PaymentRedirect));

        /// <summary>
        /// 
        /// </summary>
        public static Uri SearchUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Search));

        /// <summary>
        /// 
        /// </summary>
        public static Uri SearchAbandonedUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.SearchAbandoned));

        /// <summary>
        /// 
        /// </summary>
        public static Uri CancelUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Cancel));

        /// <summary>
        /// 
        /// </summary>
        public static Uri RefundUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Refund));

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApproval));

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalRedirectUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalRedirect));

        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static Uri PreApprovalNotificationUri =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalNotification));

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalSearchUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalSearch));

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalCancelUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalCancel));

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalPaymentUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalPayment));

        /// <summary>
        /// 
        /// </summary>
        public static Uri SessionUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Session));

        /// <summary>
        /// 
        /// </summary>
        public static Uri TransactionsUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Transactions));

        /// <summary>
        /// 
        /// </summary>
        public static Uri InstallmentUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Installment));

        /// <summary>
        /// 
        /// </summary>
        public static Uri AuthorizarionRequestUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationRequest));

        /// <summary>
        /// 
        /// </summary>
        public static Uri AuthorizarionUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.Authorization));

        /// <summary>
        /// 
        /// </summary>
        public static Uri AuthorizarionSearchUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationSearch));

        /// <summary>
        /// 
        /// </summary>
        public static Uri AuthorizationNotificationUri => new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationNotification));

        /// <summary>
        /// 
        /// </summary>
        public static int RequestTimeout => Convert.ToInt32(GetDataConfiguration(PagSeguroConfigSerializer.RequestTimeout));

        /// <summary>
        /// 
        /// </summary>
        public static string FormUrlEncoded => GetDataConfiguration(PagSeguroConfigSerializer.FormUrlEncoded);

        /// <summary>
        /// 
        /// </summary>
        public static string Encoding => GetDataConfiguration(PagSeguroConfigSerializer.Encoding);

        /// <summary>
        /// 
        /// </summary>
        public static string LibVersion => GetDataConfiguration(PagSeguroConfigSerializer.LibVersion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetUrlValue(string url)
        {
            return PagSeguroConfigSerializer.GetWebserviceUrl(LoadXmlConfig(), url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string GetDataConfiguration(string data)
        {
            return PagSeguroConfigSerializer.GetDataConfiguration(LoadXmlConfig(), data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static XmlDocument LoadXmlConfig()
        {
            var xml = new XmlDocument();
            xml.Load(UrlXmlConfiguration);
            return xml;
        }

        private static string GetBasePath()
        {
            return System.Web.HttpContext.Current != null
                ? System.Web.HttpRuntime.AppDomainAppPath
                : AppDomain.CurrentDomain.BaseDirectory;
        }

        private static PagSeguroConfigurationSection GetAppConfig(bool isSandbox, string email = null, string token = null, string appId = null, string appKey = null)
        {
            if (!(System.Configuration.ConfigurationManager.GetSection("PagSeguro") is PagSeguroConfigurationSection configuration))
                return null;

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(token) ||
                !string.IsNullOrWhiteSpace(appId) && !string.IsNullOrWhiteSpace(appKey))
                configuration = SetAppConfigCredentials(configuration, isSandbox, email, token, appId, appKey);

            if (!isSandbox)
                return configuration;

            const string pagseguroUrl = EnvironmentConfiguration.PagseguroUrl;
            const string sandboxUrl = EnvironmentConfiguration.SandboxUrl;

            configuration.Urls.Authorization.AuthorizationRequest.Link.Value = configuration.Urls.Authorization.AuthorizationRequest.Link.Value.Replace(pagseguroUrl, sandboxUrl);
            configuration.Urls.Authorization.AuthorizationUrl.Link.Value = configuration.Urls.Authorization.AuthorizationUrl.Link.Value.Replace(pagseguroUrl, sandboxUrl);
            configuration.Urls.Authorization.AuthorizationSearch.Link.Value = configuration.Urls.Authorization.AuthorizationSearch.Link.Value.Replace(pagseguroUrl, sandboxUrl);
            configuration.Urls.Authorization.AuthorizationSearch.Link.Value = configuration.Urls.Authorization.AuthorizationSearch.Link.Value.Replace(pagseguroUrl, sandboxUrl);
            configuration.Urls.Authorization.AuthorizationNotification.Link.Value = configuration.Urls.Authorization.AuthorizationNotification.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.Cancel.Link.Value = configuration.Urls.Cancel.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.DirectPayment.Session.Link.Value = configuration.Urls.DirectPayment.Session.Link.Value.Replace(pagseguroUrl, sandboxUrl);
            configuration.Urls.DirectPayment.Installment.Link.Value = configuration.Urls.DirectPayment.Installment.Link.Value.Replace(pagseguroUrl, sandboxUrl);
            configuration.Urls.DirectPayment.Transactions.Link.Value = configuration.Urls.DirectPayment.Transactions.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.Payment.Link.Value = configuration.Urls.Payment.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.PaymentRedirect.Link.Value = configuration.Urls.PaymentRedirect.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.Notification.Link.Value = configuration.Urls.Notification.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.Search.Link.Value = configuration.Urls.Search.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.SearchAbandoned.Link.Value = configuration.Urls.SearchAbandoned.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.Refund.Link.Value = configuration.Urls.Refund.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            configuration.Urls.PreApproval.Link.Value = configuration.Urls.PreApproval.Link.Value.Replace(pagseguroUrl, sandboxUrl);

            return configuration;
        }

        private static PagSeguroConfigurationSection SetAppConfigCredentials(PagSeguroConfigurationSection appConfig,
            bool isSandbox, string email, string token, string appId = null, string appKey = null)
        {
            if (isSandbox)
            {
                appConfig.Credential.SandboxEmail.Value = email ?? appConfig.Credential.SandboxEmail.Value;
                appConfig.Credential.SandboxToken.Value = token ?? appConfig.Credential.SandboxToken.Value;
                appConfig.Credential.SandboxAppId.Value = appId ?? appConfig.Credential.SandboxAppId.Value;
                appConfig.Credential.SandboxAppKey.Value = appKey ?? appConfig.Credential.SandboxAppKey.Value;
                return appConfig;
            }

            appConfig.Credential.Email.Value = email ?? appConfig.Credential.Email.Value;
            appConfig.Credential.Token.Value = token ?? appConfig.Credential.Token.Value;
            appConfig.Credential.AppId.Value = appId ?? appConfig.Credential.AppId.Value;
            appConfig.Credential.AppKey.Value = appKey ?? appConfig.Credential.AppKey.Value;
            return appConfig;
        }
    }
}
