using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checklist.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Column("EmpID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Employee ID is required")]
        [Display(Name = "Employee ID")]
        public int EmpID { get; set; }

        [Column("FirstName", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(20, ErrorMessage = "First Name must be less than 20")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(20, ErrorMessage = "Last Name must be less than 20")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Column("Title", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(20, ErrorMessage = "Title must be less than 20")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Column("Birthday", TypeName = "DateTime")]
        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Column("HireDate", TypeName = "DateTime")]
        [Required(ErrorMessage = "Hire Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Column("Country", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Country is required")]
        [StringLength(20, ErrorMessage = "Country must be less than 20")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Column("Note", TypeName = "varchar(50)")]
        [StringLength(20, ErrorMessage = "Note must be less than 20")]
        [Display(Name = "Note")]
        public string Note { get; set; }
    }
}