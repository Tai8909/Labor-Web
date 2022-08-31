using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class LabForm
    {
        public LabForm()
        {
            LabInsuranceFee = new HashSet<LabInsuranceFee>();
        }

        public string ApplyItem { get; set; }
        public string Class { get; set; }
        public string HiredName { get; set; }
        public string School { get; set; }
        public string InsuranceType { get; set; }
        public string HiredDep { get; set; }
        public string Boss { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public int No { get; set; }
        public string IsForeign { get; set; }
        public int Salary { get; set; }
        public string ID { get; set; }
        public DateTime Birthday { get; set; }
        public string WorkType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FormStatus { get; set; }
        public int? PartSalary { get; set; }
        public int? PartDay { get; set; }
        public string Disability { get; set; }
        public string Bugeno { get; set; }
        public string ReceivePerson { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public string RejectReason { get; set; }
        public string StudentID { get; set; }
        public int SelfRetirePercent { get; set; }
        public string Budgettype { get; set; }
        public string Fundname { get; set; }
        public string Bossdep { get; set; }
        public string Bossemail { get; set; }
        public string pclass { get; set; }
        public string Is_Nuk { get; set; }
        public string BossID { get; set; }       
        public virtual ICollection<LabInsuranceFee> LabInsuranceFee { get; set; }
    }
}
