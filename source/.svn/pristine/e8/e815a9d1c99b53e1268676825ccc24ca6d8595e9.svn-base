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
    internal static class SenderSerializer
    {
        internal const string Sender = "sender";

        private const string Name = "name";
        private const string Email = "email";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="sender"></param>
        internal static void Read(XmlReader reader, Sender sender)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(SenderSerializer.Sender);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, SenderSerializer.Sender))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case SenderSerializer.Name:
                            sender.Name = reader.ReadElementContentAsString();
                            break;
                        case SenderSerializer.Email:
                            sender.Email = reader.ReadElementContentAsString();
                            break;
                        case PhoneSerializer.Phone:
                            Phone phone = new Phone();
                            PhoneSerializer.Read(reader, phone);
                            sender.Phone = phone;
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
