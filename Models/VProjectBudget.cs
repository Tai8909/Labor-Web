using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class VProjectBudget
    {
        public string Bugetno { get; set; }
        public string Bugname { get; set; }
        public string Projectno { get; set; }
        public string Leader { get; set; }
        public string Leaderid { get; set; }
        public string Leaderdep { get; set; }
        public string Fundno { get; set; }
        public string Fundname { get; set; }
        public decimal Budget { get; set; }
        public decimal Shiftin { get; set; }
        public decimal Shiftout { get; set; }
        public decimal? Remain { get; set; }
        public string Start { get; set; }
        public string Deadline { get; set; }
        public string Pjttype { get; set; }
        public decimal Sourcetype { get; set; }
        public string Com { get; set; }
        public string Comname { get; set; }
        public string Closetype { get; set; }
    }
}
