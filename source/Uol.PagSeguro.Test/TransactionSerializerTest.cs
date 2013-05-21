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
    public class TransactionSerializerTest
    {
        [Test]
        public void ReadTransaction()
        {
            Transaction transaction = new Transaction();
            using (XmlReader reader = XmlReader.Create(@"..\..\Transaction.xml"))
            {
                reader.MoveToContent();               
                TransactionSerializer.Read(reader, transaction); 
            }
            Assert.AreEqual("9E884542-81B3-4419-9A75-BCC6FB495EF1", transaction.Code);
            Assert.AreEqual("REF1234", transaction.Reference);

            DateTime expectedDate = new DateTime(2011, 2, 10, 19, 13, 41, DateTimeKind.Utc);
            Assert.True(expectedDate.Equals(transaction.Date.ToUniversalTime()));

            Assert.AreEqual(3, transaction.TransactionStatus);
            Assert.AreEqual(1, transaction.TransactionType);
            Assert.AreEqual(101, transaction.PaymentMethod.PaymentMethodCode);
            Assert.AreEqual(1, transaction.PaymentMethod.PaymentMethodType);

            Assert.AreEqual(49900.00, transaction.GrossAmount);
            Assert.AreEqual(0.01, transaction.DiscountAmount);
            Assert.AreEqual(0.02, transaction.FeeAmount);
            Assert.AreEqual(49900.99, transaction.NetAmount);
            Assert.AreEqual(0.03, transaction.ExtraAmount);

            Assert.AreEqual(1, transaction.InstallmentCount);

            // Items
            Assert.AreEqual(2, transaction.Items.Count);

            Item item = transaction.Items[0];
            Assert.AreEqual("0001", item.Id);
            Assert.AreEqual("Notebook Prata", item.Description);
            Assert.AreEqual(1, item.Quantity);
            Assert.AreEqual(2430.00, item.Amount);

            item = transaction.Items[1];
            Assert.AreEqual("0002", item.Id);
            Assert.AreEqual("Notebook Rosa", item.Description);
            Assert.AreEqual(1, item.Quantity);
            Assert.AreEqual(2560.00, item.Amount);

            // Sender
            Assert.AreEqual("José Comprador", transaction.Sender.Name);
            Assert.AreEqual("comprador@uol.com.br", transaction.Sender.Email);
            Assert.AreEqual("11", transaction.Sender.Phone.AreaCode);
            Assert.AreEqual("56273440", transaction.Sender.Phone.Number);

            // Shipping
            Assert.AreEqual("Av. Brig. Faria Lima", transaction.Shipping.Address.Street);
            Assert.AreEqual("1384", transaction.Shipping.Address.Number);
            Assert.AreEqual("Notebook Rosa", transaction.Shipping.Address.Complement);
            Assert.AreEqual("Jardim Paulistano", transaction.Shipping.Address.District);
            Assert.AreEqual("01452002", transaction.Shipping.Address.PostalCode);
            Assert.AreEqual("São Paulo", transaction.Shipping.Address.City);
            Assert.AreEqual("SP", transaction.Shipping.Address.State);
            Assert.AreEqual("BRA", transaction.Shipping.Address.Country);

            Assert.AreEqual(1, transaction.Shipping.ShippingType.Value);
            Assert.AreEqual(21.50, transaction.Shipping.Cost);
        }

        [Test]
        public void ReadTransactionSummary()
        {
            TransactionSummary transaction = new TransactionSummary();
            using (XmlReader reader = XmlReader.Create(@"..\..\Transaction.xml"))
            {
                reader.MoveToContent();
                TransactionSummarySerializer.Read(reader, transaction);
            }
            Assert.AreEqual("9E884542-81B3-4419-9A75-BCC6FB495EF1", transaction.Code);
            Assert.AreEqual("REF1234", transaction.Reference);

            DateTime expectedDate = new DateTime(2011, 2, 10, 19, 13, 41, DateTimeKind.Utc);
            Assert.True(expectedDate.Equals(transaction.Date.ToUniversalTime()));

            Assert.AreEqual(3, transaction.TransactionStatus);
            Assert.AreEqual(1, transaction.TransactionType);
            Assert.AreEqual(101, transaction.PaymentMethod.PaymentMethodCode);
            Assert.AreEqual(1, transaction.PaymentMethod.PaymentMethodType);

            Assert.AreEqual(49900.00, transaction.GrossAmount);
            Assert.AreEqual(0.01, transaction.DiscountAmount);
            Assert.AreEqual(0.02, transaction.FeeAmount);
            Assert.AreEqual(49900.99, transaction.NetAmount);
            Assert.AreEqual(0.03, transaction.ExtraAmount);
        }

        [Test]
        public void ReadMissingFieldsTransaction()
        {
            Transaction transaction = new Transaction();
            using (XmlReader reader = XmlReader.Create(@"..\..\MissingFieldsTransaction.xml"))
            {
                reader.MoveToContent();
                TransactionSerializer.Read(reader, transaction);
            }

            Assert.AreEqual(0, transaction.GrossAmount);
            Assert.AreEqual(0, transaction.DiscountAmount);
            Assert.AreEqual(0, transaction.FeeAmount);
            Assert.AreEqual(0, transaction.NetAmount);
            Assert.AreEqual(0, transaction.ExtraAmount);
            Assert.AreEqual(0, transaction.InstallmentCount);

            // Items
            Assert.AreEqual(0, transaction.Items.Count);

            // Sender
            Assert.AreEqual("José Comprador", transaction.Sender.Name);
            Assert.AreEqual("comprador@uol.com.br", transaction.Sender.Email);
            Assert.AreEqual(null, transaction.Sender.Phone);

            // Shipping
            Assert.AreEqual(null, transaction.Shipping.Address.Country);

            Assert.AreEqual(1, transaction.Shipping.ShippingType);
            Assert.AreEqual(21.50, transaction.Shipping.Cost);
        }
    }
}
