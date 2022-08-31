using Checklist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Dtos
{
    public class LabFormPostDto:IValidatableObject
    {
        //申請項目:加保,退保
        public string ApplyItem { get; set; }

        //薪資類型:月薪,日薪,時薪
        public string Class { get; set; }

        //被保險人姓名
        [Required(ErrorMessage = "被保險人姓名不能為空")]
        public string HiredName { get; set; }

        //被保險人學校/系所
        [Required(ErrorMessage = "被保險人學校/系所不能為空")]
        public string School { get; set; }

        //用人單位
        [Required(ErrorMessage = "用人單位不能為空")]
        public string HiredDep { get; set; }

        //雇主
        [Required(ErrorMessage = "雇主不能為空")]
        public string Boss { get; set; }

        //起始/退保時間
        [Required(ErrorMessage = "起始/退保時間不能為空")]
        public DateTime? StartTime { get; set; }

        //結束時間
        [Required(ErrorMessage = "結束時間不能為空")]
        public DateTime? EndTime { get; set; }

        //本國人,外國人
        public string IsForeign { get; set; }

        //薪水
        [Required(ErrorMessage = "薪水不能為空")]
        public int? Salary { get; set; }

        //身分證字號
        [Required(ErrorMessage = "身分證字號不能為空")]
        public string ID { get; set; }

        //出生日期
        [Required(ErrorMessage = "出生日期不能為空")]
        public DateTime? Birthday { get; set; }

        //工作類別:兼任助理,臨時工
        public string WorkType { get; set; }

        //被保險人Email
        [Required(ErrorMessage = "被保險人Email不能為空")]
        [EmailAddress]
        public string Email { get; set; }

        //被保險人電話
        [Required(ErrorMessage = "被保險人電話不能為空")]
        public string Phone { get; set; }

        //審核狀態
        public string FormStatus { get; set; }

        //每月/日/小時薪水
        public int? PartSalary { get; set; }
        public int? PartDay { get; set; }

        //是否有身心障礙:有,無
        public string Disability { get; set; }

        //計畫編號
        [Required(ErrorMessage = "計畫編號不能為空")]
        public string? Bugeno { get; set; }
        public string Budgettype { get; set; }
        public string Fundname { get; set; }
        public string Bossdep { get; set; }
        public string Bossemail { get; set; }

        //被保險人學號
        [Required(ErrorMessage = "被保險人學號不能為空")]
        public string StudentID { get; set; }

        //自提勞工退休金0,1,2,3,4,5,6%
        public int SelfRetirePercent { get; set; }
        public string pclass { get; set; }
        public string Is_Nuk { get; set; }
        public string BossID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            schoolContext _schoolcontext = (schoolContext)validationContext.GetService(typeof(schoolContext));
            if ((Class=="日薪"||Class=="時薪")&&(PartSalary==null))
            {
                yield return new ValidationResult("部分薪資不能為空", new string[] { "PartSalary" });
            }
        }
    }
}
