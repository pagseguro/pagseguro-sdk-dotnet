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
//   limitation

using System.Xml;
using System;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Installment;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace Uol.PagSeguro.XmlParse
{


    /// <summary>
    /// 
    /// </summary>
    public static class InstallmentSerializer
    {
        private const String Boolean = "Boolean";
        private const String Error = "error";
        private const String Errors = "errors";
        private const String Installments = "installments";
        private const String Quantity = "quantity";
        private const String InstallmentAmount = "installmentAmount";
        private const String TotalAmount = "totalAmount";
        private const String InterestFree = "interestFree";

        /// <summary>
        /// Read a direct payment session request result
        /// </summary>
        /// <param name="streamReader"></param>
        /// <param name="installments"></param>
        public static void Read(StreamReader streamReader, Installments installments)
        {

            StringReader sReader = new StringReader(streamReader.ReadToEnd());

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Dictionary<string, object> response = (Dictionary<string, object>)serializer.DeserializeObject(sReader.ReadToEnd());

            if (response.ContainsKey(InstallmentSerializer.Error))
            {
                if (Convert.ToBoolean(response[InstallmentSerializer.Error]))
                {
                    throw new ArgumentException(response[InstallmentSerializer.Errors].ToString());
                }
            }

            Installment installment = new Installment();

            foreach (var reader in response)
            {
                if (reader.Key.Equals(InstallmentSerializer.Installments))
                {
                    Dictionary<string, object> nv2 = (Dictionary<string, object>)reader.Value;
                    foreach (var obj in nv2)
                    {
                        installment.cardBrand = obj.Key.ToString();
                        String brand = installment.cardBrand;
                        object[] nv3 = (object[])obj.Value;

                        foreach (var items in nv3)
                        {
                            Dictionary<string, object> item = (Dictionary<string, object>)items;

                            installment.quantity = Convert.ToInt32(item[InstallmentSerializer.Quantity].ToString());
                            installment.amount = Convert.ToDecimal(item[InstallmentSerializer.InstallmentAmount].ToString());
                            installment.totalAmount = Convert.ToDecimal(item[InstallmentSerializer.TotalAmount].ToString());
                            installment.interestFree = Convert.ToBoolean(item[InstallmentSerializer.InterestFree].ToString());

                            installments.Add(installment);
                            installment = new Installment();
                            installment.cardBrand = brand;
                        }  
                    }
                }
            }
        }
    }
}
