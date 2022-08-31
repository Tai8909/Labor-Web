using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checklist.ValidationAttributes;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class PaymentSearch
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Boss { get; set; }
        public string StudentID { get; set; }
        public decimal? LaborRank { get; set; }
        public decimal? RetireRank { get; set; }
        public double? SelfLabor { get; set; }
        public double? DepLabor { get; set; }
        public double? SelfRetire { get; set; }
        public double? DepRetire { get; set; }
        public double? LaborFund { get; set; }
        public DateTime PaymentStartTime { get; set; }
        public DateTime PaymentEndTime { get; set; }
        public int Salary { get; set; }
        public int? PartDay { get; set; }
        public string IsForeign { get; set; }
        public string InsuranceType { get; set; }
        public int SelfRetirePercent { get; set; }
        public string BossID { get; set; }
        public string FormNo { get; set; }
        public string ?ApplyItem { get; set; }
        public string? Hiredname { get; set; }
        public string? ID { get; set; }
        public DateTime? Birthday { get; set; }
        public Boolean Paid { get; set; }
    }
}
