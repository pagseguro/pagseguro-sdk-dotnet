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

using System.Globalization;
using System.Net;
using System.Xml;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Direct;
using Uol.PagSeguro.Log;
using Uol.PagSeguro.Parse;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.XmlParse;

namespace Uol.PagSeguro.Service
{
    /// <summary>
    /// Encapsulates web service calls to search for PagSeguro transactions
    /// </summary>
    public static class TransactionService
    {

        /// <summary>
        /// Create a new transaction checkout
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="checkout"></param>
        /// <returns cref="T:Uol.PagSeguro.Transaction"><c>Transaction</c></returns>
        public static Transaction CreateCheckout(Credentials credentials, Checkout checkout)
        {
            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "TransactionService.Register() - begin"));
            try
            {
                using (var response = HttpUrlConnectionUtil.GetHttpPostConnection(
                    PagSeguroUris.GetTransactionsUri(credentials).AbsoluteUri, BuildTransactionUrl(credentials, checkout)))
                {
                    using (var reader = XmlReader.Create(response.GetResponseStream()))
                    {
                        var transaction = new Transaction();
                        TransactionSerializer.Read(reader, transaction);
                        PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "TransactionService.Register() - end {0}", transaction));
                        return transaction;
                    }
                }
            }
            catch (WebException exception)
            {
                var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response, exception);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "TransactionService.Register() - error {0}", pse));
                throw pse;
            }
        }

        internal static string BuildTransactionUrl(Credentials credentials, Checkout checkout)
        {
            var builder = new QueryStringBuilder();
            var data = TransactionParse.GetData(checkout);

            builder.EncodeCredentialsAsQueryString(credentials);

            foreach (var pair in data)
            {
                builder.Append(pair.Key, pair.Value);
            }

            return builder.ToString();
        }

     }
}