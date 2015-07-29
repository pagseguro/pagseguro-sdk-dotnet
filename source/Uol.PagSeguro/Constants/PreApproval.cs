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

namespace Uol.PagSeguro.Constants.PreApproval
{

    /// <summary>
    /// Defines a list of known pre-approval charge types.
    /// </summary>
    /// <remarks>
    /// This class is not an enum to enable the introduction of new pre-approval charge types
    /// without breaking this version of the library.
    /// </remarks>
    public static class Charge
    {
        /// <summary>
        /// Auto
        /// </summary>
        public const string Auto = "auto";
        /// <summary>
        /// Manual
        /// </summary>
        public const string Manual = "manual";
    }

    /// <summary>
    /// Defines a list of known pre-approval periods.
    /// </summary>
    /// <remarks>
    /// This class is not an enum to enable the introduction of new pre-approval periods
    /// without breaking this version of the library.
    /// </remarks>
    public static class Period
    {
        /// <summary>
        /// Weekly
        /// </summary>
        public const string Weekly = "WEEKLY";

        /// <summary>
        /// Monthly
        /// </summary>
        public const string Monthly = "MONTHLY";

        /// <summary>
        /// Bimonthly
        /// </summary>
        public const string Bimonthly = "BIMONTHLY";

        /// <summary>
        /// Trimonthly
        /// </summary>
        public const string Trimonthly = "TRIMONTHLY";

        /// <summary>
        /// Semi Annually
        /// </summary>
        public const string SemiAnnually = "SEMIANNUALLY";

        /// <summary>
        /// Yearly
        /// </summary>
        public const string Yearly = "YEARLY";
    }

    /// <summary>
    /// Defines a list of known pre-approval days of week.
    /// </summary>
    /// <remarks>
    /// This class is not an enum to enable the introduction of new pre-approval days of week
    /// without breaking this version of the library.
    /// </remarks>
    public static class DayOfWeekMethod
    {
        /// <summary>
        /// Monday
        /// </summary>
        public const string Monday = "MONDAY";

        /// <summary>
        /// Tuesday
        /// </summary>
        public const string Tuesday = "TUESDAY";

        /// <summary>
        /// Wednesday
        /// </summary>
        public const string Wednesday = "WEDNESDAY";

        /// <summary>
        /// Thursday
        /// </summary>
        public const string Thursday = "THURSDAY";

        /// <summary>
        /// Friday
        /// </summary>
        public const string Friday = "FRIDAY";

        /// <summary>
        /// Saturday
        /// </summary>
        public const string Saturday = "SATURDAY";

        /// <summary>
        /// Sunday
        /// </summary>
        public const string Sunday = "SUNDAY";
    }

    /// <summary>
    /// Defines a list of known pre-approval status.
    /// </summary>
    /// <remarks>
    /// This class is not an enum to enable the introduction of new pre-approval status
    /// without breaking this version of the library.
    /// </remarks>
    public static class Status
    {
        /// <summary>
        /// Initiated
        /// </summary>
        public const string Initiated = "INITIATED";

        /// <summary>
        /// Pending
        /// </summary>
        public const string Pending = "PENDING";

        /// <summary>
        /// Active
        /// </summary>
        public const string Active = "ACTIVE";

        /// <summary>
        /// Cancelled
        /// </summary>
        public const string Cancelled = "CANCELLED";

        /// <summary>
        /// Cancelled by receiver
        /// </summary>
        public const string CancelledByReceiver = "CANCELLED_BY_RECEIVER";

        /// <summary>
        /// Cancelled by sender
        /// </summary>
        public const string CancelledBySender = "CANCELLED_BY_SENDER";

        /// <summary>
        /// Expired
        /// </summary>
        public const string Expired = "EXPIRED";
    }
}