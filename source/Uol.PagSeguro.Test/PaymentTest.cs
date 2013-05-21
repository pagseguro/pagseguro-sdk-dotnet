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
    public class PaymentTest
    {
        private static Credentials credentials = new AccountCredentials("your@email.com", "your_token_here");

        [Test]
        public void MinPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            try
            {
                Uri redirectUri = PaymentService.Register(credentials, payment);
            }
            catch (PagSeguroServiceException e)
            {
                Assert.AreEqual(e.StatusCode, HttpStatusCode.BadRequest);
                Assert.AreEqual(e.Errors.Count, 1);
                PagSeguroServiceError error = e.Errors[0];
                Assert.AreEqual(error.Code, "11024"); // invalid items quantity
            }
        }

        [Test]
        public void NoItemsPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Reference = "REF1234";
            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.Sedex;
            payment.Shipping.Address = new Address("BRA", "SP", "Sao Paulo", "Jardim Paulistano", "01452002", "Av. Brig. Faria Lima", "1384", "5o andar");
            payment.Sender = new Sender("Joao Comprador", "comprador@uol.com.br", new Phone("11", "56273440"));

            try
            {
                Uri redirectUri = PaymentService.Register(credentials, payment);
            }
            catch (PagSeguroServiceException e)
            {
                Assert.AreEqual(e.StatusCode, HttpStatusCode.BadRequest);
                Assert.AreEqual(e.Errors.Count, 1);
                PagSeguroServiceError error = e.Errors[0];
                Assert.AreEqual(error.Code, "11024"); // invalid items quantity
            }
        }

        [Test]
        public void PaymentRedirectUri()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));

            // TODO: implementar este teste independente da posição do codigo na query string
            PaymentRequestResponse response = PaymentService.RegisterCore(credentials, payment);
            Assert.NotNull(response);
            Assert.Greater(response.Code.Length, 0);
            string uri = response.PaymentRedirectUri.ToString();
            Assert.GreaterOrEqual(uri.Length, response.Code.Length);
            Assert.AreEqual(response.Code, uri.Substring(uri.Length - response.Code.Length));
        }

        [Test]
        public void JustItemsPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

            Uri redirectUri = PaymentService.Register(credentials, payment);
            Assert.NotNull(redirectUri);
        }

        [Test]
        public void NoSenderPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Currency = Currency.Brl;

            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

            payment.Reference = "REF1234";
            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.Sedex;
            payment.Shipping.Address = new Address("BRA", "SP", "Sao Paulo", "Jardim Paulistano", "01452002", "Av. Brig. Faria Lima", "1384", "5o andar");

            Uri redirectUri = PaymentService.Register(credentials, payment);
            Assert.NotNull(redirectUri);
        }

        [Test]
        public void NoShippingPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Currency = Currency.Brl;

            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

            payment.Reference = "REF1234";
            payment.Sender = new Sender("Joao Comprador", "comprador@uol.com.br", new Phone("11", "56273440"));

            Uri redirectUri = PaymentService.Register(credentials, payment);
            Assert.NotNull(redirectUri);
        }

        [Test]
        public void EmptyShippingPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Currency = Currency.Brl;

            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

            payment.Reference = "REF1234";
            payment.Shipping = new Shipping();

            Uri redirectUri = PaymentService.Register(credentials, payment);
            Assert.NotNull(redirectUri);
        }

        [Test]
        public void NoAddressPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Currency = Currency.Brl;

            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

            payment.Reference = "REF1234";
            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.Sedex;
            payment.Sender = new Sender("Joao Comprador", "comprador@uol.com.br", new Phone("11", "56273440"));

            Uri redirectUri = PaymentService.Register(credentials, payment);
            Assert.NotNull(redirectUri);
        }

        [Test]
        public void EmptyAddressPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Currency = Currency.Brl;

            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

            payment.Reference = "REF1234";
            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.Sedex;
            payment.Shipping.Address = new Address();
            payment.Sender = new Sender("Joao Comprador", "comprador@uol.com.br", new Phone("11", "56273440"));

            Uri redirectUri = PaymentService.Register(credentials, payment);
            Assert.NotNull(redirectUri);
        }

        [Test]
        public void InvalidAddressPayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Currency = Currency.Brl;

            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

            payment.Reference = "REF1234";
            payment.Shipping = new Shipping();
            payment.Shipping.ShippingType = ShippingType.Sedex;
            payment.Shipping.Address = new Address("", "SP", "", "", "", "", "", "");
            payment.Sender = new Sender("Joao Comprador", "comprador@uol.com.br", new Phone("11", "56273440"));

            try
            {
                Uri redirectUri = PaymentService.Register(credentials, payment);
            }
            catch (PagSeguroServiceException e)
            {
                string[] expected = { "11017", "11018", "11019", "11022" };
                TestHelper.ValidateErrors(e.Errors, expected);
            }
        }

        [Test]
        public void NoShippingTypePayment()
        {
            PaymentRequest payment = new PaymentRequest();
            payment.Currency = Currency.Brl;

            payment.Items.Add(new Item("0001", "Notebook Prata", 1, 2430, 1000, 0));
            payment.Items.Add(new Item("0002", "Notebook Rosa", 2, 2560, 750, 0));

            payment.Reference = "REF1234";
            payment.Shipping = new Shipping();
            payment.Shipping.Address = new Address("BRA", "SP", "Sao Paulo", "Jardim Paulistano", "01452002", "Av. Brig. Faria Lima", "1384", "5o andar");
            payment.Sender = new Sender("Joao Comprador", "comprador@uol.com.br", new Phone("11", "56273440"));
            try
            {
                Uri redirectUri = PaymentService.Register(credentials, payment);
            }
            catch (PagSeguroServiceException e)
            {
                if (e.Errors.Count != 1)
                    throw;

                PagSeguroServiceError error = e.Errors[0];
                if (error.Code != "11015")
                    throw;
            }
        }

        [Test]
        public void CompletePayment()
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

            Uri redirectUri = PaymentService.Register(credentials, payment);
            Assert.NotNull(redirectUri);
        }
    }
}
