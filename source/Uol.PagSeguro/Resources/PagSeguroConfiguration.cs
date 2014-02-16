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
using System.IO;

namespace Uol.PagSeguro.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public static class PagSeguroConfiguration
    {
        //Capturando o caminho do arquivo
        private static string caminho = Path.GetDirectoryName(Assembly.GetAssembly(typeof(PagSeguroConfiguration)).CodeBase);
        private const string urlXmlConfiguration = "../Configuration/PagSeguroConfig.xml";
        private static string _moduleVersion;
        private static string _cmsVersion;

        /// <summary>
        /// 
        /// </summary>
        public static AccountCredentials Credentials
        {
            get
            {
                if (XmlConfigFileExist())
                    return PagSeguroConfigSerializer.GetAccountCredentials(LoadXmlConfig());
                else
                    return new AccountCredentials("your@email.com", "your_token_here");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ModuleVersion
        {
            get
            {
                return _moduleVersion;
            }

            set
            {
                _moduleVersion = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string CmsVersion
        {
            get
            {
                return _cmsVersion;
            }

            set
            {
                _cmsVersion = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string LanguageEngineDescription
        {
            get
            {
                return Environment.Version.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri NotificationUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Notification));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PaymentUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Payment));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PaymentRedirectUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.PaymentRedirect));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri SearchUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Search));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int RequestTimeout
        {
            get 
            {
                return Convert.ToInt32(GetDataConfiguration(PagSeguroConfigSerializer.RequestTimeout));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string FormUrlEncoded
        {
            get
            {
                return GetDataConfiguration(PagSeguroConfigSerializer.FormUrlEncoded);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Encoding
        {
            get
            {
                return GetDataConfiguration(PagSeguroConfigSerializer.Encoding);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string LibVersion
        {
            get
            {
                return GetDataConfiguration(PagSeguroConfigSerializer.LibVersion);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetUrlValue(string url)
        {
            if (XmlConfigFileExist())
                return PagSeguroConfigSerializer.GetWebserviceUrl(LoadXmlConfig(), url);
            else
            {
                switch(url)
                {
                    case PagSeguroConfigSerializer.Notification:
                        return "https://ws.pagseguro.uol.com.br/v2/transactions/notifications";
                    case PagSeguroConfigSerializer.Payment:
                        return "https://ws.pagseguro.uol.com.br/v2/checkout";
                    case PagSeguroConfigSerializer.PaymentRedirect:
                        return "https://pagseguro.uol.com.br/v2/checkout/payment.html";
                    case PagSeguroConfigSerializer.Search:
                        return "https://ws.pagseguro.uol.com.br/v2/transactions";
                    default:
                        throw new ApplicationException("Não foi possível encontrar a configuração para " + url);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string GetDataConfiguration(string data)
        {
            if (XmlConfigFileExist())
                return PagSeguroConfigSerializer.GetDataConfiguration(LoadXmlConfig(), data);
            else
            {
                switch (data)
                {
                    case PagSeguroConfigSerializer.LibVersion:
                        return "2.0.4";
                    case PagSeguroConfigSerializer.FormUrlEncoded:
                        return "application/x-www-form-urlencoded";
                    case PagSeguroConfigSerializer.Encoding:
                        return "ISO-8859-1";
                    case PagSeguroConfigSerializer.RequestTimeout:
                        return "10000";
                    default:
                        throw new ApplicationException("Não foi possível encontrar a configuração para " + data);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static XmlDocument LoadXmlConfig()
        {
            XmlDocument xml = new XmlDocument();
            using (xml as IDisposable)
            {
                xml.Load(Path.Combine(caminho, urlXmlConfiguration));
            }
            return xml;
        }

        /// <summary>
        /// Identifica se há um arquivo de configuração
        /// </summary>
        /// <returns>True se o arquivo xml de configuração existe, caso contrário False.</returns>
        private static bool XmlConfigFileExist()
        {
            return File.Exists(Path.Combine(caminho, urlXmlConfiguration));
        }
    }
}
