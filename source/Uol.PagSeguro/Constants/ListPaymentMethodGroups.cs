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


namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Defines a list of known accepted payment groups.
    /// </summary>
    public static class ListPaymentMethodGroups
    {
        /// <summary>
        /// PagSeguro Balance
        /// </summary>
        public const string Balance = "BALANCE";

        /// <summary>
        /// Boleto
        /// </summary>
        public const string Boleto = "BOLETO";

        /// <summary>
        /// Credit card
        /// </summary>
        public const string CreditCard = "CREDIT_CARD";

        /// <summary>
        /// Deposit
        /// </summary>
        public const string Deposit = "DEPOSIT";

        /// <summary>
        /// Online debit
        /// </summary>
        public const string ETF = "EFT";
    }
}
