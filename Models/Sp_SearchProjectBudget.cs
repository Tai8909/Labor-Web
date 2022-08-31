﻿using System;
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
    public partial class Sp_SearchProjectBudget
    {
        public string bugname { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public decimal source_type { get; set; }
        public string fund_name { get; set; }
        public string Proj_Num { get; set; }

    }
}
