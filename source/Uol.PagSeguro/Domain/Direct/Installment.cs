using System.Text;

namespace Uol.PagSeguro.Domain.Direct
{
    /// <summary>
    /// Represents the installment of a credit card payment
    /// </summary>
    public class Installment
    {
        /// <summary>
        /// Quantity of installments
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Value of each installment
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// No interest insallment quantity.
        /// </summary>
        public int NoInterestInstallmentQuantity { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the Installment Class
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public Installment() : this (default(int), default(decimal), default(int)) { }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the Installment Class
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="value"></param>
        // ReSharper disable once UnusedMember.Global
        public Installment(int quantity, decimal value) : this (quantity, value, default(int)) { }

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
        public override string ToString() {
            var builder = new StringBuilder();
            builder.Append("Installment(");
            builder.Append("quantity=" + Quantity);
            builder.Append(",value=" + Value);
            builder.Append(",noInterestInstallmentQuantity=" + NoInterestInstallmentQuantity);
            builder.Append(")");
            return builder.ToString();
        }
    }
}
