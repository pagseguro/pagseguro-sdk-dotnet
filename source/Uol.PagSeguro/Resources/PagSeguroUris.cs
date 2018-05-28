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
    public static class PagSeguroUris
    {
        /// <summary>
        /// 
        /// </summary>
        public static XmlDocument XmlConfig => PagSeguroConfiguration.XmlConfiguration;

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetNotificationUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Notification, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetPaymentUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Payment, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetPaymentRedirectUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.PaymentRedirect, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetSearchUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Search, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetSearchAbandonedUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.SearchAbandoned, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetCancelUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Cancel, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetRefundUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Refund, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetPreApprovalUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApproval, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetPreApprovalRedirectUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalRedirect, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static Uri GetPreApprovalNotificationUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalNotification, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetPreApprovalCancelUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalCancel, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetPreApprovalSearchUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalSearch, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetPreApprovalPaymentUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.PreApprovalPayment, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetSessionUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Session, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetTransactionsUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Transactions, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetInstallmentUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Installment, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetAuthorizarionRequestUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationRequest, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetAuthorizarionUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.Authorization, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetAuthorizarionSearchUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationSearch, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        public static Uri GetAuthorizationNotificationUri(Credentials credentials) =>
            new Uri(GetUrlValue(PagSeguroConfigSerializer.AuthorizationNotification, credentials.IsSandbox));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        private static string GetUrlValue(string url, bool sandbox)
        {
            var appConfig = PagSeguroConfigurationSection.GetCurrent(sandbox);
            var urlFromAppConfig = appConfig?.Urls.Get(url, sandbox);

            return urlFromAppConfig ?? PagSeguroConfigSerializer.GetWebserviceUrl(XmlConfig, url);
        }
    }
}
