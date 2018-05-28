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
//   limitations under the License.

using System;
using System.Xml;
using System.Text.RegularExpressions;

namespace Uol.PagSeguro.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnvironmentConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public const string PagseguroUrl = "pagseguro.uol";

        /// <summary>
        /// 
        /// </summary>
        public const string SandboxUrl = "sandbox.pagseguro.uol";

        /// <summary>
        /// 
        /// </summary>
        public static void ChangeEnvironment(bool sandbox)
        {
            var urlXmlConfiguration = PagSeguroConfiguration.UrlXmlConfiguration;

            var xml = new XmlDocument();
            xml.Load(urlXmlConfiguration);
            var elemList = xml.GetElementsByTagName("Link");
            var changed = false;

            try
            {
                if (sandbox)
                {
                    for (var i = 0; i < elemList.Count; i++)
                    {

                        var match = Regex.Match(elemList[i].InnerText, SandboxUrl);
                        if (match.Success)
                            continue;

                        elemList[i].InnerXml = elemList[i].InnerXml.Replace(PagseguroUrl, SandboxUrl);
                        changed = true;
                    }
                }
                else
                {
                    for (var i = 0; i < elemList.Count; i++)
                    {
                        var match = Regex.Match(elemList[i].InnerText, SandboxUrl);
                        if (!match.Success)
                            continue;

                        elemList[i].InnerXml = elemList[i].InnerXml.Replace(SandboxUrl, PagseguroUrl);
                        changed = true;
                    }
                }

                if (changed)
                    xml.Save(urlXmlConfiguration);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message + "\n");
                Console.ReadKey();
            }
        }
    }
}