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
using Uol.PagSeguro;
using Uol.PagSeguro.Serialization;
using System.Xml;

namespace Uol.PagSeguro.Test
{
    [TestFixture]
    public class AccountCredentialsTest
    {
        public const string Email = "your_email";
        public const string Token = "your_token";

        [Test]
        public void PropertyTests()
        {
            AccountCredentials credentials = new AccountCredentials(Email, Token);
            Assert.AreEqual(credentials.Email, Email);
            Assert.AreEqual(credentials.Token, Token);
        }

        [Test]
        public void ConvertToQueryString()
        {
            AccountCredentials credentials = new AccountCredentials(Email, Token);
            string queryString = ServiceHelper.EncodeCredentialsAsQueryString(credentials);
            string[] parameters = queryString.Split('&');
            string[] expected = new string[2];
            expected[0] = "email=" + Email;
            expected[1] = "token=" + Token;

            foreach (string value in parameters)
            {
                bool found = false;
                foreach (string expectedValue in expected)
                {
                    if (expectedValue == value)
                    {
                        found = true;
                        break;
                    }
                }
                Assert.True(found, String.Format("Unexpected parameter in query string '{0}'", value));
            }
        }
    }
}
