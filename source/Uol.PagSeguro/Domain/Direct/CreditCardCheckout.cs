namespace Uol.PagSeguro.Domain.Direct
{
    public class CreditCardCheckout : Checkout
    {
        /// <summary>
        /// Credit Card Token
        /// </summary>
        public string Token
        {
            get;
            set;
        }

        /// <summary>
        /// Installment
        /// </summary>
        public Installment Installment
        {
            get;
            set;
        }

        /// <summary>
        /// Holder
        /// </summary>
        public Holder Holder
        {
            get;
            set;
        }

        /// <summary>
        /// Billing
        /// </summary>
        public Billing Billing
        {
            get;
            set;
        }

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
        public CreditCardCheckout()
        {
            PaymentMethod = "CreditCard";
        }
    }
}