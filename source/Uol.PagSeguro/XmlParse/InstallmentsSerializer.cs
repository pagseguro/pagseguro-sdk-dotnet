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
using System;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Installment;
using System.IO;
using System.Collections.Generic;

namespace Uol.PagSeguro.XmlParse
{


    /// <summary>
    /// 
    /// </summary>
    public static class InstallmentsSerializer
    {

        /// <summary>
        /// Read a direct payment session request result
        /// </summary>
        /// <param name="streamReader"></param>
        /// <param name="installments"></param>
        internal static void Read(XmlReader reader, Installments installments)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(SerializerHelper.Installments);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, SerializerHelper.Installments))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case SerializerHelper.Installment:
                            Installment installment = new Installment();
                            InstallmentSerializer.Read(reader, installment);
                            installments.Add(installment);
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
