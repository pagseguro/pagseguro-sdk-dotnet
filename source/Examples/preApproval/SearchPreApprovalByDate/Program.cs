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

namespace FindPreApprovalByDate
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // TODO: Substitute the code below with a valid preApproval reference for your transaction
            DateTime initialDate = new DateTime(2015, 05, 14, 08, 50, 0);
            DateTime finalDate = new DateTime(2015, 06, 12, 08, 50, 0);
            int maxPageResults = 10;
            int pageNumber = 1;

            try
            {

                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);
                PreApprovalSearchResult result = 
                    PreApprovalSearchService.SearchByDate(
                        credentials, 
                        initialDate,
                        finalDate,
                        pageNumber,
                        maxPageResults
                    );

                if (result.PreApprovals.Count <= 0)
                {
                    Console.WriteLine("Nenhuma assinatura");
                }

                foreach (PreApprovalSummary preApproval in result.PreApprovals)
                {
                    Console.WriteLine("Começando listagem de assinaturas - \n");
                    Console.WriteLine(preApproval.ToString());
                    Console.WriteLine(" - Terminando listagem de assinaturas ");
                }

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
