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
        public string Charge 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public string Details
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public decimal AmountPerPayment
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public string Period
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public DateTime InitialDate
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public DateTime FinalDate
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public decimal MaxTotalAmount
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public decimal MaxAmountPerPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public int MaxPaymentsPerPeriod
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public int DayOfMonth
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public int DayOfWeek
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public int DayOfYear
        {
            get;
            set;
        }
    }
}