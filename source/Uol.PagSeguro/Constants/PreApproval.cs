namespace Uol.PagSeguro.Constants.PreApproval
{
    public static class Charge
    {
        public const string Auto = "auto";
        public const string Manual = "manual";
    }

    public static class Period
    {
        public const string Weekly = "WEEKLY";
        public const string Monthly = "MONTHLY";
        public const string Bimonthly = "BIMONTHLY";
        public const string Trimonthly = "TRIMONTHLY";
        public const string SemiAnnually = "SEMIANNUALLY";
        public const string Yearly = "YEARLY";
    }

    public static class DayOfWeekMethod
    {
        public const string Monday = "MONDAY";
        public const string Tuesday = "TUESDAY";
        public const string Wednesday = "WEDNESDAY";
        public const string Thursday = "THURSDAY";
        public const string Friday = "FRIDAY";
        public const string Saturday = "SATURDAY";
        public const string Sunday = "SUNDAY";
    }

    public static class Status
    {
        public const string Initiated = "INITIATED";
        public const string Pending = "PENDING";
        public const string Active = "ACTIVE";
        public const string Cancelled = "CANCELLED";
        public const string CancelledByReceiver = "CANCELLED_BY_RECEIVER";
        public const string CancelledBySender = "CANCELLED_BY_SENDER";
        public const string Expired = "EXPIRED";
    }
}