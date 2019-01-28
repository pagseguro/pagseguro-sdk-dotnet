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
    public class InstallmentService
    {
        /// <summary>
        /// Request a direct payment session
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <returns><c cref="T:Uol.PagSeguro.CancelRequestResponse">Result</c></returns>
        public static Installments GetInstallments(Credentials credentials, decimal amount, string cardBrand, int maxInstallmentNoInterest)
        {
            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "InstallmentService.GetInstallments() - begin"));
            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(
                    BuildInstallmentURL(credentials, amount, cardBrand, maxInstallmentNoInterest)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        Installments result = new Installments();
                        InstallmentsSerializer.Read(reader, result);
                        PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "InstallmentService.Register({0}) - end", result.ToString()));
                        return result;
                    }
                }
            }
            catch (ArgumentException exception)
            {
                PagSeguroServiceException pse = new PagSeguroServiceException(exception.Message);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "InstallmentService.Register() - error {0}", exception.Message));
                throw pse;
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "InstallmentService.Register() - error {0}", pse));
                throw pse;
            }
        }

        private static string BuildInstallmentURL(Credentials credentials, decimal amount, string cardBrand, int maxInstallmentNoInterest)
        {
            QueryStringBuilder builder = new QueryStringBuilder("{url}?{credentials}&amount={amount}&cardBrand={cardBrand}&maxInstallmentNoInterest={maxInstallmentNoInterest}");

            builder.ReplaceValue("{url}", PagSeguroConfiguration.InstallmentUri.AbsoluteUri);
            builder.ReplaceValue("{credentials}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            builder.ReplaceValue("{amount}", PagSeguroUtil.DecimalFormat(amount));
            builder.ReplaceValue("{cardBrand}", HttpUtility.UrlEncode(cardBrand.ToString()));
            builder.ReplaceValue("{maxInstallmentNoInterest}", HttpUtility.UrlEncode(maxInstallmentNoInterest.ToString()));

            return builder.ToString();
        }
    }
}