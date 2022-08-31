using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class DocVwMultiUser
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string DepartId { get; set; }
        public string DepName { get; set; }
        public string DepAlias { get; set; }
    }
}
