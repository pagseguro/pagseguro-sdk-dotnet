using System.Collections.Generic;
using System.Text;

namespace Uol.PagSeguro.Domain.Direct
{
    /// <summary>
    /// Represents a payment request
    /// </summary>
    public class Checkout
    {
        private IList<Item> _items;
        private MetaData _metaData;
        private Parameter _parameter;

        /// <summary>
        /// 
        /// </summary>
        public string PaymentMode { get; set; }

        /// <summary>
        /// Specifies the e- mail should will get paid
        /// </summary>
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// Party that will be sending the money
        /// </summary>
        public Sender Sender { get; set; }

        /// <summary>
        /// Payment currency.
        /// </summary>
        /// <remarks>
        /// The expected currency values are defined in the <c cref="T:Uol.PagSeguro.Domain.Currency">Currencty</c> class.
        /// </remarks>
        public string Currency { get; set; }

        /// <summary>
        /// Products/items in this payment request
        /// </summary>
        public IList<Item> Items => _items ?? (_items = new List<Item>());

        /// <summary>
        /// Extra amount to be added to the transaction total
        /// </summary>
        /// <remarks>
        /// This value can be used to add an extra charge to the transaction
        /// or provide a discount in the case <c>ExtraAmount</c> is a negative value.
        /// </remarks>
        public decimal? ExtraAmount { get; set; }

        /// <summary>
        /// Reference code
        /// </summary>
        /// <remarks>
        /// Optional. You can use the reference code to store an identifier so you can
        /// associate the PagSeguro transaction to a transaction in your system.
        /// </remarks>
        public string Reference { get; set; }

        /// <summary>
        /// Shipping information associated with this payment request
        /// </summary>
        public Shipping Shipping { get; set; }

        /// <summary>
        /// How long this payment request will remain valid, in seconds.
        /// </summary>
        /// <remarks>
        /// Optional. After this payment request is submitted, the payment code returned
        /// will remain valid for the period specified here.
        /// </remarks>
        public int? MaxAge { get; set; }

        /// <summary>
        /// How many times the payment redirect uri returned by the payment web service can be accessed.
        /// </summary>
        /// <remarks>
        /// Optional. After this payment request is submitted, the payment redirect uri returned by
        /// the payment web service will remain valid for the number of uses specified here.
        ///
        /// </remarks>
        public int? MaxUses { get; set; }

        /// <summary>
        /// Determines for which url PagSeguro will send the order related notifications codes.
        /// <remarks>
        /// Optional. Any change happens in the transaction status, a new notification request will be send
        /// to this url. You can use that for update the related order.
        /// </remarks>
        /// </summary>
        public string NotificationUrl { get; set; }

        /// <summary>
        /// Meta Data reference
        /// </summary>
        public MetaData MetaData
        {
            get => _metaData ?? (_metaData = new MetaData());
            set => _metaData = value;
        }

        /// <summary>
        /// Parameter reference
        /// </summary>
        public Parameter Parameter
        {
            get => _parameter ?? (_parameter = new Parameter());
            set => _parameter = value;
        }

        /// <summary>
        /// Initializes a new instance of the PaymentRequest class
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public Checkout()
        {
            Currency = Constants.Currency.Brl;
        }

        /// <summary>
        /// Add a parameter for PagSeguro metadata checkout request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        // ReSharper disable once UnusedMember.Global
        public void AddMetaData(string key, string value)
        {
            MetaData.Items.Add(new MetaDataItem(key, value));
        }

        /// <summary>
        /// Add a parameter for PagSeguro metadata checkout request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="group"></param>
        // ReSharper disable once UnusedMember.Global
        public void AddMetaData(string key, string value, int? group)
        {
            MetaData.Items.Add(new MetaDataItem(key, value, group));
        }

        /// <summary>
        /// Add a parameter for PagSeguro checkout request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        // ReSharper disable once UnusedMember.Global
        public void AddParameter(string key, string value)
        {
            Parameter.Items.Add(new ParameterItem(key, value));
        }

        /// <summary>
        /// Add a parameter for PagSeguro checkout request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="group"></param>
        // ReSharper disable once UnusedMember.Global
        public void AddIndexedParameter(string key, string value, int? group)
        {
            Parameter.Items.Add(new ParameterItem(key, value, group));
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(GetType().Name).Append("(");
            builder.Append("Reference=").Append(Reference).Append(", ");
            var email = Sender?.Email ?? string.Empty;
            builder.Append("Sender.Email=").Append(email).Append(")");
            return builder.ToString();
        }
    }
}
    