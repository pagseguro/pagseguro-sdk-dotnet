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

        //Website
        //private static string urlXmlConfiguration = HttpRuntime.AppDomainAppPath + "PagSeguroConfig.xml";

        private static string _moduleVersion;
        private static string _cmsVersion;

        private static IPagSeguroConfiguration currentConfig;

        /// <summary>
        /// 
        /// </summary>
        public static AccountCredentials Credentials(bool sandbox)
        {
            return PagSeguroConfigSerializer.GetAccountCredentials(CurrentConfig, sandbox);
        }

        /// <summary>
        /// Allow's the user to change configuration
        /// </summary>
        /// <param name="newLocation"></param>
        public static void SetConfiguration(IPagSeguroConfiguration config)
        {
            currentConfig = config;
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
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri NotificationUri
        {
            get
            {
                return CurrentConfig.NotificationUrl;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri PaymentUri
        {
            get
            {
                return CurrentConfig.PaymentUrl;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri PaymentRedirectUri
        {
            get
            {
                return CurrentConfig.PaymentRedirectUrl;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri SearchUri
        {
            get
            {
                return CurrentConfig.SearchUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri PreApprovalUri
        {
            get
            {
                return CurrentConfig.PreApprovalUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri PreApprovalRedirectUri
        {
            get
            {
                return CurrentConfig.PreApprovalRedirectUrl;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri PreApprovalNotificationUri
        {
            get
            {
                return CurrentConfig.PreApprovalNotificationUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri PreApprovalSearchUri
        {
            get
            {
                return CurrentConfig.PreApprovalSearchUrl;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri PreApprovalCancelUri
        {
            get
            {
                return CurrentConfig.PreApprovalCancelUrl;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static Uri PreApprovalPaymentUri
        {
            get
            {
                return CurrentConfig.PreApprovalPaymentUrl;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static int RequestTimeout
        {
            get
            {
                return CurrentConfig.RequestTimeout;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static string FormUrlEncoded
        {
            get
            {
                return CurrentConfig.FormUrlEncoded;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static string Encoding
        {
            get
            {
                return CurrentConfig.Encoding;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("ShouldUse CurrentConfig.", true)]
        public static string LibVersion
        {
            get
            {
                return CurrentConfig.LibVersion;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IPagSeguroConfiguration CurrentConfig
        {
            get
            {
                return currentConfig ?? (currentConfig = new PagSeguroXmlConfiguration());
            }
        }
    }
}
