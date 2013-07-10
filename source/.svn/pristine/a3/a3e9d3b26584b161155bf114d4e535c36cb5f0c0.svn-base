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
    internal static class ItemSerializer
    {
        internal const string Item = "item";

        private const string Id = "id";
        private const string Description = "description";
        private const string Quantity = "quantity";
        private const string Amount = "amount";
        private const string Weight = "weight";
        private const string ShippingCost = "shippingCost";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="item"></param>
        internal static void Read(XmlReader reader, Item item)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(ItemSerializer.Item);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, ItemSerializer.Item))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case ItemSerializer.Id:
                            item.Id = reader.ReadElementContentAsString();
                            break;
                        case ItemSerializer.Description:
                            item.Description = reader.ReadElementContentAsString();
                            break;
                        case ItemSerializer.Quantity:
                            item.Quantity = reader.ReadElementContentAsInt();
                            break;
                        case ItemSerializer.Amount:
                            item.Amount = reader.ReadElementContentAsDecimal();
                            break;
                        case ItemSerializer.Weight:
                            item.Weight = reader.ReadElementContentAsLong();
                            break;
                        case ItemSerializer.ShippingCost:
                            item.ShippingCost = reader.ReadElementContentAsDecimal();
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
