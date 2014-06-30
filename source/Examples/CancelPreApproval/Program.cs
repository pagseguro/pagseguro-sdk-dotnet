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

namespace CancelPreApproval
{
    class Program
    {
        static void Main(string[] args)
        {
           
            bool sandbox = true;

            // TODO: Substitute the parameters below with your credentials on XML config
            //AccountCredentials credentials = PagSeguroConfiguration.Credentials(sandbox);

            AccountCredentials credentials;
            if (sandbox) 
            {
                // TODO: Substitute the parameters below with your sandbox credentials
                credentials = new AccountCredentials("your_sandbox@email.com", "your_sandbox_token_here");
            } 
            else 
            {
                // TODO: Substitute the parameters below with your production credentials
                credentials = new AccountCredentials("your@email.com", "your_token_here");
            }

            try
            {
                // TODO: Substitute the code below with a valid transaction code for your transaction
                bool cancelResult = PreApprovalService.CancelPreApproval(credentials, "3DFAD3123412340334A96F9136C38804");

                Console.WriteLine(cancelResult);
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