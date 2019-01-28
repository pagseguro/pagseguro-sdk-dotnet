using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using Uol.PagSeguro.NetStandard.Domain;
using Uol.PagSeguro.NetStandard.Domain.Installment;
using Uol.PagSeguro.NetStandard.Exception;
using Uol.PagSeguro.NetStandard.Log;
using Uol.PagSeguro.NetStandard.Resources;
using Uol.PagSeguro.NetStandard.Util;
using Uol.PagSeguro.NetStandard.XmlParse;

namespace Uol.PagSeguro.NetStandard.Service
{
    public class InstallmentService
    {

        /// <summary>
        /// Request a direct payment session
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <returns><c cref="T:Uol.PagSeguro.NetStandard.CancelRequestResponse">Result</c></returns>
        public static Installments GetInstallments(Credentials credentials, Decimal amount, String cardBrand, Int32 maxInstallmentNoInterest)
        {

            PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "InstallmentService.GetInstallments() - begin"));
            try
            {
                using (HttpWebResponse response = HttpURLConnectionUtil.GetHttpGetConnection(
                    BuildInstallmentURL(credentials, amount, cardBrand, maxInstallmentNoInterest)))
                {
                    using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                    {

                        Installments result = new Installments();
                        InstallmentsSerializer.Read(reader, result);
                        PagSeguroTrace.Info(String.Format(CultureInfo.InvariantCulture, "InstallmentService.Register({0}) - end", result.ToString()));
                        return result;
                    }
                }
            }
            catch (ArgumentException exception)
            {
                PagSeguroServiceException pse = new PagSeguroServiceException(exception.Message);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "InstallmentService.Register() - error {0}", exception.Message));
                throw pse;
            }
            catch (WebException exception)
            {
                PagSeguroServiceException pse = HttpURLConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(String.Format(CultureInfo.InvariantCulture, "InstallmentService.Register() - error {0}", pse));
                throw pse;
            }
        }

        private static String BuildInstallmentURL(Credentials credentials, Decimal amount, String cardBrand, Int32 maxInstallmentNoInterest)
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
