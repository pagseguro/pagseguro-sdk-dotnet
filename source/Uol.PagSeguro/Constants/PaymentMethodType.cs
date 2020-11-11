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
    /// Defines a list of known payment method types.
    /// </summary>
    /// <remarks>
    /// This class is not an enum to enable the introduction of new payment method types
    /// without breaking this version of the library.
    /// </remarks>
    public static class PaymentMethodType
    {
        /// <summary>
        /// Credit card
        /// </summary>
        public static int CreditCard
     {
      get { return 1; }
     }
        /// <summary>
        /// Boleto is a form of invoicing in Brazil
        /// </summary>
        public static int Boleto
     {
      get { return 2; }
     }
        /// <summary>
        /// Online transfer
        /// </summary>
        public static int OnlineTransfer
     {
      get { return 3; }
     }
        /// <summary>
        /// PagSeguro account balance
        /// </summary>
        public static int Balance
     {
      get { return 4; }
     }
        /// <summary>
        /// OiPaggo
        /// </summary>
        public static int OiPaggo
     {
      get { return 5; }
     }
        /// <summary>
        /// Direct Deposit
        /// </summary>
        public static int DirectDeposit
             {
      get { return 7; }
     }
    }
}
