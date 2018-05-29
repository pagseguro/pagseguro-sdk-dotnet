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
using Uol.PagSeguro.Configuration;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public static class PagSeguroConfiguration
    {
        //PagSeguro .NET Library Tests

        /// <summary>
        /// 
        /// </summary>
        public static string XmlConfigFile { get; set; } = $"{GetBasePath()}/PagSeguroConfig.xml";

        /// <summary>
        /// 
        /// </summary>
        public static XmlDocument XmlConfiguration => PagSeguroConfigSerializer.GetXmlConfig(XmlConfigFile);

        /// <summary>
        /// 
        /// </summary>
        public static string ModuleVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string CmsVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string LanguageEngineDescription => Environment.Version.ToString();

        /// <summary>
        /// 
        /// </summary>
        public static AccountCredentials GetAccountCredentials(bool isSandbox, string email = null, string token = null)
        {
            var appConfig = GetAppConfig(isSandbox, email, token);
            if (appConfig == null)
                return PagSeguroConfigSerializer.GetAccountCredentials(XmlConfiguration, isSandbox);

            return new AccountCredentials(isSandbox,
                appConfig.GetCredentialEmail(isSandbox),
                appConfig.GetCredentialToken(isSandbox));
        }

        /// <summary>
        /// 
        /// </summary>
        public static ApplicationCredentials GetApplicationCredentials(bool isSandbox, string appId = null, string appKey = null)
        {
            var appConfig = GetAppConfig(isSandbox, null, null, appId, appKey);
            if (appConfig == null)
                return PagSeguroConfigSerializer.GetApplicationCredentials(XmlConfiguration, isSandbox);

            return new ApplicationCredentials(isSandbox,
                appConfig.GetCredentialAppId(isSandbox),
                appConfig.GetCredentialAppKey(isSandbox));
        }

        /// <summary>
        /// 
        /// </summary>
        public static int RequestTimeout => Convert.ToInt32(GetDataValue(PagSeguroConfigSerializer.RequestTimeout));

        /// <summary>
        /// 
        /// </summary>
        public static string FormUrlEncoded => GetDataValue(PagSeguroConfigSerializer.FormUrlEncoded);

        /// <summary>
        /// 
        /// </summary>
        public static string Encoding => GetDataValue(PagSeguroConfigSerializer.Encoding);

        /// <summary>
        /// 
        /// </summary>
        public static string LibVersion => GetDataValue(PagSeguroConfigSerializer.LibVersion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        private static string GetDataValue(string dataKey)
        {
            var appConfig = PagSeguroConfigurationSection.GetCurrent();
            var dataFromAppConfig = appConfig?.Configuration.Get(dataKey);

            return dataFromAppConfig ?? PagSeguroConfigSerializer.GetDataConfiguration(XmlConfiguration, dataKey);
        }

        private static string GetBasePath()
        {
            return System.Web.HttpContext.Current != null
                ? System.Web.HttpRuntime.AppDomainAppPath
                : AppDomain.CurrentDomain.BaseDirectory;
        }

        private static PagSeguroConfigurationSection GetAppConfig(bool isSandbox, string email = null,
            string token = null, string appId = null, string appKey = null)
        {
            return PagSeguroConfigurationSection.GetCurrent(isSandbox, email, token, appId, appKey);
        }
    }
}
