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
using Uol.PagSeguro.Constants.PreApproval;
using Uol.PagSeguro.Constants;
using System;

namespace Uol.PagSeguro.Parse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PaymentParse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetData(PaymentRequest payment)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            // reference
            if (payment.Reference != null)
            {
                data["reference"] = payment.Reference;
            }

            // sender
            if (payment.Sender != null)
            {

                if (payment.Sender.Name != null)
                {
                    data["senderName"] = payment.Sender.Name;
                }
                if (payment.Sender.Email != null)
                {
                    data["senderEmail"] = payment.Sender.Email;
                }

                // phone
                if (payment.Sender.Phone != null)
                {
                    if (payment.Sender.Phone.AreaCode != null)
                    {
                        data["senderAreaCode"] = payment.Sender.Phone.AreaCode;
                    }
                    if (payment.Sender.Phone.Number != null)
                    {
                        data["senderPhone"] = payment.Sender.Phone.Number;
                    }
                }

                // documents
                if (payment.Sender.Documents != null)
                {
                    var documents = payment.Sender.Documents;
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
            if (payment.Currency != null)
            {
                data["currency"] = payment.Currency;
            }

            // items
            if (payment.Items.Count > 0)
            {

                var items = payment.Items;
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

            //preApproval
            if (payment.PreApproval != null)
            {
                data["preApprovalCharge"] = payment.PreApproval.Charge;
                data["preApprovalName"] = payment.PreApproval.Name;
                data["preApprovalDetails"] = payment.PreApproval.Details;
                data["preApprovalPeriod"] = payment.PreApproval.Period;
                data["preApprovalFinalDate"] = payment.PreApproval.FinalDate.ToString("yyyy-MM-dd") + "T01:00:00.45-03:00";
                data["preApprovalMaxTotalAmount"] = payment.PreApproval.MaxTotalAmount.ToString("F").Replace(",", ".");
                data["preApprovalAmountPerPayment"] = payment.PreApproval.AmountPerPayment.ToString("F").Replace(",", ".");

                if (payment.PreApproval.Charge == Charge.Manual)
                {
                    data["preApprovalInitialDate"] = payment.PreApproval.InitialDate.ToString("yyyy-MM-dd") + "T01:00:00.45-03:00";
                    data["preApprovalMaxAmountPerPeriod"] = payment.PreApproval.MaxAmountPerPeriod.ToString("F").Replace(",", ".");
                    data["preApprovalMaxPaymentsPerPeriod"] = payment.PreApproval.MaxPaymentsPerPeriod.ToString();

                    if (payment.PreApproval.Period == Period.Yearly)
                        data["preApprovalDayOfYear"] = payment.PreApproval.DayOfYear.ToString();

                    if (payment.PreApproval.Period == Period.Monthly || payment.PreApproval.Period == Period.Bimonthly || payment.PreApproval.Period == Period.Trimonthly || payment.PreApproval.Period == Period.SemiAnnually)
                        data["preApprovalDayOfMonth"] = payment.PreApproval.DayOfMonth.ToString();

                    if (payment.PreApproval.Period == Period.Weekly)
                        data["preApprovalDayOfWeek"] = payment.PreApproval.DayOfWeek.ToString();
                }

                data["reviewUrl"] = payment.ReviewUri.ToString();
            }

            //preApproval payment
            if (payment.PreApprovalCode != null)
            {
                data["preApprovalCode"] = payment.PreApprovalCode;
            }

            // extraAmount
            if (payment.ExtraAmount != null)
            {
                data["extraAmount"] = PagSeguroUtil.DecimalFormat((decimal)payment.ExtraAmount);
            }

            // shipping
            if (payment.Shipping != null)
            {

                if (payment.Shipping.ShippingType != null && payment.Shipping.ShippingType.Value != null)
                {
                    data["shippingType"] = payment.Shipping.ShippingType.Value.ToString();
                }

                if (payment.Shipping.Cost != null)
                {
                    data["shippingCost"] = PagSeguroUtil.DecimalFormat((decimal)payment.Shipping.Cost);
                }

                // address
                if (payment.Shipping.Address != null)
                {
                    if (payment.Shipping.Address.Street != null)
                    {
                        data["shippingAddressStreet"] = payment.Shipping.Address.Street;
                    }
                    if (payment.Shipping.Address.Number != null)
                    {
                        data["shippingAddressNumber"] = payment.Shipping.Address.Number;
                    }
                    if (payment.Shipping.Address.Complement != null)
                    {
                        data["shippingAddressComplement"] = payment.Shipping.Address.Complement;
                    }
                    if (payment.Shipping.Address.City != null)
                    {
                        data["shippingAddressCity"] = payment.Shipping.Address.City;
                    }
                    if (payment.Shipping.Address.State != null)
                    {
                        data["shippingAddressState"] = payment.Shipping.Address.State;
                    }
                    if (payment.Shipping.Address.District != null)
                    {
                        data["shippingAddressDistrict"] = payment.Shipping.Address.District;
                    }
                    if (payment.Shipping.Address.PostalCode != null)
                    {
                        data["shippingAddressPostalCode"] = payment.Shipping.Address.PostalCode;
                    }
                    if (payment.Shipping.Address.Country != null)
                    {
                        data["shippingAddressCountry"] = payment.Shipping.Address.Country;
                    }
                }
            }

            // maxAge
            if (payment.MaxAge != null)
            {
                data["maxAge"] = payment.MaxAge.ToString();
            }
            // maxUses
            if (payment.MaxUses != null)
            {
                data["maxUses"] = payment.MaxUses.ToString();
            }

            // redirectURL
            if (payment.RedirectUri != null)
            {
                data["redirectURL"] = payment.RedirectUri.ToString();
            }

            // notificationURL
            if (payment.NotificationURL != null)
            {
                data["notificationURL"] = payment.NotificationURL;
            }

            // metadata
            if (payment.MetaData.Items.Count > 0)
            {
                int i = 0;
                var metaDataItems = payment.MetaData.Items;
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
            if (payment.Parameter.Items.Count > 0)
            {
                var parameterItems = payment.Parameter.Items;
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

            // paymentMethodConfig 
            if (payment.PaymentMethodConfig.Items.Count > 0)
            {
                int i = 0;
                var configItems = payment.PaymentMethodConfig.Items;
                foreach (PaymentMethodConfigItem item in configItems)
                {
                    if (!PagSeguroUtil.IsEmpty(item.Key) && !PagSeguroUtil.IsEmpty(item.Group))
                    {
                        i++;
                        data["paymentMethodGroup" + i] = item.Group;
                        data["paymentMethodConfigKey" + i + "_1"] = item.Key;
                        if (item.Key.Equals(PaymentMethodConfigKeys.DiscountPercent)) {
                            data["paymentMethodConfigValue" + i + "_1"] = PagSeguroUtil.DecimalFormat(item.Value);
                        } else {
                            data["paymentMethodConfigValue" + i + "_1"] = PagSeguroUtil.DoubleToInt(item.Value);
                        }
                    }
                }
            }

            // paymentMethodConfig 
            if (payment.AcceptedPaymentMethods.Items.Count > 0)
            {
                var acceptGroupList = new List<string>();
                var acceptNameList = new List<string>();
                var excludeGroupList = new List<string>();
                var excludeNameList = new List<string>();
                var config = payment.AcceptedPaymentMethods.Items;

                foreach (AcceptedPayments item in config)
                {

                    if (item.GetType() == typeof(AcceptPaymentMethod))
                    {
                        if (!acceptGroupList.Contains(item.Group))
                        {
                            acceptGroupList.Add(item.Group);
                        }
                        acceptNameList = item.Name;
                    }
                    if (item.GetType() == typeof(ExcludePaymentMethod))
                    {
                        if (!excludeGroupList.Contains(item.Group))
                        {
                            excludeGroupList.Add(item.Group);
                        }
                        excludeNameList = item.Name;
                    }
                }

                if (acceptGroupList.Count > 0 && acceptNameList.Count > 0)
                {
                    data["acceptPaymentMethodGroup"] = String.Join(",", acceptGroupList.ToArray());
                    data["acceptPaymentMethodName"] = String.Join(",", acceptNameList.ToArray());
                }
                if (excludeGroupList.Count > 0 && excludeNameList.Count > 0)
                {
                    data["excludePaymentMethodGroup"] = String.Join(",", excludeGroupList.ToArray());
                    data["excludePaymentMethodName"] = String.Join(",", excludeNameList.ToArray());
                }
            }

            return data;
        }
    }
}
