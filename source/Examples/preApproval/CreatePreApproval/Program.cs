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

namespace CreatePreApproval
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Instantiate a new preApproval request
            PreApprovalRequest preApproval = new PreApprovalRequest();

            // Sets the currency
            preApproval.Currency = Currency.Brl;

            // Sets a reference code for this preApproval request, it is useful to identify this payment in future notifications.
            preApproval.Reference = "REF1234";

            // Sets your customer information.
            preApproval.Sender = new Sender(
                "Joao Comprador",
                "comprador@uol.com.br",
                new Phone("11", "56273440")
            );

            // Sets the preApproval informations
            var now = DateTime.Now;
            preApproval.PreApproval = new PreApproval();
            preApproval.PreApproval.Charge = Charge.Manual;
            preApproval.PreApproval.Name = "Seguro contra roubo do Notebook";
            preApproval.PreApproval.AmountPerPayment = 100.00m;
            preApproval.PreApproval.MaxAmountPerPeriod = 100.00m;
            preApproval.PreApproval.MaxPaymentsPerPeriod = 5;
            preApproval.PreApproval.Details = string.Format("Todo dia {0} será cobrado o valor de {1} referente ao seguro contra roubo do Notebook.", now.Day, preApproval.PreApproval.AmountPerPayment.ToString("C2"));
            preApproval.PreApproval.Period = Period.Monthly;
            preApproval.PreApproval.DayOfMonth = now.Day;
            preApproval.PreApproval.InitialDate = now;
            preApproval.PreApproval.FinalDate = now.AddMonths(6);
            preApproval.PreApproval.MaxTotalAmount = 600.00m;

            // Sets the url used by PagSeguro for redirect user after ends checkout process
            preApproval.RedirectUri = new Uri("http://www.lojamodelo.com.br/retorno");

            // Sets the url used for user review the signature or read the rules
            preApproval.ReviewUri = new Uri("http://www.lojamodelo.com.br/revisao");

            SenderDocument senderCPF = new SenderDocument(Documents.GetDocumentByType("CPF"), "12345678909");
            preApproval.Sender.Documents.Add(senderCPF);

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

                Uri preApprovalRedirectUri = preApproval.Register(credentials);

                Console.WriteLine("URL do pagamento : " + preApprovalRedirectUri);
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