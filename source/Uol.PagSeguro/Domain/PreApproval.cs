using System;
using Uol.PagSeguro.Util;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// </summary>
    public class PreApproval
    {
        /// <summary>
        /// </summary>

        public string Charge { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public decimal AmountPerPayment { get; set; }
        public string Period { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public decimal MaxTotalAmount { get; set; }
        public decimal MaxAmountPerPeriod { get; set; }
        public int MaxPaymentsPerPeriod { get; set; }
        public int DayOfMonth { get; set; }
        public int DayOfWeek { get; set; }
        public int DayOfYear { get; set; }
    }
}