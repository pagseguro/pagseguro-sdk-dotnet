using System.Globalization;
using System.Net;
using System.Xml;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Direct;
using Uol.PagSeguro.Log;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionService
    {

        /// <summary>
        /// Request a direct payment session
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <returns><c cref="T:Uol.PagSeguro.CancelRequestResponse">Result</c></returns>
        public static Session CreateSession(Credentials credentials)
        {

            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "SessionService.Register() - begin"));
            try
            {
                using (var response = HttpUrlConnectionUtil.GetHttpPostConnection(
                    PagSeguroUris.GetSessionUri(credentials).AbsoluteUri, BuildSessionUrl(credentials)))
                {

                    using (var reader = XmlReader.Create(response.GetResponseStream()))
                    {

                        var result = new Session();
                        SessionSerializer.Read(reader, result);
                        PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "SessionService.Register({0}) - end", result.ToString()));
                        return result;
                    }
                }
            }
            catch (WebException exception)
            {
                var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "SessionService.Register() - error {0}", pse));
                throw pse;
            }
        }

        private static string BuildSessionUrl(Credentials credentials)
        {
            var builder = new QueryStringBuilder();
            builder.EncodeCredentialsAsQueryString(credentials);
            return builder.ToString();
        }

    }
}
