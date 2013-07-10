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

namespace Uol.PagSeguro.XmlParse
{
    /// <summary>
    /// 
    /// </summary>
    internal static class XMLParserUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        internal static void SkipNode(XmlReader reader)
        {
            reader.Read();
            reader.MoveToContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static bool IsEndElement(XmlReader reader, string name)
        {
            return
                reader.NodeType == XmlNodeType.EndElement &&
                reader.Name.Equals(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        internal static void SkipElement(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                reader.Read();
            }
            else
            {
                reader.Skip();
            }
        }
    }
}
