using System.Configuration;
// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc cref="ConfigurationElement" />
    public class DirectPaymentElement : ConfigurationElement, ITypedConfigValueProvider
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

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="elementKey"></param>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        public T GetValue<T>(string elementKey = null, bool sandbox = false)
        {
            if (typeof(T) != typeof(string) || string.IsNullOrWhiteSpace(elementKey))
                return default(T);

            return ((UrlElement) this[elementKey]).GetValue<T>();
        }
    }
}
