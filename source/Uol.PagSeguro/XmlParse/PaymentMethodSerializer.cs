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

using System;
using System.Xml;
using Uol.PagSeguro.Domain;

namespace Uol.PagSeguro.XmlParse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class PaymentMethodSerializer
    {
        internal const string PaymentMethod = "paymentMethod";

        private const string PaymentMethodType = "type";
        private const string PaymentMethodCode = "code";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="paymentMethod"></param>
        internal static void Read(XmlReader reader, PaymentMethod paymentMethod)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(PaymentMethodSerializer.PaymentMethod);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, PaymentMethodSerializer.PaymentMethod))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case PaymentMethodSerializer.PaymentMethodType:
                            paymentMethod.PaymentMethodType = reader.ReadElementContentAsInt();
                            break;
                        case PaymentMethodSerializer.PaymentMethodCode:
                            paymentMethod.PaymentMethodCode = reader.ReadElementContentAsInt();
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
