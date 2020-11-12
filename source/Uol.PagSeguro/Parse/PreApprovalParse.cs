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

namespace Uol.PagSeguro.Parse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PreApprovalParse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="preApproval"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetData(PreApprovalRequest preApproval)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            // reference
            if (preApproval.Reference)
                data["reference"] = preApproval.Reference;

            // sender
            if (preApproval.Sender)
            {
                if (preApproval.Sender.Name)
                    data["senderName"] = preApproval.Sender.Name;

                if (preApproval.Sender.Email)
                    data["senderEmail"] = preApproval.Sender.Email;

                // phone
                if (preApproval.Sender.Phone)
                {
                    if (preApproval.Sender.Phone.AreaCode)
                        data["senderAreaCode"] = preApproval.Sender.Phone.AreaCode;

                    if (preApproval.Sender.Phone.Number)
                        data["senderPhone"] = preApproval.Sender.Phone.Number;
                }

                // documents
                if (preApproval.Sender.Documents)
                {
                    var documents = preApproval.Sender.Documents;
                    if (documents.Count == 1)
                    {
                        foreach (var document in documents)
                        {
                            if (document)
                                data["senderCPF"] = document.Value;
                        }
                    }
                }

                // address
                if (preApproval.Sender.Address)
                {
                    var address = preApproval.Sender.Address;
                    
                    // country
                    if (address.Country)
                    {
                        data["senderAddressCountry"] = address.Country;
                    }

                    // state
                    if (address.State)
                    {
                        data["senderAddressState"] = address.State;
                    }

                    // city
                    if (address.City)
                    {
                        data["senderAddressCity"] = address.City;
                    }

                    // PostalCode
                    if (address.PostalCode)
                    {
                        data["senderAddressPostalCode"] = address.PostalCode;
                    }

                    // PostalCode
                    if (address.District)
                    {
                        data["senderAddressDistrict"] = address.District;
                    }

                    // Complement
                    if (address.Complement)
                    {
                        data["senderAddressComplement"] = address.Complement;
                    }

                    // Address Number
                    if (address.Number)
                    {
                        data["senderAddressNumber"] = address.Number;
                    }

                    // Street
                    if (address.Street)
                    {
                        data["senderAddressStreet"] = address.Street;
                    }


                }
            }

            data["preApprovalCharge"] = preApproval.PreApproval.Charge;
            data["preApprovalName"] = preApproval.PreApproval.Name;
            data["preApprovalDetails"] = preApproval.PreApproval.Details;
            data["preApprovalPeriod"] = preApproval.PreApproval.Period;
            data["preApprovalFinalDate"] = preApproval.PreApproval.FinalDate.ToString("yyyy-MM-dd") + "T01:00:00.45-03:00";
            data["preApprovalMaxTotalAmount"] = preApproval.PreApproval.MaxTotalAmount.ToString("F").Replace(",", ".");
            data["preApprovalAmountPerPayment"] = preApproval.PreApproval.AmountPerPayment.ToString("F").Replace(",", ".");

            if (preApproval.PreApproval.Charge == Charge.Manual)
            {
                data["preApprovalInitialDate"] = preApproval.PreApproval.InitialDate.ToString("yyyy-MM-dd") + "T01:00:00.45-03:00";
                data["preApprovalMaxAmountPerPeriod"] = preApproval.PreApproval.MaxAmountPerPeriod.ToString("F").Replace(",", ".");
                data["preApprovalMaxPaymentsPerPeriod"] = preApproval.PreApproval.MaxPaymentsPerPeriod.ToString();

                if (preApproval.PreApproval.Period == Period.Yearly)
                    data["preApprovalDayOfYear"] = preApproval.PreApproval.DayOfYear.ToString();

                if (preApproval.PreApproval.Period == Period.Monthly ||
                    preApproval.PreApproval.Period == Period.Bimonthly ||
                    preApproval.PreApproval.Period == Period.Trimonthly ||
                    preApproval.PreApproval.Period == Period.SemiAnnually)
                    data["preApprovalDayOfMonth"] = preApproval.PreApproval.DayOfMonth.ToString();

                if (preApproval.PreApproval.Period == Period.Weekly)
                    data["preApprovalDayOfWeek"] = preApproval.PreApproval.DayOfWeek.ToString();
            }

            // currency
            if (preApproval.Currency)
                data["currency"] = preApproval.Currency;

            // redirectURL
            if (preApproval.RedirectUri)
                data["redirectURL"] = preApproval.RedirectUri.ToString();

            // redirectURL
            if (preApproval.ReviewUri)
                data["reviewUrl"] = preApproval.ReviewUri.ToString();

            // notificationURL
            if (preApproval.NotificationURL)
                data["notificationURL"] = preApproval.NotificationURL;

            // metadata
            if (preApproval.MetaData.Items.Count > 0)
            {
                var i = 0;
                var metaDataItems = preApproval.MetaData.Items;
                foreach (var item in metaDataItems)
                {
                    if (PagSeguroUtil.IsEmpty(item.Key) || PagSeguroUtil.IsEmpty(item.Value))
                        continue;

                    i++;
                    data["metadataItemKey" + i] = item.Key;
                    data["metadataItemValue" + i] = item.Value;

                    if (item.Group)
                        data["metadataItemGroup" + i] = item.Group.ToString();
                }
            }

            // parameter
            if (preApproval.Parameter.Items.Count <= 0)
                return data;

            var parameterItems = preApproval.Parameter.Items;
            foreach (var item in parameterItems)
            {
                if (PagSeguroUtil.IsEmpty(item.Key) || PagSeguroUtil.IsEmpty(item.Value))
                    continue;

                if (item.Group)
                    data[item.Key + "" + item.Group] = item.Value;
                else
                    data[item.Key] = item.Value;
            }

            return data;
        }
    }
}
