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
        public int Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Value of each installment
        /// </summary>
        public decimal Value
        {
            get;
            set;
        }

        /// <summary>
        /// No interest insallment quantity.
        /// </summary>
        public int NoInterestInstallmentQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the Installment Class
        /// </summary>
        public Installment() { }

        /// <summary>
        /// Initializes a new instance of the Installment Class
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="value"></param>
        public Installment(int quantity, decimal value) : this (quantity, value, new int()) { }

        /// <summary>
        /// Initializes a new instance of the Installment Class
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="value"></param>
        /// <param name="noInterestInstallmentQuantity"></param>
        public Installment(int quantity, decimal value, int noInterestInstallmentQuantity) 
        {   
                Quantity = quantity;
                Value = value;
                NoInterestInstallmentQuantity = noInterestInstallmentQuantity;
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
            builder.Append(",noInterestInstallmentQuantity=" + NoInterestInstallmentQuantity);
            builder.Append(")");
            return builder.ToString();
        }

    }
}
