using System.Configuration;
using Uol.PagSeguro.Resources;

// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc cref="ConfigurationElement" />
    public class AuthorizationElement : ConfigurationElement, IUrlCollectionElement
    {
        private const string AuthorizationRequestKey = "AuthorizationRequest";
        private const string AuthorizationUrlKey = "AuthorizationURL";
        private const string AuthorizationSearchKey = "AuthorizationSearch";
        private const string AuthorizationNotificationKey = "AuthorizationNotification";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(AuthorizationRequestKey, IsRequired = true)]
        public UrlElement AuthorizationRequest
        {
            get => (UrlElement)this[AuthorizationRequestKey];
            set => this[AuthorizationRequestKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(AuthorizationUrlKey, IsRequired = true)]
        public UrlElement AuthorizationUrl
        {
            get => (UrlElement)this[AuthorizationUrlKey];
            set => this[AuthorizationUrlKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(AuthorizationSearchKey, IsRequired = true)]
        public UrlElement AuthorizationSearch
        {
            get => (UrlElement)this[AuthorizationSearchKey];
            set => this[AuthorizationSearchKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(AuthorizationNotificationKey, IsRequired = true)]
        public UrlElement AuthorizationNotification
        {
            get => (UrlElement)this[AuthorizationNotificationKey];
            set => this[AuthorizationNotificationKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlKey"></param>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        public string Get(string urlKey, bool sandbox)
        {
            var urlValue = ((UrlElement) this[urlKey]).Link.Value;

            if (sandbox && !string.IsNullOrWhiteSpace(urlValue))
                urlValue = urlValue.Replace(EnvironmentConfiguration.PagseguroUrl, EnvironmentConfiguration.SandboxUrl);

            return urlValue;
        }
    }
}
