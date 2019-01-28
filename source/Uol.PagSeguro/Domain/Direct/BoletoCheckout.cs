namespace Uol.PagSeguro.Domain.Direct
{
    public class BoletoCheckout : Checkout
    {
        /// <summary>
        /// Payment Method
        /// </summary>
        public string PaymentMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BoletoCheckout()
        {
            PaymentMethod = "Boleto";
        }
    }
}