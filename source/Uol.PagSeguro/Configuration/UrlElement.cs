using System.Configuration;
// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc />
    public class UrlElement : ConfigurationElement
    {
        private const string LinkKey = "Link";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(LinkKey, IsRequired = true)]
        public TextElement Link
        {
            get => (TextElement)this[LinkKey];
            set => this[LinkKey] = value;
        }
    }
}
