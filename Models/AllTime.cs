using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checklist.ValidationAttributes;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class AllTime
    {
        public DateTime? Time { get; set; }
        public string se { get; set; }
        public string Boss { get; set; }
        public string Payment { get; set; }
        public string No { get; set; }
    }
}
