using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checklist.ValidationAttributes;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class LabFormDetail
    {

        //申請項目:加保,退保
        public string ApplyItem { get; set; }

        //薪資類型:月薪,日薪,時薪
        public string Class { get; set; }

        //被保險人姓名
        public string HiredName { get; set; }

        //被保險人學校/系所
        public string School { get; set; }

        //投保類型:日保,月保
        public string? InsuranceType { get; set; }

        //用人單位
        public string HiredDep { get; set; }

        //雇主
        public string Boss { get; set; }

        //起始/退保時間
        public DateTime? StartTime { get; set; }

        //結束時間
        public DateTime? EndTime { get; set; }

        //收件時間
        public DateTime? ReceiveTime { get; set; }

        //申請編號
        public string No { get; set; }

        //本國人,外國人
        public string IsForeign { get; set; }

        //薪水
        public int Salary { get; set; }

        //身分證字號
        public string ID { get; set; }

        //出生日期
        public DateTime Birthday { get; set; }

        //工作類別:兼任助理,臨時工
        public string WorkType { get; set; }

        //被保險人Email
        public string Email { get; set; }

        //被保險人電話
        public string Phone { get; set; }

        //審核狀態
        public string FormStatus { get; set; }

        //每月/日/小時薪水
        public int? PartSalary { get; set; }
        public int? PartDay { get; set; }

        //是否有身心障礙:有,無
        public string Disability { get; set; }

        //計畫編號
        public string Bugeno { get; set; }
        public string Budgettype { get; set; }
        public string Fundname { get; set; }

        public string Bossdep { get; set; }
        public string Bossemail { get; set; }

        //收件人
        public string? ReceivePerson { get; set; }

        //審核失敗原因
        public string? RejectReason { get; set; }

        //被保險人學號
        public string StudentID { get; set; }

        //自提勞工退休金0,1,2,3,4,5,6%
        public int SelfRetirePercent { get; set; }

        public DateTime? ActualEndTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public decimal? LaborRank { get; set; }
        public decimal? RetireRank { get; set; }
        public double? SelfLabor { get; set; }
        public double? DepLabor { get; set; }
        public double? SelfRetire { get; set; }
        public double? DepRetire { get; set; }
        public double? LaborFund { get; set; }

        public string? Origin { get; set; }
        public string? pclass { get; set; }
        public string? Is_Nuk { get; set; }
        public string? BossID { get; set; }
    }
}
