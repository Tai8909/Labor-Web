using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checklist.ValidationAttributes;
using System.Linq;
using System.Threading.Tasks;
using Checklist.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Dtos
{
    public partial class SspGetDepBudgetDTO
    {
        [Required]
        public decimal DepNo { get; set; }
        [Required]
        public decimal DeppartID { get; set; }
        [Required]
        public string ProjectNo { get; set; }
        [Required]
        public string FundNo { get; set; }
        [Required]
        public string FundName { get; set; }
        [Required]
        public decimal Budget { get; set; }
        [Required]
        public decimal BudgetRemain { get; set; }
        [Required]
        public string SourceType { get; set; }
        [Required]
        public string Leader { get; set; }
        //[Required]
        //public int WorkDay { get; set; }
        [Required]
        public int LeaderID { get; set; }
        [Required]
        public string DepartName { get; set; }
    }
}
