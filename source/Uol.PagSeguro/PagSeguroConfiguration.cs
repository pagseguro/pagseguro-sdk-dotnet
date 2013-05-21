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
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Uol.PagSeguro
{
    internal static class PagSeguroConfiguration
    {
        private static readonly Uri defaultPaymentUri = new Uri("https://ws.pagseguro.uol.com.br/v2/checkout");
        private static readonly Uri defaultPaymentRedirectUri = new Uri("https://pagseguro.uol.com.br/v2/checkout/payment.html");
        private static readonly Uri defaultNotificationUri = new Uri("https://ws.pagseguro.uol.com.br/v2/transactions/notifications");
        private static readonly Uri defaultSearchUri = new Uri("https://ws.pagseguro.uol.com.br/v2/transactions");

        private const int defaultRequestTimeout = 10000;

        internal static Uri NotificationUri
        {
            get
            { 
                return defaultNotificationUri;
            }
        }
        
        internal static Uri PaymentUri
        {
            get
            {
                return defaultPaymentUri;
            }
        }

        internal static Uri PaymentRedirectUri
        {
            get
            {
                return defaultPaymentRedirectUri;
            }
        }    

        internal static Uri SearchUri
        {
            get
            {
                return defaultSearchUri;
            }
        }

        internal static int RequestTimeout
        {
            get
            {
                return defaultRequestTimeout;
            }
        }
    }
}
