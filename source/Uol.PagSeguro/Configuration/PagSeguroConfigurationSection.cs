using System.Configuration;
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
    }
}
