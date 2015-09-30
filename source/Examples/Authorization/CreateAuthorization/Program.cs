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
using Uol.PagSeguro.Domain.Authorization;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;

namespace CreateAuthorization
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            AuthorizationRequest authorization = new AuthorizationRequest();

            authorization.Reference = "REF1234";

            authorization.RedirectURL = "http://www.lojamodelo.com.br/redirect";

            authorization.NotificationURL = "http://www.lojamodelo.com.br/notification";

            authorization.addPermission(PermissionType.CREATE_CHECKOUTS);
            authorization.addPermission(PermissionType.DIRECT_PAYMENT);
            authorization.addPermission(PermissionType.MANAGE_PAYMENT_PRE_APPROVALS);
            authorization.addPermission(PermissionType.RECEIVE_TRANSACTION_NOTIFICATIONS);
            authorization.addPermission(PermissionType.SEARCH_TRANSACTIONS);

            try
            {

                ApplicationCredentials credentials = PagSeguroConfiguration.ApplicationCredentials(isSandbox);

                String result = authorization.Register(credentials);

                Console.WriteLine("URL da autorização: " + result);
                Console.ReadKey();
            }
            catch (WebException exception)
            {
                Console.WriteLine(exception.Message + "\n");
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
