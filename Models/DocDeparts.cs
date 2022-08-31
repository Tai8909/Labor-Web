using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class DocDeparts
    {
        public int? DepNo { get; set; }
        public string DepCname { get; set; }
        public string Depalias { get; set; }
    }
}
