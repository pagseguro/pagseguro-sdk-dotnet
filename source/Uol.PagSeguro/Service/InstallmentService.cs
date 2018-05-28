using System;
using System.Globalization;
using System.Net;
using System.Web;
using System.Xml;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Installment;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Log;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class InstallmentService
    {
        /// <summary>
        /// Request a direct payment session
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="amount"></param>
        /// <param name="cardBrand"></param>
        /// <param name="maxInstallmentNoInterest"></param>
        /// <returns><c cref="T:Uol.PagSeguro.CancelRequestResponse">Result</c></returns>
        public static Installments GetInstallments(Credentials credentials, decimal amount, string cardBrand, int maxInstallmentNoInterest)
        {
            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "InstallmentService.GetInstallments() - begin"));
            try
            {
                using (var response = HttpUrlConnectionUtil.GetHttpGetConnection(
                    BuildInstallmentUrl(credentials, amount, cardBrand, maxInstallmentNoInterest)))
                {
                    using (var reader = XmlReader.Create(response.GetResponseStream()))
                    {

                        var result = new Installments();
                        InstallmentsSerializer.Read(reader, result);
                        PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "InstallmentService.Register({0}) - end", result.ToString()));
                        return result;
                    }
                }
            }
            catch (ArgumentException exception)
            {
                var pse = new PagSeguroServiceException(exception.Message);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "InstallmentService.Register() - error {0}", exception.Message));
                throw pse;
            }
            catch (WebException exception)
            {
                var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "InstallmentService.Register() - error {0}", pse));
                throw pse;
            }
        }

        private static string BuildInstallmentUrl(Credentials credentials, decimal amount, string cardBrand, int maxInstallmentNoInterest)
        {
            var builder = new QueryStringBuilder("{url}?{credentials}&amount={amount}&cardBrand={cardBrand}&maxInstallmentNoInterest={maxInstallmentNoInterest}");

            builder.ReplaceValue("{url}", PagSeguroConfiguration.InstallmentUri.AbsoluteUri);
            builder.ReplaceValue("{credentials}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            builder.ReplaceValue("{amount}", PagSeguroUtil.DecimalFormat(amount));
            builder.ReplaceValue("{cardBrand}", HttpUtility.UrlEncode(cardBrand));
            builder.ReplaceValue("{maxInstallmentNoInterest}", HttpUtility.UrlEncode(maxInstallmentNoInterest.ToString()));

            return builder.ToString();
        }
    }
}
