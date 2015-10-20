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

namespace Uol.PagSeguro.Resources
{
    /// <summary>
    /// 
    /// </summary>
    public static class PagSeguroConfiguration
    {
        //PagSeguro .NET Library Tests
        private static string urlXmlConfiguration = ".../.../Configuration/PagSeguroConfig.xml";

        //Website
        //private static string urlXmlConfiguration = HttpRuntime.AppDomainAppPath + "PagSeguroConfig.xml";

        private static string _moduleVersion;
        private static string _cmsVersion;

        /// <summary>
        /// 
        /// </summary>
        public static AccountCredentials Credentials(bool sandbox)
        {
            return PagSeguroConfigSerializer.GetAccountCredentials(LoadXmlConfig(), sandbox);
        }

        /// <summary>
        /// 
        /// </summary>
        public static ApplicationCredentials ApplicationCredentials(bool sandbox)
        {
            return PagSeguroConfigSerializer.GetApplicationCredentials(LoadXmlConfig(), sandbox);
        }

        /// <summary>
        /// 
        /// </summary>
        public static string UrlXmlConfiguration
        {
            get
            {
                return urlXmlConfiguration;
            }
            set
            {
                urlXmlConfiguration = value;
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
        public static Uri SearchAbandonedUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.SearchAbandoned));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri CancelUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Cancel));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri RefundUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Refund));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApproval));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalRedirectUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalRedirect));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalNotificationUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalNotification));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalSearchUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalSearch));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalCancelUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalCancel));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri PreApprovalPaymentUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalPayment));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri SessionUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Session));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri TransactionsUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Transactions));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri InstallmentUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Installment));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri AuthorizarionRequestUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationRequest));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri AuthorizarionUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.Authorization));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri AuthorizarionSearchUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationSearch));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Uri AuthorizationNotificationUri
        {
            get
            {
                return new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationNotification));
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
            return PagSeguroConfigSerializer.GetWebserviceUrl(LoadXmlConfig(), url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string GetDataConfiguration(string data)
        {
            return PagSeguroConfigSerializer.GetDataConfiguration(LoadXmlConfig(), data);
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
                xml.Load(urlXmlConfiguration);
            }
            return xml;
        }
    }
}
