using Checklist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checklist.ValidationAttributes;

namespace Checklist.Dtos
{
    [FormStatus]
    public class LabFormPutDto
    {
        //申請編號
        public int No { get; set; }

        //收件時間
        public DateTime ReceiveTime { get; set; }

        //投保類型:日保,月保
        public string InsuranceType { get; set; }

        //審核狀態
        public string FormStatus { get; set; }

        //收件人
        [Required(ErrorMessage = "收件人不能為空")]
        public string ReceivePerson { get; set; }

        //實際開始時間
        public DateTime ActualStartTime { get; set; }

        //審核失敗原因
        public string RejectReason { get; set; }

        //勞保金級距
        public decimal LaborRank { get; set; }

        //勞退金級距
        public decimal RetireRank { get; set; }

        //勞保自提
        public double SelfLabor { get; set; }

        //勞保公提
        public double DepLabor { get; set; }

        //勞退自提
        public double SelfRetire { get; set; }

        //勞退公提
        public double DepRetire { get; set; }

        //勞保工資墊償金
        public double LaborFund { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    schoolContext _schoolcontext = (schoolContext)validationContext.GetService(typeof(schoolContext));
        //    if (ReceiveTime >= ActualStartTime)
        //    {
        //        yield return new ValidationResult("收件時間不能大於實際開始時間", new string[] { "time" });
        //    }
        //}
    }
}
