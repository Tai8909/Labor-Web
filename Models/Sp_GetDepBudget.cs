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

namespace Checklist.Models
{
    public partial class Sp_GetDepBudget
    {
        public string DepNo { get; set; }
        public string DepartID { get; set; }
        public string ProjectNo { get; set; }
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public decimal Budget { get; set; }
        public decimal Budget_remain { get; set; }
        public decimal SourceType { get; set; }
        public string Leader { get; set; }
        public string LeaderID { get; set; }
        public string DepCName { get; set; }

    }
}
