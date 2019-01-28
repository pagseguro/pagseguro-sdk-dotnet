﻿// Copyright [2011] [PagSeguro Internet Ltda.]
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
    /// Represents a page of pre-approval transactions returned by the pre-approval search service
    /// </summary>
    public class PreApprovalSearchResult
    {
        private readonly List<PreApprovalSummary> preApprovals = new List<PreApprovalSummary>();

        /// <summary>
        /// Date/time when this search was executed
        /// </summary>
        public DateTime Date
        {
            get;
            internal set;
        }

        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage
        {
            get;
            internal set;
        }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages
        {
            get;
            internal set;
        }

        /// <summary>
        /// PreApprovals in this page
        /// </summary>
        public IList<PreApprovalSummary> PreApprovals => preApprovals;

        /// <summary>
        /// Returns a string that represents the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append('(');
            builder.Append("Date=").Append(Date).Append(", ");
            builder.Append("CurrentPage=").Append(CurrentPage).Append(", ");
            builder.Append("TotalPages=").Append(TotalPages).Append(", ");
            builder.Append("PreApprovals in this page=").Append(PreApprovals.Count);
            builder.Append(')');

            return builder.ToString();
        }
    }
}