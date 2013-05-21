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
    public class TransactionSearchResultSerializerTest
    {
        [Test]
        public void ReadTransactionSearchResult()
        {
            TransactionSearchResult result = new TransactionSearchResult();
            using (XmlReader reader = XmlReader.Create(@"..\..\TransactionSearchResult.xml"))
            {
                reader.MoveToContent();
                TransactionSearchResultSerializer.Read(reader, result); 
            }
            Assert.AreEqual(1, result.CurrentPage);
            Assert.AreEqual(1, result.TotalPages);

            DateTime expectedDate = new DateTime(2011, 2, 16, 22, 14, 35, DateTimeKind.Utc);
            Assert.AreEqual(expectedDate, result.Date.ToUniversalTime());

            // Transactions
            Assert.AreEqual(2, result.Transactions.Count);

            TransactionSummary transaction = result.Transactions[0];

            Assert.AreEqual("9E884542-81B3-4419-9A75-BCC6FB495EF1", transaction.Code);
            Assert.AreEqual("REF1234", transaction.Reference);

            expectedDate = new DateTime(2011, 2, 05, 17, 46, 12, DateTimeKind.Utc);
            Assert.AreEqual(expectedDate, transaction.Date.ToUniversalTime());
            expectedDate = new DateTime(2011, 2, 15, 20, 39, 14, DateTimeKind.Utc);
            Assert.AreEqual(expectedDate, transaction.LastEventDate.ToUniversalTime());

            Assert.AreEqual(3, transaction.TransactionStatus);
            Assert.AreEqual(1, transaction.TransactionType);
            Assert.AreEqual(1, transaction.PaymentMethod.PaymentMethodType);

            Assert.AreEqual(49900.00, transaction.GrossAmount);
            Assert.AreEqual(0.00, transaction.DiscountAmount);
            Assert.AreEqual(0.00, transaction.FeeAmount);
            Assert.AreEqual(49900.00, transaction.NetAmount);
            Assert.AreEqual(0.00, transaction.ExtraAmount);

            transaction = result.Transactions[1];
            Assert.AreEqual("2FB07A22-68FF-4F83-A356-24153A0C05E1", transaction.Code);
            Assert.AreEqual("REF5678", transaction.Reference);

            expectedDate = new DateTime(2011, 2, 07, 20, 57, 52, DateTimeKind.Utc);
            Assert.AreEqual(expectedDate, transaction.Date.ToUniversalTime());
            expectedDate = new DateTime(2011, 2, 15, 0, 37, 24, DateTimeKind.Utc);
            Assert.AreEqual(expectedDate, transaction.LastEventDate.ToUniversalTime());

            Assert.AreEqual(4, transaction.TransactionStatus);
            Assert.AreEqual(3, transaction.TransactionType);
            Assert.AreEqual(3, transaction.PaymentMethod.PaymentMethodType);

            Assert.AreEqual(26900.00, transaction.GrossAmount);
            Assert.AreEqual(0.00, transaction.DiscountAmount);
            Assert.AreEqual(0.00, transaction.FeeAmount);
            Assert.AreEqual(26900.00, transaction.NetAmount);
            Assert.AreEqual(0.00, transaction.ExtraAmount);
        }
    }
}
