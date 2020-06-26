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
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace RequestTransactionRefund
{
    class Program
    {
        static void Main(string[] args)
        {
            const bool isSandbox = false;
            const string transactionCode = "F3D9490291B54FA59F39B22AB9E76799";
            //const decimal refundValue = 150m;

            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            try
            {

                var credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

                // TODO: Substitute the code below with a valid transaction code for your transaction
                var result = RefundService.RequestRefund(credentials, transactionCode);
                //var result = RefundService.RequestRefund(credentials, transactionCode, refundValue);

                Console.WriteLine(result.ToString());

                Console.ReadKey();
            }
            catch (PagSeguroServiceException exception)
            {
                Console.WriteLine(exception.Message + "\n");

                foreach (var element in exception.Errors)
                {
                    Console.WriteLine(element + "\n");
                }
                Console.ReadKey();
            }
        }
    }
}