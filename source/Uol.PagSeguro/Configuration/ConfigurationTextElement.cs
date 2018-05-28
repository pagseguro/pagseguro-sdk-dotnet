using System.Configuration;
using System.Xml;

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc />
    public class ConfigurationTextElement<T> : ConfigurationElement
    {
        /// <inheritdoc />
        protected override void DeserializeElement(XmlReader reader,
                                bool serializeCollectionKey)
        {
            Value = (T)reader.ReadElementContentAs(typeof(T), null);
        }

        /// <summary>
        /// 
        /// </summary>
        public T Value { get; set; }
    }
}
