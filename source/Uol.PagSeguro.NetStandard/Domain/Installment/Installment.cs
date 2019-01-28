using System;
using System.Text;

namespace Uol.PagSeguro.NetStandard.Domain.Installment
{
    public class Installment : IComparable<Installment>
    {
        /// <summary>
        /// Credit card brand
        /// </summary>
        public string cardBrand
        {
            get;
            set;
        }

        /// <summary>
        /// Quantity of installments
        /// </summary>
        public int quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Value of each installment
        /// </summary>
        public decimal amount
        {
            get;
            set;
        }

        /// <summary>
        /// Total value of installments
        /// </summary>
        public decimal totalAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates if is an interest free transaction
        /// </summary>
        public bool interestFree
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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Installment(");
            builder.Append("cardBrand=").Append(cardBrand);
            builder.Append(",quantity=").Append(quantity);
            builder.Append(",amount=").Append(amount);
            builder.Append(",totalAmount=").Append(totalAmount);
            builder.Append(",interestFree=").Append(interestFree);
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