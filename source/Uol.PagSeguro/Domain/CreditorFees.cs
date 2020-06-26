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
    /// Represents a product/creditorFees in a transaction
    /// </summary>
    public class CreditorFees
    {
        /// <summary>
        /// Rate amount
        /// </summary>
        public decimal IntermediationRateAmount { get; set; }

        /// <summary>
        /// Fee amount
        /// </summary>
        public decimal IntermediationFeeAmount { get; set; }

        internal CreditorFees()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CreditorFees class
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public CreditorFees(decimal intermediationRateAmount, decimal intermediationFeeAmount)
        {
            IntermediationRateAmount = intermediationRateAmount;
            IntermediationFeeAmount = intermediationFeeAmount;
        }
    }
}
