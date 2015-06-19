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

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Represents a PagSeguro pre-approval transaction
    /// </summary>
    public class PreApprovalTransaction
    {
        private IList<Item> items;

        internal PreApprovalTransaction()
        {
        }

        /// <summary>
        /// Pre-approval transaction date
        /// </summary>
        public DateTime Date
        {
            get;
            internal set;
        }

        /// <summary>
        /// Pre-approval transaction code
        /// </summary>
        public string Code
        {
            get;
            internal set;
        }

        /// <summary>
        /// Reference code
        /// </summary>
        /// <remarks>
        /// You can use the reference code to store an identifier so you can 
        /// associate the PagSeguro transaction to a transaction in your system.
        /// </remarks>
        public string Reference
        {
            get;
            internal set;
        }

        /// <summary>
        /// Pre-approval transaction type
        /// </summary>
        public int PreApprovalTransactionType
        {
            get;
            internal set;
        }
      
        /// <summary>
        /// Last event date
        /// </summary>
        public DateTime LastEventDate
        {
            get;
            internal set;
        }

        /// <summary>
        /// Products/items in this pre-approval transaction
        /// </summary>
        public IList<Item> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = new List<Item>();
                }
                return items;
            }
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get;
            internal set;
        }

        /// <summary>
        /// Tracker
        /// </summary>
        public string Tracker
        {
            get;
            internal set;
        }

        /// <summary>
        /// Status
        /// </summary>
        public string Status
        {
            get;
            internal set;
        }

        /// <summary>
        /// charge
        /// </summary>
        /// <remarks>
        /// Manual or Auto
        /// </remarks>
        public string Charge
        {
            get;
            internal set;
        }

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetType().Name);
            builder.Append('(');
            builder.Append("Code=").Append(this.Code).Append(", ");
            builder.Append("Date=").Append(this.Date).Append(", ");
            builder.Append("Reference=").Append(this.Reference.ToString()).Append(", ");
            builder.Append("Status=").Append(this.Status).Append(", ");
            builder.Append("Charge=").Append(this.Charge);
            builder.Append(')');
            return builder.ToString();
        }
    }
}
