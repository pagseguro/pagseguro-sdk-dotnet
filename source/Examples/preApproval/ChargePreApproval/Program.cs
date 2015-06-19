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
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;
using Uol.PagSeguro.Constants;

namespace ChargePreApproval
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

            // Add an item for this preApproval payment request
            payment.Items.Add(new Item("0001", "Seguro contra roubo do Notebook", 1, 10.00m));

            // Sets a reference code for this payment request, it is useful to identify this payment in future notifications.
            payment.Reference = "REF1234";

            // Sets the previous preApproval code
            payment.PreApprovalCode = "235FD1B69494E6ACC4C37F8A60C05E89";

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);
                String result = PreApprovalService.ChargePreApproval(credentials, payment);

                Console.WriteLine(result);
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
