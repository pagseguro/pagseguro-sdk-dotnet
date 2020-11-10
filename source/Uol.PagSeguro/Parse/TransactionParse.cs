// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System.Collections.Generic;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.Domain.Direct;

namespace Uol.PagSeguro.Parse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TransactionParse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkout"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetData(Checkout checkout)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            // payment mode
            if (checkout.PaymentMode)
                data["paymentMode"] = checkout.PaymentMode;

            // receiver e-mail
            if (checkout.ReceiverEmail)
                data["receiverEmail"] = checkout.ReceiverEmail;
   
            // reference
            if (checkout.Reference)
                data["reference"] = checkout.Reference;

            // sender
            if (checkout.Sender)
            {
                if (checkout.Sender.Name)
                    data["senderName"] = checkout.Sender.Name;

                if (checkout.Sender.Email)
                    data["senderEmail"] = checkout.Sender.Email;

                if (checkout.Sender.Hash)
                    data["senderHash"] = checkout.Sender.Hash;

                // phone
                if (checkout.Sender.Phone)
                {
                    if (checkout.Sender.Phone.AreaCode)
                        data["senderAreaCode"] = checkout.Sender.Phone.AreaCode;

                    if (checkout.Sender.Phone.Number)
                        data["senderPhone"] = checkout.Sender.Phone.Number;
                }

                // documents
                if (checkout.Sender.Documents)
                {
                    var documents = checkout.Sender.Documents;
                    if (documents.Count == 1)
                    {
                        foreach (var document in documents)
                        {
                            if (document)
                            {
                                if (document.Type.Equals("Cadastro de Pessoa Física"))
                                    data["senderCPF"] = document.Value;
                                else
                                    data["senderCNPJ"] = document.Value;
                            }
                        }
                    }
                }
            }

            // currency
            if (checkout.Currency)
                data["currency"] = checkout.Currency;

            // items
            if (checkout.Items.Count > 0)
            {
                var i = 0;
                var items = checkout.Items;
                foreach (var item in items)
                {
                    i++;

                    if (item.Id)
                        data["itemId" + i] = item.Id;

                    if (item.Description)
                        data["itemDescription" + i] = item.Description;

                    data["itemQuantity" + i] = item.Quantity.ToString();
                    data["itemAmount" + i] = PagSeguroUtil.DecimalFormat(item.Amount);

                    if (item.Weight)
                        data["itemWeight" + i] = item.Weight.ToString();

                    if (item.ShippingCost)
                        data["itemShippingCost" + i] = PagSeguroUtil.DecimalFormat((decimal)item.ShippingCost);
                }
            }

            // extraAmount
            if (checkout.ExtraAmount)
                data["extraAmount"] = PagSeguroUtil.DecimalFormat((decimal)checkout.ExtraAmount);

            // shipping
            if (checkout.Shipping)
            {
                if (checkout.Shipping.ShippingType.HasValue)
                    data["shippingType"] = checkout.Shipping.ShippingType.Value.ToString();

                if (checkout.Shipping.Cost.HasValue)
                    data["shippingCost"] = PagSeguroUtil.DecimalFormat(checkout.Shipping.Cost.Value);

                // address
                if (checkout.Shipping.Address)
                {
                    if (checkout.Shipping.Address.Street)
                        data["shippingAddressStreet"] = checkout.Shipping.Address.Street;

                    if (checkout.Shipping.Address.Number)
                        data["shippingAddressNumber"] = checkout.Shipping.Address.Number;

                    if (checkout.Shipping.Address.Complement)
                        data["shippingAddressComplement"] = checkout.Shipping.Address.Complement;

                    if (checkout.Shipping.Address.City)
                        data["shippingAddressCity"] = checkout.Shipping.Address.City;

                    if (checkout.Shipping.Address.State)
                        data["shippingAddressState"] = checkout.Shipping.Address.State;

                    if (checkout.Shipping.Address.District)
                        data["shippingAddressDistrict"] = checkout.Shipping.Address.District;

                    if (checkout.Shipping.Address.PostalCode)
                        data["shippingAddressPostalCode"] = checkout.Shipping.Address.PostalCode;

                    if (checkout.Shipping.Address.Country)
                        data["shippingAddressCountry"] = checkout.Shipping.Address.Country;
                }
            }

            // maxAge
            if (checkout.MaxAge)
                data["maxAge"] = checkout.MaxAge.ToString();

            // maxUses
            if (checkout.MaxUses)
                data["maxUses"] = checkout.MaxUses.ToString();

            // notificationURL
            if (checkout.NotificationUrlnull)
                data["notificationURL"] = checkout.NotificationUrl;

            // metadata
            if (checkout.MetaData.Items.Count > 0)
            {
                var i = 0;
                var metaDataItems = checkout.MetaData.Items;
                foreach (var item in metaDataItems)
                {
                    if (PagSeguroUtil.IsEmpty(item.Key) || PagSeguroUtil.IsEmpty(item.Value))
                        continue;

                    i++;
                    data["metadataItemKey" + i] = item.Key;
                    data["metadataItemValue" + i] = item.Value;

                    if (item.Group != null)
                        data["metadataItemGroup" + i] = item.Group.ToString();
                }
            }

            // parameter
            if (checkout.Parameter.Items.Count > 0)
            {
                var parameterItems = checkout.Parameter.Items;
                foreach (var item in parameterItems)
                {
                    if (PagSeguroUtil.IsEmpty(item.Key) || PagSeguroUtil.IsEmpty(item.Value))
                        continue;

                    if (item.Group)
                        data[item.Key + "" + item.Group] = item.Value;
                    else
                        data[item.Key] = item.Value;
                }
            }

            switch (checkout)
            {
                //Verify if exists the credit card checkout data
                case CreditCardCheckout creditcard:
                    // billing address
                    if (creditcard.Billing?.Address != null)
                    {
                        if (creditcard.Billing.Address.Street)
                            data["billingAddressStreet"] = creditcard.Billing.Address.Street;

                        if (creditcard.Billing.Address.Number)
                            data["billingAddressNumber"] = creditcard.Billing.Address.Number;

                        if (creditcard.Billing.Address.Complement)
                            data["billingAddressComplement"] = creditcard.Billing.Address.Complement;

                        if (creditcard.Billing.Address.City)
                            data["billingAddressCity"] = creditcard.Billing.Address.City;

                        if (creditcard.Billing.Address.State)
                            data["billingAddressState"] = creditcard.Billing.Address.State;

                        if (creditcard.Billing.Address.District)
                            data["billingAddressDistrict"] = creditcard.Billing.Address.District;

                        if (creditcard.Billing.Address.PostalCode)
                            data["billingAddressPostalCode"] = creditcard.Billing.Address.PostalCode;

                        if (creditcard.Billing.Address.Country)
                            data["billingAddressCountry"] = creditcard.Billing.Address.Country;
                    }

                    // holder
                    if (creditcard.Holder)
                    {
                        //holder name
                        if (creditcard.Holder.Name)
                            data["creditCardHolderName"] = creditcard.Holder.Name;

                        //holder phone
                        if (creditcard.Holder.Phone)
                        {
                            if (creditcard.Holder.Phone.AreaCode)
                                data["creditCardHolderAreaCode"] = creditcard.Holder.Phone.AreaCode;

                            if (creditcard.Holder.Phone.Number)
                                data["creditCardHolderPhone"] = creditcard.Holder.Phone.Number;
                        }

                        //holder document
                        if (creditcard.Holder.Document?.Value)
                            data["creditCardHolderCPF"] = creditcard.Holder.Document.Value;

                        //holder birth date
                        if (creditcard.Holder.Birthdate)
                            data["creditCardHolderBirthDate"] = creditcard.Holder.Birthdate;
                    }

                    // token
                    if (creditcard.Token)
                        data["creditCardToken"] = creditcard.Token;

                    // installment
                    if (creditcard.Installment)
                    {
                        if (creditcard.Installment.Quantity > 0)
                            data["installmentQuantity"] = creditcard.Installment.Quantity.ToString();

                        if (creditcard.Installment.Value > 0)
                            data["installmentValue"] = PagSeguroUtil.DecimalFormat(creditcard.Installment.Value);

                        if (creditcard.Installment.NoInterestInstallmentQuantity > 0)
                            data["noInterestInstallmentQuantity"] = creditcard.Installment.NoInterestInstallmentQuantity.ToString();
                    }

                    // payment method
                    if (creditcard.PaymentMethod)
                        data["paymentMethod"] = creditcard.PaymentMethod;

                    break;

                //Verify if exists the boleto checkout data
                case BoletoCheckout _:
                    var boleto = (BoletoCheckout)checkout;

                    // payment method
                    if (boleto.PaymentMethod)
                        data["paymentMethod"] = boleto.PaymentMethod;

                    break;

                //Verify if exists the online debit checkout data
                case OnlineDebitCheckout _:
                    var onlineDebit = (OnlineDebitCheckout)checkout;

                    // payment method
                    if (onlineDebit.PaymentMethod)
                        data["paymentMethod"] = onlineDebit.PaymentMethod;

                    // bank name
                    if (onlineDebit.BankName)
                        data["bankName"] = onlineDebit.BankName;

                    break;
            }

            return data;
        }
    }
}
