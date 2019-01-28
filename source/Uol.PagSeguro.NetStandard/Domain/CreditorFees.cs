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
using Uol.PagSeguro.NetStandard.Util;

namespace Uol.PagSeguro.NetStandard.Domain
{
    /// <summary>
    /// Represents a product/creditorFees in a transaction
    /// </summary>
    public class CreditorFees
    {
        internal CreditorFees()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CreditorFees class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="quantity"></param>
        /// <param name="amount"></param>
        public CreditorFees(decimal intermediationRateAmount, decimal intermediationFeeAmount)
        {
            this.intermediationRateAmount = intermediationRateAmount;
            this.intermediationFeeAmount = intermediationFeeAmount;
        }

 
        /// <summary>
        /// Rate amount
        /// </summary>
        public decimal intermediationRateAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Fee amount
        /// </summary>
        public decimal intermediationFeeAmount
        {
            get;
            set;
        }
    }
}
