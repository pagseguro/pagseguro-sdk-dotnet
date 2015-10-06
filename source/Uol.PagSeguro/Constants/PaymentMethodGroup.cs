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
    /// Defines a list of known payment method groups.
    /// </summary>
    public static class PaymentMethodGroup
    {
        /// <summary>
        /// Payment with Credit Card
        /// </summary>
        public const string CreditCard = "CREDIT_CARD";

        /// <summary>
        /// Payment with Boleto
        /// </summary>
        public const string Boleto = "BOLETO";

        /// <summary>
        /// Payment with Online Debit
        /// </summary>
        public const string EFT = "EFT";

        /// <summary>
        /// Payment with PagSeguro Balance
        /// </summary>
        public const string Balance = "BALANCE";

        /// <summary>
        /// Payment with Deposit
        /// </summary>
        public const string Deposit = "DEPOSIT";
    }
}
