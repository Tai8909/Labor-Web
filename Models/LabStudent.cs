using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class LabStudent
    {
        public string Name { get; set; }
        public int StudentId { get; set; }
        public int Id { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public DateTime Birthday { get; set; }
    }
}
