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
    public partial class Sp_GetProjectBudgetDto
    {
        [Required]
        public string p_UserCard { get; set; }
    }
}
