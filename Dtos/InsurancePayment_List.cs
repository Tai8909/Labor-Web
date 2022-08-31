using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Dtos
{
    public partial class InsurancePayment_List
    {
        public List<InsurancePayment> InsurancePaymentList { get; set; }
        public string StudentID { get; set; }
    }
}
