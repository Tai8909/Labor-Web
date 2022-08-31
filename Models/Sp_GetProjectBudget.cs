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
    public partial class Sp_GetProjectBudget
    {
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string FundNo { get; set; }
        public string FundName { get; set; }
        public decimal Budget { get; set; }
        public decimal Budget_remain { get; set; }
        public decimal SourceType { get; set; }
        public string ProjNo { get; set; }
        public string PJTTYPE { get; set; }
        public string StartDate { get; set; }
        public string LeaderCard { get; set; }
        public string LeaderName { get; set; }
        public string COM { get; set; }
        public string COMName { get; set; }
        public string EndDate { get; set; }

    }
}
