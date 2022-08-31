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

namespace Checklist.Dtos
{
    public partial class SpInsuranceFeeDto
    {
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public int SelfRetirePercent { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        //[Required]
        public string PartDay { get; set; }
        [Required]
        public string Foreign { get; set; }
        [Required]
        public string InsuranceType { get; set; }
    }
}
