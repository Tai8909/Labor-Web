using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class LabPayment
    {
        public int No { get; set; }
        public string StudentID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Payment { get; set; }
        public string FormNo { get; set; }
        public Boolean Paid { get; set; }
    }
}
