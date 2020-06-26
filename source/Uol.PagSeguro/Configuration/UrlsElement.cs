using System;
using System.Configuration;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.XmlParse;

// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc cref="ConfigurationElement" />
    public class UrlsElement : ConfigurationElement, ITypedConfigValueProvider
    {
        private const string PaymentKey = "Payment";
        private const string PaymentRedirectKey = "PaymentRedirect";
        private const string NotificationKey = "Notification";
        private const string SearchKey = "Search";
        private const string SearchAbandonedKey = "SearchAbandoned";
        private const string CancelKey = "Cancel";
        private const string RefundKey = "Refund";
        private const string PreApprovalKey = "PreApproval";
        private const string DirectPaymentKey = "DirectPayment";
        private const string AuthorizationKey = "Authorization";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PaymentKey, IsRequired = true)]
        public UrlElement Payment
        {
            get => (UrlElement)this[PaymentKey];
            set => this[PaymentKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PaymentRedirectKey, IsRequired = true)]
        public UrlElement PaymentRedirect
        {
            get => (UrlElement)this[PaymentRedirectKey];
            set => this[PaymentRedirectKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(NotificationKey, IsRequired = true)]
        public UrlElement Notification
        {
            get => (UrlElement)this[NotificationKey];
            set => this[NotificationKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(SearchKey, IsRequired = true)]
        public UrlElement Search
        {
            get => (UrlElement)this[SearchKey];
            set => this[SearchKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(SearchAbandonedKey, IsRequired = true)]
        public UrlElement SearchAbandoned
        {
            get => (UrlElement)this[SearchAbandonedKey];
            set => this[SearchAbandonedKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(CancelKey, IsRequired = true)]
        public UrlElement Cancel
        {
            get => (UrlElement)this[CancelKey];
            set => this[CancelKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(RefundKey, IsRequired = true)]
        public UrlElement Refund
        {
            get => (UrlElement)this[RefundKey];
            set => this[RefundKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PreApprovalKey, IsRequired = true)]
        public PreApprovalElement PreApproval
        {
            get => (PreApprovalElement)this[PreApprovalKey];
            set => this[PreApprovalKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(DirectPaymentKey, IsRequired = true)]
        public DirectPaymentElement DirectPayment
        {
            get => (DirectPaymentElement)this[DirectPaymentKey];
            set => this[DirectPaymentKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(AuthorizationKey, IsRequired = true)]
        public AuthorizationElement Authorization
        {
            get => (AuthorizationElement)this[AuthorizationKey];
            set => this[AuthorizationKey] = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="elementKey"></param>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        public T GetValue<T>(string elementKey = null, bool sandbox = false)
        {
            if (typeof(T) != typeof(string))
                return default(T);

            T configValue;
            switch (elementKey)
            {
                case PagSeguroConfigSerializer.Payment:
                case PagSeguroConfigSerializer.PaymentRedirect:
                case PagSeguroConfigSerializer.Notification:
                case PagSeguroConfigSerializer.Search:
                case PagSeguroConfigSerializer.SearchAbandoned:
                case PagSeguroConfigSerializer.Cancel:
                case PagSeguroConfigSerializer.Refund:
                    configValue = ((UrlElement)this[elementKey]).GetValue<T>();
                    break;
                case PagSeguroConfigSerializer.Session:
                case PagSeguroConfigSerializer.Transactions:
                case PagSeguroConfigSerializer.Installment:
                    configValue = DirectPayment.GetValue<T>(elementKey);
                    break;
                case PagSeguroConfigSerializer.PreApprovalRequest:
                case PagSeguroConfigSerializer.PreApprovalRedirect:
                case PagSeguroConfigSerializer.PreApprovalNotification:
                case PagSeguroConfigSerializer.PreApprovalCancel:
                case PagSeguroConfigSerializer.PreApprovalSearch:
                case PagSeguroConfigSerializer.PreApprovalPayment:
                    configValue = PreApproval.GetValue<T>(elementKey);
                    break;
                case PagSeguroConfigSerializer.Authorization:
                case PagSeguroConfigSerializer.AuthorizationSearch:
                case PagSeguroConfigSerializer.AuthorizationRequest:
                case PagSeguroConfigSerializer.AuthorizationNotification:
                    configValue = Authorization.GetValue<T>(elementKey);
                    break;
                default:
                    return default(T);
            }

            var urlValue = configValue as string;
            const string pagSeguroUrl = EnvironmentConfiguration.PagseguroUrl;
            const string sandboxUrl = EnvironmentConfiguration.SandboxUrl;
            if (sandbox && !string.IsNullOrWhiteSpace(urlValue) &&
                urlValue.IndexOf(EnvironmentConfiguration.SandboxUrl, StringComparison.InvariantCultureIgnoreCase) < 0)
                return (T) (object) urlValue.Replace(pagSeguroUrl, sandboxUrl);

            return (T) (object) urlValue;
        }
    }
}
