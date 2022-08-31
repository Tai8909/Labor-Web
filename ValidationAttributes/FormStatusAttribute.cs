using Checklist.Models;
using Checklist.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checklist.ValidationAttributes
{
    public class FormStatusAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            schoolContext _schoolContext = (schoolContext)validationContext.GetService(typeof(schoolContext));
            var temp = (LabFormPutDto)value;
            if (temp.FormStatus =="審核失敗"&&temp.RejectReason=="")
            {
                return new ValidationResult("審核失敗原因不能為空", new string[] { "RejectReason" });
            }if(temp.FormStatus == "審核通過")
            {
                if (temp.ActualStartTime == null)
                {
                    return new ValidationResult("實際開始時間不能為空", new string[] { "ActualStartTime" });
                }
                var ReceiveTime = DateTime.Now;
                if (ReceiveTime >= temp.ActualStartTime)
                {
                    return new ValidationResult("收件時間不能大於實際開始時間", new string[] { "ActualStartTime" });
                }
            }
            return ValidationResult.Success;
        }
    }
}