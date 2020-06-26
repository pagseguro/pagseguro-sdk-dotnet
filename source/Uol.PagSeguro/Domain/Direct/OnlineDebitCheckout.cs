namespace Uol.PagSeguro.Domain.Direct
{
    /// <inheritdoc />
    public class OnlineDebitCheckout : Checkout
    {
        /// <summary>
        /// Payment Method
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Bank Name
        /// </summary>
        public string BankName { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public OnlineDebitCheckout()
        {
            PaymentMethod = "eft";
        }
    }
}
