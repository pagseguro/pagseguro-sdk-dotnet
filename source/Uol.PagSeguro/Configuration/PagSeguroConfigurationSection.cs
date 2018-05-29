using System;
using System.Configuration;
using Uol.PagSeguro.Resources;

// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc />
    public class PagSeguroConfigurationSection : ConfigurationSection
    {
        private const string UrlsKey = "Urls";        
        private const string CredentialKey = "Credential";
        private const string ConfigurationKey = "Configuration";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(UrlsKey, IsRequired = true)]
        public UrlsElement Urls
        {
            get => (UrlsElement)this[UrlsKey];
            set => this[UrlsKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(CredentialKey, IsRequired = true)]
        public CredentialElement Credential
        {
            get => (CredentialElement)this[CredentialKey];
            set => this[CredentialKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(ConfigurationKey, IsRequired = true)]
        public PagSeguroConfigurationElement Configuration
        {
            get => (PagSeguroConfigurationElement)this[ConfigurationKey];
            set => this[ConfigurationKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSandbox"></param>
        /// <returns></returns>
        public string GetCredentialEmail(bool isSandbox) =>
            isSandbox ? Credential.SandboxEmail.Value : Credential.Email.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSandbox"></param>
        /// <returns></returns>
        public string GetCredentialToken(bool isSandbox) =>
            isSandbox ? Credential.SandboxToken.Value : Credential.Token.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSandbox"></param>
        /// <returns></returns>
        public string GetCredentialAppId(bool isSandbox) =>
            isSandbox ? Credential.SandboxAppId.Value : Credential.AppId.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSandbox"></param>
        /// <returns></returns>
        public string GetCredentialAppKey(bool isSandbox) =>
            isSandbox ? Credential.SandboxAppKey.Value : Credential.AppKey.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSandbox"></param>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static PagSeguroConfigurationSection GetCurrent(bool isSandbox = false, string email = default(string),
            string token = default(string), string appId = default(string), string appKey = default(string))
        {
            if (!(ConfigurationManager.GetSection("PagSeguro") is PagSeguroConfigurationSection configuration))
                return null;

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(token) ||
                !string.IsNullOrWhiteSpace(appId) && !string.IsNullOrWhiteSpace(appKey))
                configuration = SetAppConfigCredentials(configuration, isSandbox, email, token, appId, appKey);

            if (!isSandbox)
                return configuration;

            CheckSandboxEnvironment(configuration.Urls.Cancel.Link);
            CheckSandboxEnvironment(configuration.Urls.Payment.Link);
            CheckSandboxEnvironment(configuration.Urls.PaymentRedirect.Link);
            CheckSandboxEnvironment(configuration.Urls.Notification.Link);
            CheckSandboxEnvironment(configuration.Urls.Search.Link);
            CheckSandboxEnvironment(configuration.Urls.SearchAbandoned.Link);
            CheckSandboxEnvironment(configuration.Urls.Refund.Link);

            CheckSandboxEnvironment(configuration.Urls.DirectPayment.Session.Link);
            CheckSandboxEnvironment(configuration.Urls.DirectPayment.Installment.Link);
            CheckSandboxEnvironment(configuration.Urls.DirectPayment.Transactions.Link);

            CheckSandboxEnvironment(configuration.Urls.Authorization.AuthorizationUrl.Link);
            CheckSandboxEnvironment(configuration.Urls.Authorization.AuthorizationRequest.Link);
            CheckSandboxEnvironment(configuration.Urls.Authorization.AuthorizationSearch.Link);
            CheckSandboxEnvironment(configuration.Urls.Authorization.AuthorizationNotification.Link);

            CheckSandboxEnvironment(configuration.Urls.PreApproval.PreApprovalRequest.Link);
            CheckSandboxEnvironment(configuration.Urls.PreApproval.PreApprovalRedirect.Link);
            CheckSandboxEnvironment(configuration.Urls.PreApproval.PreApprovalNotification.Link);
            CheckSandboxEnvironment(configuration.Urls.PreApproval.PreApprovalPayment.Link);
            CheckSandboxEnvironment(configuration.Urls.PreApproval.PreApprovalSearch.Link);
            CheckSandboxEnvironment(configuration.Urls.PreApproval.PreApprovalCancel.Link);

            return configuration;
        }

        private static void CheckSandboxEnvironment(TextElement configElement)
        {
            var urlValue = configElement?.Value;
            const string pagSeguroUrl = EnvironmentConfiguration.PagseguroUrl;
            const string sandboxUrl = EnvironmentConfiguration.SandboxUrl;

            if (string.IsNullOrWhiteSpace(urlValue) ||
                urlValue.IndexOf(sandboxUrl, StringComparison.InvariantCultureIgnoreCase) >= 0)
                return;

            configElement.Value = urlValue.Replace(pagSeguroUrl, sandboxUrl);
        }

        private static PagSeguroConfigurationSection SetAppConfigCredentials(PagSeguroConfigurationSection appConfig,
            bool isSandbox, string email = default(string), string token = default(string), string appId = default(string), string appKey = default(string))
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
