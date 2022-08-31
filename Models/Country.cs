using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checklist.Models
{
    [Table("Country")]
    public class Country
    {
        [Display(Name = "Country Code")]
        [Column("CountryID")]
        public int CountryID { get; set; }

        [Display(Name = "Country Name")]
        [Column("CountryName")]
        public string CountryName { get; set; }
    }
}