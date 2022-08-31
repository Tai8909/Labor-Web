using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class PaymentTimeFee
    {
        public string StudentID { get; set; }
        public decimal? LaborRank { get; set; }
        public decimal? RetireRank { get; set; }
        public double? SelfLabor { get; set; }
        public double? DepLabor { get; set; }
        public double? SelfRetire { get; set; }
        public double? DepRetire { get; set; }
        public double? LaborFund { get; set; }
        public string Boss { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int No { get; set; }
        public int Salary { get; set; }
        public int SelfRetirePercent { get; set; }
        public int FormNo { get; set; }
        public string Origin { get; set; }
        public Boolean Paid { get; set; }
    }
}
