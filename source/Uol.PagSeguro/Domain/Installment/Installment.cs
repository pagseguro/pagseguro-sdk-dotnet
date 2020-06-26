using System;
using System.Text;

namespace Uol.PagSeguro.Domain.Installment
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class Installment : IComparable<Installment>
    {
        /// <summary>
        /// Credit card brand
        /// </summary>
        public string CardBrand { get; set; }

        /// <summary>
        /// Quantity of installments
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Value of each installment
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Total value of installments 
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Indicates if is an interest free transaction
        /// </summary>
        public bool InterestFree { get; set; }

        /// <summary>
        /// Initializes a new instance of the Installment class
        /// </summary>
        // ReSharper disable once EmptyConstructor
        public Installment()
        {
        }

        /// <inheritdoc />
        public override string ToString() {

            var builder = new StringBuilder();

            builder.Append("Installment(");
            builder.Append("cardBrand=" + CardBrand);
            builder.Append(",quantity=" + Quantity);
            builder.Append(",amount=" + Amount);
            builder.Append(",totalAmount=" + TotalAmount);
            builder.Append(",interestFree=" + InterestFree);
            builder.Append(")");

            return builder.ToString();
        }

        int IComparable<Installment>.CompareTo(Installment other)
        {
            if (other == null)
                return -1;

            return Quantity - other.Quantity;
        }
    }
}
