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
    public partial class Sp_StudentInfo
    {
        public string Student_ID { get; set; }
        public string cname { get; set; }
        public string ID { get; set; }
        public string Birthday { get; set; }
        public string pclass { get; set; }
        public string sclass { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
