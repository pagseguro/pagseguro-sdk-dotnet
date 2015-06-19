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
    internal static class PreApprovalSearchResultSerializer
    {

        internal const string PreApprovalSearchResult = "preApprovalSearchResult";
        private const string Date = "date";
        private const string CurrentPage = "currentPage";
        private const string TotalPages = "totalPages";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="result"></param>
        internal static void Read(XmlReader reader, PreApprovalSearchResult result)
        {
            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            reader.ReadStartElement(PreApprovalSearchResultSerializer.PreApprovalSearchResult);
 
            reader.MoveToContent();

            while (!reader.EOF)
            {

                if (XMLParserUtils.IsEndElement(reader, PreApprovalSearchResultSerializer.PreApprovalSearchResult))
                {
                    XMLParserUtils.SkipNode(reader);
                    break;
                }
                

                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case PreApprovalSearchResultSerializer.Date:
                            result.Date = reader.ReadElementContentAsDateTime();
                            break;
                        case PreApprovalSearchResultSerializer.CurrentPage:
                            result.CurrentPage = reader.ReadElementContentAsInt();
                            break;
                        case PreApprovalSearchResultSerializer.TotalPages:
                            result.TotalPages = reader.ReadElementContentAsInt();
                            break;
                        case PreApprovalSummaryListSerializer.PreApprovals:
                            PreApprovalSummaryListSerializer.Read(reader, result.PreApprovals);
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
