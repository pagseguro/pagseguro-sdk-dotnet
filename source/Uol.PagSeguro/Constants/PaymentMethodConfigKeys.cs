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

namespace Uol.PagSeguro.Constants
{
    /// <summary>
    /// Defines a list of known payment method config keys.
    /// </summary>
    public static class PaymentMethodConfigKeys
    {
        /// <summary>
        /// Discount percentage
        /// </summary>
        public const string DiscountPercent = "DISCOUNT_PERCENT";

        /// <summary>
        /// Installment without interest
        /// </summary>
        public const string MaxInstallmentsNoInterest = "MAX_INSTALLMENTS_NO_INTEREST";

        /// <summary>
        /// Installmnet limit
        /// </summary>
        public const string MaxInstallmentsLimit = "MAX_INSTALLMENTS_LIMIT";
    }
}
