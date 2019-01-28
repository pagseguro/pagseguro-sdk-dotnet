using System.Collections.Generic;
using Uol.PagSeguro.NetStandard.Domain;
using Uol.PagSeguro.NetStandard.Domain.Direct;
using Uol.PagSeguro.NetStandard.Util;

namespace Uol.PagSeguro.NetStandard.Parse
{
    internal static class TransactionParse
    {
        public static IDictionary<string, string> GetData(Checkout checkout)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            if (checkout.PaymentMode != null)
            {
                data["paymentMode"] = checkout.PaymentMode;
            }

            if (checkout.ReceiverEmail != null)
            {
                data["receiverEmail"] = checkout.ReceiverEmail;
            }

            if (checkout.Reference != null)
            {
                data["reference"] = checkout.Reference;
            }

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

                if (checkout.Sender.Documents != null)
                {
                    var documents = checkout.Sender.Documents;
                    if (documents.Count == 1)
                    {
                        foreach (SenderDocument document in documents)
                        {
                            if (document != null)
                            {
                                if (document.Type.Equals("Cadastro de Pessoa Física"))
                                {
                                    data["senderCPF"] = document.Value;
                                }
                                else
                                {
                                    data["senderCNPJ"] = document.Value;
                                }
                            }
                        }
                    }
                }
            }

            if (checkout.Currency != null)
            {
                data["currency"] = checkout.Currency;
            }

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
                    data["itemQuantity" + i] = item.Quantity.ToString();
                    data["itemAmount" + i] = PagSeguroUtil.DecimalFormat(item.Amount);
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

            if (checkout.ExtraAmount != null)
            {
                data["extraAmount"] = PagSeguroUtil.DecimalFormat((decimal)checkout.ExtraAmount);
            }

            if (checkout.Shipping != null)
            {
                if (checkout.Shipping.ShippingType != null)
                {
                    data["shippingType"] = checkout.Shipping.ShippingType.Value.ToString();
                }

                if (checkout.Shipping.Cost != null)
                {
                    data["shippingCost"] = PagSeguroUtil.DecimalFormat((decimal)checkout.Shipping.Cost);
                }

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

            if (checkout.MaxAge != null)
            {
                data["maxAge"] = checkout.MaxAge.ToString();
            }
            if (checkout.MaxUses != null)
            {
                data["maxUses"] = checkout.MaxUses.ToString();
            }

            if (checkout.NotificationURL != null)
            {
                data["notificationURL"] = checkout.NotificationURL;
            }

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

            if (checkout is CreditCardCheckout)
            {
                CreditCardCheckout creditcard = checkout as CreditCardCheckout;
                if (creditcard.Billing != null)
                {
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

                if (creditcard.Holder != null)
                {
                    if (creditcard.Holder.Name != null)
                    {
                        data["creditCardHolderName"] = creditcard.Holder.Name;
                    }
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
                    if (creditcard.Holder.Document != null)
                    {
                        if (creditcard.Holder.Document.Value != null)
                        {
                            data["creditCardHolderCPF"] = creditcard.Holder.Document.Value;
                        }
                    }
                    if (creditcard.Holder.Birthdate != null)
                    {
                        data["creditCardHolderBirthDate"] = creditcard.Holder.Birthdate;
                    }
                }

                if (creditcard.Token != null)
                {
                    data["creditCardToken"] = creditcard.Token;
                }

                if (creditcard.Installment != null)
                {
                    if (creditcard.Installment.Quantity > 0)
                    {
                        data["installmentQuantity"] = creditcard.Installment.Quantity.ToString();
                    }
                    if (creditcard.Installment.Value > 0)
                    {
                        data["installmentValue"] = PagSeguroUtil.DecimalFormat(creditcard.Installment.Value);
                    }
                    if (creditcard.Installment.NoInterestInstallmentQuantity > 0)
                    {
                        data["noInterestInstallmentQuantity"] = creditcard.Installment.NoInterestInstallmentQuantity.ToString();
                    }
                }

                if (creditcard.PaymentMethod != null)
                {
                    data["paymentMethod"] = creditcard.PaymentMethod;
                }
            }

            if (checkout is BoletoCheckout boleto)
            {
                if (boleto.PaymentMethod != null)
                {
                    data["paymentMethod"] = boleto.PaymentMethod;
                }
            }

            if (checkout is OnlineDebitCheckout onlineDebitCheckout)
            {
                OnlineDebitCheckout onlineDebit = onlineDebitCheckout;

                if (onlineDebit.PaymentMethod != null)
                {
                    data["paymentMethod"] = onlineDebit.PaymentMethod;
                }

                if (onlineDebit.BankName != null)
                {
                    data["bankName"] = onlineDebit.BankName;
                }
            }

            return data;
        }
    }
}