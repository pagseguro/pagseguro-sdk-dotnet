using System;
using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain.Direct
{
    /// <summary>
    /// Represents the holder of the credit card payment
    /// </summary>
    public class Billing
    {
        /// <summary>
        /// Initializes a new instance of the Billing class
        /// </summary>
        public Billing()
        {
        }

        /// <summary>
        /// Billing address
        /// </summary>
        public Address Address
        {
            get;
            set;
        }
    }
}
