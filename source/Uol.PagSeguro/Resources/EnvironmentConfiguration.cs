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
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.XmlParse;
using System.Reflection;
using System.Diagnostics;
using System.Web;
using System.Text.RegularExpressions;
using Uol.PagSeguro.Exception;

namespace Uol.PagSeguro.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnvironmentConfiguration
    {
        private const string pagseguroUrl = "pagseguro.uol";
        private const string sandboxUrl = "sandbox.pagseguro.uol";

        /// <summary>
        /// 
        /// </summary>
        public static void ChangeEnvironment(bool sandbox)
        {

            string urlXmlConfiguration = PagSeguroConfiguration.UrlXmlConfiguration;

            XmlDocument xml = new XmlDocument();
            xml.Load(urlXmlConfiguration);
            XmlNodeList elemList = xml.GetElementsByTagName("Link");
            bool changed = false;

            try
            {
                if (sandbox)
                {
                    for (int i = 0; i < elemList.Count; i++)
                    {

                        Match match = Regex.Match(elemList[i].InnerText, sandboxUrl);

                        if (!match.Success)
                        {
                            elemList[i].InnerXml = elemList[i].InnerXml.Replace(pagseguroUrl, sandboxUrl);
                            changed = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        Match match = Regex.Match(elemList[i].InnerText, sandboxUrl);

                        if (match.Success)
                        {
                            elemList[i].InnerXml = elemList[i].InnerXml.Replace(sandboxUrl, pagseguroUrl);
                            changed = true;
                        }
                    }
                }

                if (changed)
                {
                    xml.Save(urlXmlConfiguration);
                }
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message + "\n");
                Console.ReadKey();
            }
        }
    }
}