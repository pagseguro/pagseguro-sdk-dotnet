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
using NUnit.Framework;
using System.Net;

namespace Uol.PagSeguro.Test
{
    [TestFixture]
    public class TransactionSearchTest
    {
        private static Credentials credentials = new AccountCredentials("your@email.com", "your_token_here");

        [Test]
        public void InvalidDateRange()
        {
            try
            {
                TransactionSearchResult result = TransactionSearchService.SearchByDate(credentials, DateTime.Now.AddMonths(-12));
            }
            catch (PagSeguroServiceException e)
            {
                string[] expected = { "13006" };
                TestHelper.ValidateErrors(e.Errors, expected);
            }
        }

        [Test]
        public void Last5Months()
        {
            TransactionSearchResult result = TransactionSearchService.SearchByDate(credentials, DateTime.Now.AddMonths(-5));
        }
    }
}
