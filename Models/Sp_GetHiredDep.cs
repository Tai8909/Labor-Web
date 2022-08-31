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
    public partial class Sp_GetHiredDep
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string Depart { get; set; }
        public string DepName { get; set; }

    }
}
