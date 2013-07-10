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
    /// Class which deserialize request error xml
    /// </summary>
    internal static class ErrorSerializer
    {
        internal const string Error = "error";

        private const string Code = "code";
        private const string Message = "message";

        /// <summary>
        /// Converts xml into a ServiceError class
        /// </summary>
        /// <param name="reader"></param>
        internal static ServiceError Read(XmlReader reader)
        {

            string code = String.Empty;
            String message = String.Empty;

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return new ServiceError(code, message);
            }

            reader.ReadStartElement(ErrorSerializer.Error);
            reader.MoveToContent();

            while (!reader.EOF)
            {
                if (XMLParserUtils.IsEndElement(reader, ErrorSerializer.Error))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case ErrorSerializer.Code:
                            code = reader.ReadElementContentAsString();
                            break;
                        case ErrorSerializer.Message:
                            message = reader.ReadElementContentAsString();
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

            ServiceError error = new ServiceError(code, message);
            return error;
        }
    }
}
