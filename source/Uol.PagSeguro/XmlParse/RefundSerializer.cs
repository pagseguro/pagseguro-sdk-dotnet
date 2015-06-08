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

namespace Uol.PagSeguro.XmlParse
{


    /// <summary>
    /// 
    /// </summary>
    public static class RefundSerializer
    {
        /// <summary>
        /// Read a payment refund request result
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="result"></param>
        public static void Read(XmlReader reader, RequestResponse result)
        {

            if (reader.IsEmptyElement)
            {
                XMLParserUtils.SkipNode(reader);
                return;
            }

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Text:
                        result.Response = reader.Value;
                        break;
                }
            }    
        }
    }
}
