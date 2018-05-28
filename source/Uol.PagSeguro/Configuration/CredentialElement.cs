using System.Configuration;
// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc />
    public class CredentialElement : ConfigurationElement
    {
        private const string EmailKey = "Email";
        private const string TokenKey = "Token";
        private const string SandboxEmailKey = "SandboxEmail";
        private const string SandboxTokenKey = "SandboxToken";
        private const string AppIdKey = "AppId";
        private const string AppKeyKey = "AppKey";
        private const string SandboxAppIdKey = "SandboxAppId";
        private const string SandboxAppKeyKey = "SandboxAppKey";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(EmailKey, IsRequired = true)]
        public TextElement Email
        {
            get => (TextElement)this[EmailKey];
            set => this[EmailKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(TokenKey, IsRequired = true)]
        public TextElement Token
        {
            get => (TextElement)this[TokenKey];
            set => this[TokenKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(SandboxEmailKey, IsRequired = true)]
        public TextElement SandboxEmail
        {
            get => (TextElement)this[SandboxEmailKey];
            set => this[SandboxEmailKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(SandboxTokenKey, IsRequired = true)]
        public TextElement SandboxToken
        {
            get => (TextElement)this[SandboxTokenKey];
            set => this[SandboxTokenKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(AppIdKey, IsRequired = true)]
        public TextElement AppId
        {
            get => (TextElement)this[AppIdKey];
            set => this[AppIdKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(AppKeyKey, IsRequired = true)]
        public TextElement AppKey
        {
            get => (TextElement)this[AppKeyKey];
            set => this[AppKeyKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(SandboxAppIdKey, IsRequired = true)]
        public TextElement SandboxAppId
        {
            get => (TextElement)this[SandboxAppIdKey];
            set => this[SandboxAppIdKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(SandboxAppKeyKey, IsRequired = true)]
        public TextElement SandboxAppKey
        {
            get => (TextElement)this[SandboxAppKeyKey];
            set => this[SandboxAppKeyKey] = value;
        }
    }
}
