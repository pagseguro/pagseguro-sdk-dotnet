namespace Uol.PagSeguro.Domain.Direct
{
    /// <inheritdoc />
    public class BoletoCheckout : Checkout
    {
        /// <summary>
        /// Payment Method
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public BoletoCheckout()
        {
            PaymentMethod = "Boleto";
        }
    }
}
