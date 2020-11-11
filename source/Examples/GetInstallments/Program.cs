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
using System.Net;
using System.Xml;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Installment;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace GetInstallments
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            Decimal amount = 1000.00m;
            String creditCardBrand = "visa";
            Int32 maxInstallmentNoInterest = 5;

            try
            {

                AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

                Installments result = InstallmentService.GetInstallments(credentials, amount, creditCardBrand, maxInstallmentNoInterest);

       
                foreach (Installment installment in result.Get())
                { 
                }
               
            }
            catch (PagSeguroServiceException exception)
            {
              

                foreach (ServiceError element in exception.Errors)
                {
                }

            }
        }
    }
}