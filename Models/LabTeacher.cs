using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class LabTeacher
    {
        public string UserName { get; set; }
        public int UserNo { get; set; }
        public string DepName { get; set; }
        public int? DepartId { get; set; }
        public string State { get; set; }
    }
}
