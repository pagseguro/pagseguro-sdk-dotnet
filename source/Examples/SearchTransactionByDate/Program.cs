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

namespace SearchTransactionByCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Substitute the parameters below with your credentials
            AccountCredentials credentials = new AccountCredentials("your@email.com", "your_token_here");

            try
            {
                // Definindo a data de ínicio da consulta 
                DateTime initialDate = DateTime.Now.AddMonths(-3);

                // Definindo a data de término da consulta
                DateTime finalDate = DateTime.Now;

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

                foreach (TransactionSummary transaction in result.Transactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
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
