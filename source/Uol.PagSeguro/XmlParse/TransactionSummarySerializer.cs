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
//   limitation

using System.Xml;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;

namespace Uol.PagSeguro.XmlParse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TransactionSummarySerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="transaction"></param>
        internal static void Read(XmlReader reader, TransactionSummary transaction, bool preApproval)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            if (preApproval == true)
                reader.ReadStartElement(TransactionSerializerHelper.PreApproval);
            else
                reader.ReadStartElement(TransactionSerializerHelper.Transaction);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (preApproval == true)
                {
                    if (XMLParserUtils.IsEndElement(reader, TransactionSerializerHelper.PreApproval))
                    {
                        XMLParserUtils.SkipNode(reader);
                        break;
                    }
                }
                else
                {
                    if (XMLParserUtils.IsEndElement(reader, TransactionSerializerHelper.Transaction))
                    {
                        XMLParserUtils.SkipNode(reader);
                        break;
                    }
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case TransactionSerializerHelper.Code:
                            transaction.Code = reader.ReadElementContentAsString();
                            break;
                        case TransactionSerializerHelper.Date:
                            transaction.Date = reader.ReadElementContentAsDateTime();
                            break;
                        case TransactionSerializerHelper.Reference:
                            transaction.Reference = reader.ReadElementContentAsString();
                            break;
                        case TransactionSerializerHelper.TransactionType:
                            transaction.TransactionType = reader.ReadElementContentAsInt();
                            break;
                        case TransactionSerializerHelper.TransactionStatus:
                            if (preApproval == true)
                                transaction.Status = reader.ReadElementContentAsString();
                            else
                                transaction.TransactionStatus = reader.ReadElementContentAsInt();
                            break;
                        case TransactionSerializerHelper.GrossAmount:
                            transaction.GrossAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case TransactionSerializerHelper.DiscountAmount:
                            transaction.DiscountAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case TransactionSerializerHelper.FeeAmount:
                            transaction.FeeAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case TransactionSerializerHelper.NetAmount:
                            transaction.NetAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case TransactionSerializerHelper.ExtraAmount:
                            transaction.ExtraAmount = reader.ReadElementContentAsDecimal();
                            break;
                        case TransactionSerializerHelper.LastEventDate:
                            transaction.LastEventDate = reader.ReadElementContentAsDateTime();
                            break;
                        case TransactionSerializerHelper.Name:
                            transaction.Name = reader.ReadElementContentAsString();
                            break;
                        case TransactionSerializerHelper.Tracker:
                            transaction.Tracker = reader.ReadElementContentAsString();
                            break;
                        case TransactionSerializerHelper.Charge:
                            transaction.Charge = reader.ReadElementContentAsString();
                            break;
                        case PaymentMethodSerializer.PaymentMethod:
                            PaymentMethod paymentMethod = new PaymentMethod();
                            PaymentMethodSerializer.Read(reader, paymentMethod);
                            transaction.PaymentMethod = paymentMethod;
                            break;
                        default:
                            XMLParserUtils.SkipElement(reader);
                            break;
                    }
                }
                else
                {
                    XMLParserUtils.SkipNode(reader);
                }
            }
        }
    }
}
