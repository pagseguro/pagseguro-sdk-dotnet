using System.Configuration;
using Uol.PagSeguro.Resources;

// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc cref="ConfigurationElement" />
    public class DirectPaymentElement : ConfigurationElement, IUrlCollectionElement
    {
        private const string SessionKey = "Session";
        private const string InstallmentKey = "Installment";
        private const string TransactionsKey = "Transactions";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(SessionKey, IsRequired = true)]
        public UrlElement Session
        {
            get => (UrlElement)this[SessionKey];
            set => this[SessionKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(InstallmentKey, IsRequired = true)]
        public UrlElement Installment
        {
            get => (UrlElement)this[InstallmentKey];
            set => this[InstallmentKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(TransactionsKey, IsRequired = true)]
        public UrlElement Transactions
        {
            get => (UrlElement)this[TransactionsKey];
            set => this[TransactionsKey] = value;
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
