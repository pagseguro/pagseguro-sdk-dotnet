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
    /// Class which deserialize request address xml
    /// </summary>
    internal static class AddressSerializer
    {
        internal const string Address = "address";

        private const string Country = "country";
        private const string State = "state";
        private const string City = "city";
        private const string District = "district";
        private const string PostalCode = "postalCode";
        private const string Street = "street";
        private const string Number = "number";
        private const string Complement = "complement";

        /// <summary>
        /// Converts xml into a Address class
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="address"></param>
        internal static void Read(XmlReader reader, Address address)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(AddressSerializer.Address);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, AddressSerializer.Address))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case AddressSerializer.Country:
                            address.Country = reader.ReadElementContentAsString();
                            break;
                        case AddressSerializer.State:
                            address.State = reader.ReadElementContentAsString();
                            break;
                        case AddressSerializer.City:
                            address.City = reader.ReadElementContentAsString();
                            break;
                        case AddressSerializer.District:
                            address.District = reader.ReadElementContentAsString();
                            break;
                        case AddressSerializer.PostalCode:
                            address.PostalCode = reader.ReadElementContentAsString();
                            break;
                        case AddressSerializer.Street:
                            address.Street = reader.ReadElementContentAsString();
                            break;
                        case AddressSerializer.Number:
                            address.Number = reader.ReadElementContentAsString();
                            break;
                        case AddressSerializer.Complement:
                            address.Complement = reader.ReadElementContentAsString();
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
