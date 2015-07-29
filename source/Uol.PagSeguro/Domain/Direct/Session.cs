using System;
using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain.Direct
{

    /// <summary>
    /// Represents a direct payment session
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Initializes a new instance of the Session class
        /// </summary>
        public Session()
        {
        }

        /// <summary>
        /// Identifier
        /// </summary>
        public string id
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Session").Append("(");
            builder.Append("id=").Append(id).Append(")");
            return builder.ToString();
        }
    }
}
