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
    internal static class ShippingSerializer
    {
        internal const string Shipping = "shipping";

        private const string ShippingType = "type";
        private const string Cost = "cost";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="shipping"></param>
        internal static void Read(XmlReader reader, Shipping shipping)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(ShippingSerializer.Shipping);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, ShippingSerializer.Shipping))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case ShippingSerializer.ShippingType:
                            shipping.ShippingType = reader.ReadElementContentAsInt();
                            break;
                        case ShippingSerializer.Cost:
                            shipping.Cost = reader.ReadElementContentAsDecimal();
                            break;
                        case AddressSerializer.Address:
                            Address address = new Address();
                            AddressSerializer.Read(reader, address);
                            shipping.Address = address;
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
