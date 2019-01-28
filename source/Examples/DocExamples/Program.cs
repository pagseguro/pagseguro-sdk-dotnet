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
using System.Web;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace DocExamples
{
    internal class Program
    {
        private static void SenderExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();

            // Sender representa quem enviará dinheiro
            // na transação, normalmente o comprador
            Sender sender =
                new Sender(
                    "José Comprador",       // Nome
                    "comprador@uol.com.br", // Email
                    new Phone("11", "56273440") // Telefone
                    );

            paymentRequest.Sender = sender;
            if (paymentRequest.Sender != null)
            {
                Console.WriteLine(paymentRequest.Sender.Name);
                Console.WriteLine(paymentRequest.Sender.Email);
                if (paymentRequest.Sender.Phone != null)
                {
                    Console.WriteLine(paymentRequest.Sender.Phone.AreaCode);
                    Console.WriteLine(paymentRequest.Sender.Phone.Number);
                }
            }
        }

        private static void PhoneExample()
        {
            Phone phone = new Phone("11", "56273440");
        }

        private static void AddItemExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            paymentRequest.Items.Add(new Item("0001", "Notebook", 1, 2430.00m));
        }

        private static void ShippingTypeExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest
            {
                Shipping = new Shipping
                {
                    ShippingType = ShippingType.Pac
                }
            };
        }

        private static void ShippingAddressExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest
            {
                Shipping = new Shipping
                {
                    Address = new Address(
                "BRA",
                "SP",
                "São Paulo",
                "Jardim Paulistano",
                "01452002",
                "Av. Brig. Faria Lima",
                "1384",
                "5o. Andar")
                }
            };
        }

        private static void ExtraAmountExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest
            {
                ExtraAmount = 15.79m
            };
        }

        private static void ReferenceExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest
            {
                Reference = "REF1234"
            };
        }

        private static void RedirectUriExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest
            {
                RedirectUri = new Uri("http://lojamodelo.com.br/conclusao.html")
            };
        }

        private static void MaxAgeUsesExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest
            {
                MaxAge = 2880, // 2 dias
                MaxUses = 15 // 15 vezes
            };
        }

        private static void RegisterExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();
            // Preencher propriedades da requisição do pagamento aqui

            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

            // Criando o código de requisição de pagamento
            // e obtendo a URL da página de pagamento
            // do PagSeguro
            Uri paymentRedirectUri = paymentRequest.Register(credentials);

            //Response.Redirect(paymentRedirectUri.ToString());
        }

        private static void RequestExample(HttpRequest Request)
        {
            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

            string notificationType = Request.Form["notificationType"];
            string notificationCode = Request.Form["notificationCode"];

            if (notificationType == "transaction")
            {
                // obtendo o objeto transaction a partir do código de notificação
                Transaction transaction =
                    NotificationService.CheckTransaction(credentials, notificationCode);

                int status = transaction.TransactionStatus;
            }
        }

        private static void SearchByCodeExample()
        {
            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

            string transactionCode = "59A13D84-52DA-4AB8-B365-1E7D893052B0";

            Transaction transaction =
                TransactionSearchService.SearchByCode(credentials, transactionCode);

            Console.WriteLine(transaction.TransactionStatus);
            Console.WriteLine(transaction.GrossAmount);
        }

        private static void PaymentMethodExample(Transaction transaction)
        {
            PaymentMethod paymentMethod = transaction.PaymentMethod;
            int code = transaction.PaymentMethod.PaymentMethodCode;
            int type = transaction.PaymentMethod.PaymentMethodType;
        }

        private static void AddressExample()
        {
            Address address =
                new Address(
                    "BRA",
                    "SP",
                    "São Paulo",
                    "Jardim Paulistano",
                    "01452002",
                    "Av. Brig. Faria Lima",
                    "1384",
                    "5o. Andar");
        }

        private static void Addres2sExample()
        {
            Address address = new Address
            {
                Country = "BRA",
                State = "SP",
                City = "São Paulo",
                District = "Jardim Paulistano",
                PostalCode = "01452002",
                Street = "Av. Brig. Faria Lima",
                Number = "1384",
                Complement = "5o. Andar"
            };
        }

        private static void ShippingExample()
        {
            PaymentRequest paymentRequest = new PaymentRequest();

            Shipping shipping = new Shipping
            {
                ShippingType = ShippingType.Pac,
                Address =
                new Address(
                    "BRA",
                    "SP",
                    "São Paulo",
                    "Jardim Paulistano",
                    "01452002",
                    "Av. Brig. Faria Lima",
                    "1384",
                    "5o. Andar")
            };

            paymentRequest.Shipping = shipping;
        }

        private static void Shipping2Example(Transaction transaction)
        {
            if (transaction.Shipping != null)
            {
                Console.WriteLine(transaction.Shipping.ShippingType);
                Console.WriteLine(transaction.Shipping.Cost);
                if (transaction.Shipping.Address != null)
                {
                    Console.WriteLine(transaction.Shipping.Address.Street);
                    Console.WriteLine(transaction.Shipping.Address.Number);
                }
            }
        }

        private static void ItemExample(PaymentRequest paymentRequest)
        {
            Item item =
                new Item(
                    "0001",     // Identificador do item em seu site ou aplicação
                    "Notebook", // Descrição
                    1,          // Quantidade
                    2430.00m,   // Valor
                    1000,       // Peso em gramas
                    17.35m);    // Valor do frete

            paymentRequest.Items.Add(item);
        }

        private static void ItemsExample(PaymentRequest paymentRequest)
        {
            foreach (Item item in paymentRequest.Items)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Description);
                Console.WriteLine(item.Quantity);
                Console.WriteLine(item.Amount);
            }
        }

        private static void SearchByCodeExample2()
        {
            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

            // Código identificador da transação
            string transactionCode = "59A13D84-52DA-4AB8-B365-1E7D893052B0";

            // Realizando uma consulta de transação a partir do código identificador
            // para obter o objeto Transaction
            Transaction transaction =
                TransactionSearchService.SearchByCode(
                    credentials,
                    transactionCode);

            // Imprime o status da transação
            Console.WriteLine(transaction.TransactionStatus);
        }

        private static void SearchByDateExample()
        {
            bool isSandbox = false;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

            // Definindo a data de ínicio da consulta
            DateTime initialDate = new DateTime(2011, 06, 1, 08, 50, 0);

            // Definindo a data de término da consulta
            DateTime finalDate = new DateTime(2011, 06, 29, 10, 30, 0);

            // Definindo o número máximo de resultados por página
            int maxPageResults = 20;

            // Definindo o número da página
            int pageNumber = 1;

            // Realizando a consulta
            TransactionSearchResult result =
                TransactionSearchService.SearchByDate(
                    credentials,
                    initialDate,
                    finalDate,
                    pageNumber,
                    maxPageResults);
        }

        private static void TransactionSearchResultExample(TransactionSearchResult transactionSearchResult)
        {
            // Obtendo a data da realização da consulta
            DateTime date = transactionSearchResult.Date;

            // Obtendo a quantidade de resultados na página
            int resultsInThisPage = transactionSearchResult.Transactions.Count;

            // Obtendo a quantidade total de páginas
            int totalPages = transactionSearchResult.TotalPages;

            // Obtendo o número da página consultada
            int currentPage = transactionSearchResult.CurrentPage;

            // Iterando na lista de transações
            foreach (TransactionSummary transaction in transactionSearchResult.Transactions)
            {
                // Código da transação
                string code = transaction.Code;
                // Status da transação
                int status = transaction.TransactionStatus;
                // Refência da transação
                string reference = transaction.Reference;
                // Valor bruto da transação
                decimal amount = transaction.GrossAmount;
            }
        }

        private static void TransactionSummaryExample(TransactionSummary transactionSummary)
        {
            // Data da criação
            DateTime date = transactionSummary.Date;

            // Data da última atualização
            DateTime lastEventDate = transactionSummary.LastEventDate;

            // Código da transação
            string code = transactionSummary.Code;

            // Refência
            string reference = transactionSummary.Reference;

            // Valor bruto
            decimal grossAmount = transactionSummary.GrossAmount;

            // Tipo
            int type = transactionSummary.TransactionType;

            // Status
            int status = transactionSummary.TransactionStatus;

            // Valor líquido
            decimal netAmount = transactionSummary.NetAmount;

            // Valor das taxas cobradas
            decimal feeAmount = transactionSummary.FeeAmount;

            // Valor extra ou desconto
            decimal extraAmount = transactionSummary.ExtraAmount;

            // Tipo de meio de pagamento
            PaymentMethod paymentMethod = transactionSummary.PaymentMethod;
        }

        private static void Main(string[] args)
        {
        }
    }
}