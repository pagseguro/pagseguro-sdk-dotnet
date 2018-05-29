using System.Configuration;
using System.Xml;

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc />
    public class TypedConfigurationElement<T> : ConfigurationElement
    {
        /// <summary>
        /// </summary>
        public T Value { get; internal set; }

        /// <summary>
        /// </summary>
        public bool IsValid { get; private set; }

        /// <inheritdoc />
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            try
            {
                Value = (T) reader.ReadElementContentAs(typeof(T), null);
                IsValid = true;
            }
            catch
            {
                Value = default(T);
                IsValid = false;
            }
        }
    }
}
