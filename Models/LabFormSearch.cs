using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checklist.ValidationAttributes;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class LabFormSearch
    {
        //申請編號
        public string No { get; set; }

        //被保險人學號
        public string StudentID { get; set; }

        //被保險人姓名
        public string HiredName { get; set; }

        //計畫編號
        public string Bugeno { get; set; }

        //起始/退保時間
        public DateTime? StartTime { get; set; }

        //結束時間
        public DateTime? EndTime { get; set; }

        //審核狀態
        public string FormStatus { get; set; }
        public string Item { get; set; }
        public string Is_foreign { get; set; }
        public string ID { get; set; }
        public DateTime Birthday { get; set; }
        public string? BossID { get; set; }
        public int? SelfRetirePercent { get; set; }
        public int? Salary { get; set; }

    }
}
