using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Checklist.ValidationAttributes;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class PaymentTime
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Boss { get; set; }
        public string Payment { get; set; }
        public Boolean Paid { get; set; }
    }
}
