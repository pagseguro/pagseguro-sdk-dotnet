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
//   limitation


namespace Uol.PagSeguro.Constants
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TransactionSerializerHelper
    {
        internal const string Transaction = "transaction";
        internal const string Code = "code";
        internal const string Date = "date";
        internal const string Reference = "reference";
        internal const string TransactionType = "type";
        internal const string TransactionStatus = "status";
        internal const string GrossAmount = "grossAmount";
        internal const string DiscountAmount = "discountAmount";
        internal const string FeeAmount = "feeAmount";
        internal const string NetAmount = "netAmount";
        internal const string ExtraAmount = "extraAmount";
        internal const string LastEventDate = "lastEventDate";
        internal const string InstallmentCount = "installmentCount";

        //PreApproval
        internal const string PreApproval = "preApproval";
        internal const string Name = "name";
        internal const string Tracker = "tracker";
        internal const string Charge = "charge";
    }
}
