using System.Configuration;
// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc />
    public class AuthorizationElement : ConfigurationElement
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
    }
}
