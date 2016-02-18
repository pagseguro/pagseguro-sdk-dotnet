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
using Uol.PagSeguro.Domain;
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
            if (checkout.PaymentMode != null)
            {
                data["paymentMode"] = checkout.PaymentMode;
            }

            // receiver e-mail
            if (checkout.ReceiverEmail != null)
            {
                data["receiverEmail"] = checkout.ReceiverEmail;
            }
   
            // reference
            if (checkout.Reference != null)
            {
                data["reference"] = checkout.Reference;
            }

            // sender
            if (checkout.Sender != null)
            {

                if (checkout.Sender.Name != null)
                {
                    data["senderName"] = checkout.Sender.Name;
                }
                if (checkout.Sender.Email != null)
                {
                    data["senderEmail"] = checkout.Sender.Email;
                }
                if (checkout.Sender.Hash != null)
                {
                    data["senderHash"] = checkout.Sender.Hash;
                }

                // phone
                if (checkout.Sender.Phone != null)
                {
                    if (checkout.Sender.Phone.AreaCode != null)
                    {
                        data["senderAreaCode"] = checkout.Sender.Phone.AreaCode;
                    }
                    if (checkout.Sender.Phone.Number != null)
                    {
                        data["senderPhone"] = checkout.Sender.Phone.Number;
                    }
                }

                // documents
                if (checkout.Sender.Documents != null)
                {
                    var documents = checkout.Sender.Documents;
                    if (documents.Count == 1)
                    {
                        foreach (SenderDocument document in documents)
                        {
                            if (document != null)
                            {
                                if (document.Type.Equals("Cadastro de Pessoa Física")) {
                                    data["senderCPF"] = document.Value;
                                } else {
                                    data["senderCNPJ"] = document.Value;
                                }
                            }
                        }
                    }
                }
            }

            // currency
            if (checkout.Currency != null)
            {
                data["currency"] = checkout.Currency;
            }

            // items
            if (checkout.Items.Count > 0)
            {

                var items = checkout.Items;
                int i = 0;
                foreach (Item item in items)
                {

                    i++;

                    if (item.Id != null)
                    {
                        data["itemId" + i] = item.Id;
                    }
                    if (item.Description != null)
                    {
                        data["itemDescription" + i] = item.Description;
                    }
                    if (item.Quantity != null)
                    {
                        data["itemQuantity" + i] = item.Quantity.ToString();
                    }
                    if (item.Amount != null)
                    {
                        data["itemAmount" + i] = PagSeguroUtil.DecimalFormat(item.Amount);
                    }
                    if (item.Weight != null)
                    {
                        data["itemWeight" + i] = item.Weight.ToString();
                    }
                    if (item.ShippingCost != null)
                    {
                        data["itemShippingCost" + i] = PagSeguroUtil.DecimalFormat((decimal)item.ShippingCost);
                    }
                }
            }

            // extraAmount
            if (checkout.ExtraAmount != null)
            {
                data["extraAmount"] = PagSeguroUtil.DecimalFormat((decimal)checkout.ExtraAmount);
            }

            // shipping
            if (checkout.Shipping != null)
            {

                if (checkout.Shipping.ShippingType != null && checkout.Shipping.ShippingType.Value != null)
                {
                    data["shippingType"] = checkout.Shipping.ShippingType.Value.ToString();
                }

                if (checkout.Shipping.Cost != null)
                {
                    data["shippingCost"] = PagSeguroUtil.DecimalFormat((decimal)checkout.Shipping.Cost);
                }

                // address
                if (checkout.Shipping.Address != null)
                {
                    if (checkout.Shipping.Address.Street != null)
                    {
                        data["shippingAddressStreet"] = checkout.Shipping.Address.Street;
                    }
                    if (checkout.Shipping.Address.Number != null)
                    {
                        data["shippingAddressNumber"] = checkout.Shipping.Address.Number;
                    }
                    if (checkout.Shipping.Address.Complement != null)
                    {
                        data["shippingAddressComplement"] = checkout.Shipping.Address.Complement;
                    }
                    if (checkout.Shipping.Address.City != null)
                    {
                        data["shippingAddressCity"] = checkout.Shipping.Address.City;
                    }
                    if (checkout.Shipping.Address.State != null)
                    {
                        data["shippingAddressState"] = checkout.Shipping.Address.State;
                    }
                    if (checkout.Shipping.Address.District != null)
                    {
                        data["shippingAddressDistrict"] = checkout.Shipping.Address.District;
                    }
                    if (checkout.Shipping.Address.PostalCode != null)
                    {
                        data["shippingAddressPostalCode"] = checkout.Shipping.Address.PostalCode;
                    }
                    if (checkout.Shipping.Address.Country != null)
                    {
                        data["shippingAddressCountry"] = checkout.Shipping.Address.Country;
                    }
                }

            }

            // maxAge
            if (checkout.MaxAge != null)
            {
                data["maxAge"] = checkout.MaxAge.ToString();
            }
            // maxUses
            if (checkout.MaxUses != null)
            {
                data["maxUses"] = checkout.MaxUses.ToString();
            }

            // notificationURL
            if (checkout.NotificationURL != null)
            {
                data["notificationURL"] = checkout.NotificationURL;
            }

            // metadata
            if (checkout.MetaData.Items.Count > 0)
            {
                int i = 0;
                var metaDataItems = checkout.MetaData.Items;
                foreach (MetaDataItem item in metaDataItems)
                {
                    if (!PagSeguroUtil.IsEmpty(item.Key) && !PagSeguroUtil.IsEmpty(item.Value))
                    {
                        i++;
                        data["metadataItemKey" + i] = item.Key;
                        data["metadataItemValue" + i] = item.Value;

                        if (item.Group != null)
                        {
                            data["metadataItemGroup" + i] = item.Group.ToString();
                        }
                    }
                }
            }

            // parameter
            if (checkout.Parameter.Items.Count > 0)
            {
                var parameterItems = checkout.Parameter.Items;
                foreach (ParameterItem item in parameterItems)
                {
                    if (!PagSeguroUtil.IsEmpty(item.Key) && !PagSeguroUtil.IsEmpty(item.Value))
                    {
                        if (item.Group != null)
                        {
                            data[item.Key + "" + item.Group] = item.Value;
                        }
                        else
                        {
                            data[item.Key] = item.Value;
                        }
                    }
                }
            }

            //Verify if exists the credit card checkout data
            if (checkout is CreditCardCheckout)
            {
                CreditCardCheckout creditcard = (CreditCardCheckout)checkout;
                // billing
                if (creditcard.Billing != null)
                {

                    // address
                    if (creditcard.Billing.Address != null)
                    {
                        if (creditcard.Billing.Address.Street != null)
                        {
                            data["billingAddressStreet"] = creditcard.Billing.Address.Street;
                        }
                        if (creditcard.Billing.Address.Number != null)
                        {
                            data["billingAddressNumber"] = creditcard.Billing.Address.Number;
                        }
                        if (creditcard.Billing.Address.Complement != null)
                        {
                            data["billingAddressComplement"] = creditcard.Billing.Address.Complement;
                        }
                        if (creditcard.Billing.Address.City != null)
                        {
                            data["billingAddressCity"] = creditcard.Billing.Address.City;
                        }
                        if (creditcard.Billing.Address.State != null)
                        {
                            data["billingAddressState"] = creditcard.Billing.Address.State;
                        }
                        if (creditcard.Billing.Address.District != null)
                        {
                            data["billingAddressDistrict"] = creditcard.Billing.Address.District;
                        }
                        if (creditcard.Billing.Address.PostalCode != null)
                        {
                            data["billingAddressPostalCode"] = creditcard.Billing.Address.PostalCode;
                        }
                        if (creditcard.Billing.Address.Country != null)
                        {
                            data["billingAddressCountry"] = creditcard.Billing.Address.Country;
                        }
                    }
                }

                // holder
                if (creditcard.Holder != null)
                {
                    //holder name
                    if (creditcard.Holder.Name != null)
                    {
                        data["creditCardHolderName"] = creditcard.Holder.Name;
                    }
                    //holder phone
                    if (creditcard.Holder.Phone != null)
                    {
                        if (creditcard.Holder.Phone.AreaCode != null)
                        {
                            data["creditCardHolderAreaCode"] = creditcard.Holder.Phone.AreaCode;
                        }
                        if (creditcard.Holder.Phone.Number != null)
                        {
                            data["creditCardHolderPhone"] = creditcard.Holder.Phone.Number;
                        }
                    }
                    //holder document
                    if (creditcard.Holder.Document != null)
                    {
                        if (creditcard.Holder.Document.Value != null)
                        {
                            data["creditCardHolderCPF"] = creditcard.Holder.Document.Value;
                        }
                    }
                    //holder birth date
                    if (creditcard.Holder.Birthdate != null)
                    {
                        data["creditCardHolderBirthDate"] = creditcard.Holder.Birthdate;
                    }
                }

                // token
                if (creditcard.Token != null)
                {
                    data["creditCardToken"] = creditcard.Token;
                }

                // installment
                if (creditcard.Installment != null)
                {
                    if (creditcard.Installment.Quantity > 0)
                    {
                        data["installmentQuantity"] = creditcard.Installment.Quantity.ToString();
                    }
                    if (creditcard.Installment.Value > 0)
                    {
                        data["installmentValue"] = PagSeguroUtil.DecimalFormat((decimal)creditcard.Installment.Value);
                    }
                    if (creditcard.Installment.NoInterestInstallmentQuantity > 0)
                    {
                        data["noInterestInstallmentQuantity"] = creditcard.Installment.NoInterestInstallmentQuantity.ToString();
                    }
                }

                // payment method
                if (creditcard.PaymentMethod != null)
                {
                    data["paymentMethod"] = creditcard.PaymentMethod;
                }
            }


            //Verify if exists the boleto checkout data
            if (checkout is BoletoCheckout)
            {
                BoletoCheckout boleto = (BoletoCheckout)checkout;

                // payment method
                if (boleto.PaymentMethod != null)
                {
                    data["paymentMethod"] = boleto.PaymentMethod;
                }
            }

            //Verify if exists the online debit checkout data
            if (checkout is OnlineDebitCheckout)
            {
                OnlineDebitCheckout onlineDebit = (OnlineDebitCheckout)checkout;

                // payment method
                if (onlineDebit.PaymentMethod != null)
                {
                    data["paymentMethod"] = onlineDebit.PaymentMethod;
                }

                // bank name
                if (onlineDebit.BankName != null)
                {
                    data["bankName"] = onlineDebit.BankName;
                }
            }

            return data;
        }
    }
}
