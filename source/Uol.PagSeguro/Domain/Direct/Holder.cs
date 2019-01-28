using System.Text;

namespace Uol.PagSeguro.Domain.Direct
{
    /// <summary>
    /// Represents the holder of the credit card payment
    /// </summary>
    public class Holder
    {
        /// <summary>
        /// Holder name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Holder phone
        /// </summary>
        public Phone Phone
        {
            get;
            set;
        }

        /// <summary>
        /// Holder document
        /// </summary>
        public HolderDocument Document
        {
            get;
            set;
        }

        /// <summary>
        /// Holder birthdate
        /// </summary>
        public string Birthdate
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the Holder class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="document"></param>
        /// <param name="birthDate"></param>
        public Holder(string name = null, Phone phone = null, HolderDocument document = null, string birthDate = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Name = name;
            }
            if (phone != null)
            {
                Phone = phone;
            }
            if (document != null)
            {
                Document = document;
            }
            if (!string.IsNullOrEmpty(birthDate))
            {
                Birthdate = birthDate;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append('(');
            builder.Append("Name=").Append(Name).Append(", ");
            builder.Append("Phone=").Append(Phone.ToString()).Append(", ");
            builder.Append("Document=").Append(Document.ToString());
            builder.Append("Birthdate=").Append(Birthdate);
            builder.Append(')');
            return builder.ToString();
        }
    }
}