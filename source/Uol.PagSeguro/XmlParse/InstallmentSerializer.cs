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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            Boolean startObject = false;
            Boolean nextIsCardBrand = false;

            StringReader sReader = new StringReader(streamReader.ReadToEnd());

            JsonTextReader reader = new JsonTextReader(sReader);

            Installment installment = new Installment();

            while (reader.Read())
            {
   
                if (startObject)
                {
                    if (reader.TokenType == JsonToken.PropertyName)
                    {

                        if (nextIsCardBrand)
                        {
                            installment.cardBrand = reader.Value.ToString();
                            nextIsCardBrand = false;
                        }
                        else
                        {
                            switch (reader.Value.ToString())
                            {

                                case InstallmentSerializer.Errors:
                                    while (reader.Read())
                                    {
                                        if (reader.TokenType == JsonToken.PropertyName)
                                        {
                                            throw new ArgumentException(reader.Value.ToString());
                                        }
                                    }
                                    break;
                                case InstallmentSerializer.Installments:
                                    nextIsCardBrand = true;
                                    break;
                                case InstallmentSerializer.Quantity:
                                    installment.quantity = Convert.ToInt32(reader.ReadAsString());
                                    break;
                                case InstallmentSerializer.InstallmentAmount:
                                    installment.amount = Convert.ToDecimal(reader.ReadAsString());
                                    break;
                                case InstallmentSerializer.TotalAmount:
                                    installment.totalAmount = Convert.ToDecimal(reader.ReadAsString());
                                    break;
                                case InstallmentSerializer.InterestFree:
                                    installment.interestFree = Convert.ToBoolean(reader.ReadAsString());
                                    break;
                                
                            }
                        }
                    }

                    if (reader.TokenType == JsonToken.EndObject)
                    {
                        startObject = false;
                        String aux = installment.cardBrand;
                        installments.Add(installment);
                        installment = new Installment();
                        installment.cardBrand = aux;
                    }
                }
                else
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        startObject = true;
                    }
                }
            }
        }
    }
}
