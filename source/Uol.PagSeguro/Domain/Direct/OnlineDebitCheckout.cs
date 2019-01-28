namespace Uol.PagSeguro.Domain.Direct
{
    public class OnlineDebitCheckout : Checkout
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
        /// Bank Name
        /// </summary>
        public string BankName
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OnlineDebitCheckout()
        {
            PaymentMethod = "eft";
        }
    }
}