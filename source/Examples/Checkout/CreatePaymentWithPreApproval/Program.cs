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

using System;
using System.Net;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Constants.PreApproval;
using System.Diagnostics;

namespace CreatePaymentWithPreApproval
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Instantiate a new payment request
            PaymentRequest payment = new PaymentRequest();

            // Sets the currency
            payment.Currency = Currency.Brl;

            // Add an item for this payment request
            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430.00m));


            // Add another item for this payment request
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 150.99m));

            // Sets a reference code for this payment request, it is useful to identify this payment in future notifications.
            payment.Reference = "REF1234";

            // Sets shipping information for this payment request
            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.Sedex;

            //Passando valor para ShippingCost
            payment.Shipping.Cost = 10.00m;

            payment.Shipping.Address = new Address(
                "BRA",
                "SP",
                "Sao Paulo",
                "Jardim Paulistano",
                "01452002",
                "Av. Brig. Faria Lima",
                "1384",
                "5o andar"
            );

            // Sets your customer information.
            payment.Sender = new Sender(
                "Joao Comprador",
                "comprador@uol.com.br",
                new Phone("11", "56273440")
            );

            // Sets the url used by PagSeguro for redirect user after ends checkout process
            payment.RedirectUri = new Uri("http://www.lojamodelo.com.br");

            // Add checkout metadata information
            payment.AddMetaData(MetaDataItemKeys.GetItemKeyByDescription("CPF do passageiro"), "123.456.789-09", 1);
            payment.AddMetaData("PASSENGER_PASSPORT", "23456", 1);

            // Another way to set checkout parameters
            payment.AddParameter("senderBirthday", "07/05/1980");
            payment.AddIndexedParameter("itemColor", "verde", 1);
            payment.AddIndexedParameter("itemId", "0003", 3);
            payment.AddIndexedParameter("itemDescription", "Mouse", 3);
            payment.AddIndexedParameter("itemQuantity", "1", 3);
            payment.AddIndexedParameter("itemAmount", "200.00", 3);

            SenderDocument senderCPF = new SenderDocument(Documents.GetDocumentByType("CPF"), "12345678909");
            payment.Sender.Documents.Add(senderCPF);

            // Sets the preApproval informations
            payment.PreApproval = new PreApproval();
            var now = DateTime.Now;

            // Only works with Manual
            payment.PreApproval.Charge = Charge.Manual;

            payment.PreApproval.Name = "Seguro contra roubo do Notebook";
            payment.PreApproval.AmountPerPayment = 100.00m;
            payment.PreApproval.MaxAmountPerPeriod = 100.00m;
            payment.PreApproval.Details = string.Format("Todo dia {0} será cobrado o valor de {1} referente ao seguro contra roubo do Notebook.", now.Day, payment.PreApproval.AmountPerPayment.ToString("C2"));
            payment.PreApproval.Period = Period.Monthly;
            payment.PreApproval.DayOfMonth = now.Day;
            payment.PreApproval.InitialDate = now;
            payment.PreApproval.FinalDate = now.AddMonths(6);
            payment.PreApproval.MaxTotalAmount = 600.00m;
            payment.PreApproval.MaxPaymentsPerPeriod = 1;

            payment.ReviewUri = new Uri("http://www.lojamodelo.com.br/revisao");

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

                Uri paymentRedirectUri = payment.Register(credentials);

                Console.WriteLine("URL do pagamento : " + paymentRedirectUri);
                Console.ReadKey();
            }
            catch (PagSeguroServiceException exception)
            {
                Console.WriteLine(exception.Message + "\n");

                foreach (ServiceError element in exception.Errors)
                {
                    Console.WriteLine(element + "\n");
                }
                Console.ReadKey();
            }
        }
    }
}