using System;
using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain.Direct
{

    /// <summary>
    /// Represents the installment of a credit card payment
    /// </summary>
    public class Installment {

        /// <summary>
        /// Quantity of installments
        /// </summary>
        public Int32 Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Value of each installment
        /// </summary>
        public Decimal Value
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the Installment Class
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="value"></param>
        public Installment(Int32? quantity = null, Decimal? value = null) 
        {
            if (quantity.HasValue)
            {
                Quantity = (Int32)quantity;
            }
            if (value.HasValue)
            {
                Value = (Decimal)value;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public String ToString() {
            StringBuilder builder = new StringBuilder();
            builder.Append("Installment(");
            builder.Append("quantity=" + Quantity);
            builder.Append(",value=" + Value);
            builder.Append(")");
            return builder.ToString();
        }

    }
}
