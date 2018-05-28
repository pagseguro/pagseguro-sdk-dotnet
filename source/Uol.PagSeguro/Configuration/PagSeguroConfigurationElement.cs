using System.Configuration;
// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc />
    public class PagSeguroConfigurationElement : ConfigurationElement
    {
        private const string LibVersionKey = "LibVersion";
        private const string FormUrlEncodedKey = "FormUrlEncoded";
        private const string EncodingKey = "Encoding";
        private const string RequestTimeoutKey = "RequestTimeout";
        private const string SandboxKey = "Sandbox";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(LibVersionKey, IsRequired = true)]
        public TextElement LibVersion
        {
            get => (TextElement)this[LibVersionKey];
            set => this[LibVersionKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(FormUrlEncodedKey, IsRequired = true)]
        public TextElement FormUrlEncoded
        {
            get => (TextElement)this[FormUrlEncodedKey];
            set => this[FormUrlEncodedKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(EncodingKey, IsRequired = true)]
        public TextElement Encoding
        {
            get => (TextElement)this[EncodingKey];
            set => this[EncodingKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(RequestTimeoutKey, IsRequired = true)]
        public TextElement RequestTimeout
        {
            get => (TextElement)this[RequestTimeoutKey];
            set => this[RequestTimeoutKey] = value;
        }

        /// <summary>
        /// Configure Sandbox TextElement
        /// </summary>
        [ConfigurationProperty(SandboxKey, IsRequired = true)]
        public TextElement Sandbox
        {
            get => (TextElement)this[SandboxKey];
            set => this[SandboxKey] = value;
        }
    }
}
