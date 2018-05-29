using System.Configuration;
// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc cref="ConfigurationElement" />
    public class UrlElement : ConfigurationElement, ITypedConfigValueProvider
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

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="elementKey"></param>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        public virtual T GetValue<T>(string elementKey = null, bool sandbox = false)
        {
            if (typeof(T) != typeof(string))
                return default(T);

            return Link.Value is T ? (T) (object) Link.Value : default(T);
        }
    }
}
