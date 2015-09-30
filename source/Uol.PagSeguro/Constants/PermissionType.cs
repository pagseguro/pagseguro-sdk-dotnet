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
using System.Text;

namespace Uol.PagSeguro.Constants
{
    /// <summary>
    /// Defines a list of known authorization permission type.
    /// </summary>
    /// <remarks>
    /// This class is not an enum to enable the introduction of authorization permission type
    /// without breaking this version of the library.
    /// </remarks>
    public class PermissionType
    {
        /// <summary>
        /// Create checkouts
        /// </summary>
        public const string CREATE_CHECKOUTS = "CREATE_CHECKOUTS";

        /// <summary>
        /// Receive transaction notifications
        /// </summary>
        public const string RECEIVE_TRANSACTION_NOTIFICATIONS = "RECEIVE_TRANSACTION_NOTIFICATIONS";

        /// <summary>
        /// Search transactions
        /// </summary>
        public const string SEARCH_TRANSACTIONS = "SEARCH_TRANSACTIONS";

        /// <summary>
        /// Manage payment pre-approvals
        /// </summary>
        public const string MANAGE_PAYMENT_PRE_APPROVALS = "MANAGE_PAYMENT_PRE_APPROVALS";

        /// <summary>
        /// Direct payment
        /// </summary>
        public const string DIRECT_PAYMENT = "DIRECT_PAYMENT";
    }

}
