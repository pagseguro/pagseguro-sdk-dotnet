using System;
using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain.Installment
{
    public class Installment : IComparable<Installment>
    {

        /// <summary>
        /// Credit card brand
        /// </summary>
        public String cardBrand
        {
            get;
            set;
        }

        /// <summary>
        /// Quantity of installments
        /// </summary>
        public Int32 quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Value of each installment
        /// </summary>
        public Decimal amount
        {
            get;
            set;
        }

        /// <summary>
        /// Total value of installments 
        /// </summary>
        public Decimal totalAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if is an interest free transaction
        /// </summary>
        public Boolean interestFree
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the Installment class
        /// </summary>
        /// <param name="cardBrand"></param>
        /// <param name="quantity"></param>
        /// <param name="amount"></param>
        /// <param name="totalAmount"></param>
        /// <param name="interestFree"></param>
        public Installment()
        {
        }

        public String ToString() {

            StringBuilder builder = new StringBuilder();

            builder.Append("Installment(");
            builder.Append("cardBrand=" + cardBrand);
            builder.Append(",quantity=" + quantity);
            builder.Append(",amount=" + amount);
            builder.Append(",totalAmount=" + totalAmount);
            builder.Append(",interestFree=" + interestFree);
            builder.Append(")");

            return builder.ToString();
        }


        int IComparable<Installment>.CompareTo(Installment other)
        {
            if (other == null)
            {
                return -1;
            }
            return quantity - other.quantity;
        }
    }
}
