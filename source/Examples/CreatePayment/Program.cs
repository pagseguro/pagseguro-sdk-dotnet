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
using System.Collections.Generic;
using System.Text;
using Uol.PagSeguro;
using System.Net;

namespace CreatePayment
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Substitute the parameters below with your credentials
            AccountCredentials credentials = new AccountCredentials("your@email.com", "your_token_here");

            try
            {
                PaymentRequest payment = new PaymentRequest();
                payment.Currency = Currency.Brl;

                payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
                payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

                payment.Reference = "REF1234";
                payment.Shipping = new Shipping();
                payment.Shipping.ShippingType = ShippingType.Sedex;
                payment.Shipping.Address = new Address("BRA", "SP", "Sao Paulo", "Jardim Paulistano", "01452002", "Av. Brig. Faria Lima", "1384", "5o andar");
                payment.Sender = new Sender("Joao Comprador", "comprador@uol.com.br", new Phone("11", "56273440"));
                
                Uri paymentRedirectUri = PaymentService.Register(credentials, payment);

                Console.WriteLine(paymentRedirectUri);
            }
            catch (PagSeguroServiceException exception)
            {
                if (exception.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: please verify if the credentials used in the web service call are correct.\n");
                }

                Console.WriteLine(exception);
            }
        }
    }
}
