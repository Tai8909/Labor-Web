using Checklist.Dtos;
using Checklist.Models;
using Checklist.Parameters;
using Checklist.Services;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Checklist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly schoolContext _schoolcontext;
        private readonly LabFormService _labFormService;
        public SchoolController(schoolContext schoolcontext, LabFormService labFormService)
        {
            _schoolcontext = schoolcontext;
            _labFormService = labFormService;
        }
        [HttpGet("Search")]
        public IEnumerable<LabFormSearch> GetSearch([FromQuery] LabFormSelectParameter labForm)
        {
            var temp = _labFormService.GetSearch();
            var result = _labFormService.Get(labForm,temp);
            return result;
        }

        [HttpGet]
        public IEnumerable<LabFormSearch> Get([FromQuery] LabFormSelectParameter labForm)
        {
            var temp = _labFormService.GetManage();
            var result = _labFormService.Get(labForm,temp);
            return result;
        }

        [HttpGet("{No}")]
        public IQueryable<LabFormDetail> GetOne(string No)
        {
            var result = _labFormService.GetOne(No);
            //if(result == null)
            //{
            //    return NotFound();
            //}
            return result;
        }

        [HttpGet("Search/{No}")]
        public IEnumerable<LabFormDetail> GetOneSearch(int No)
        {
            var result = _labFormService.GetOneSearch(No);
            //if(result == null)
            //{
            //    return NotFound();
            //}
            return result;
        }

        [HttpGet("StudentID")]
        public IEnumerable<LabInsuranceFee> GetStudentID()
        {
            var result = _labFormService.GetStudentID();
            return result;
        }

        [HttpGet("StudentID/{StudentID}")]
        public IEnumerable<LabInsuranceFee> GetStudentID_Search(string StudentID)
        {
            var result = _labFormService.GetStudentID_Search(StudentID);
            return result;
        }

        [HttpGet("ManageFee")]
        public IEnumerable<PaymentTimeFee> ManageFee(string StudentID)
        {
            var result = _labFormService.ManageFee(StudentID);
            return result;
        }

        [HttpGet("ManagePaymentTime")]
        public IEnumerable<PaymentTime> ManagePaymentTime(string StudentID)
        {
            var result = _labFormService.ManagePaymentTime(StudentID);
            return result;
        }

        //表單資料上傳
        [HttpPost]
        public IActionResult Post([FromBody] LabFormPostDto labForm)
        {
            LabForm insert = _labFormService.Post(labForm);
            return CreatedAtAction(nameof(GetOne), new { No = insert.No }, insert);
        }

        [HttpGet("InsuranceFeePayment")]
        public IEnumerable<PaymentSearch> InsuranceFeePayment([FromQuery] PaymentSelectParameter Payment)
        {
            var result = _labFormService.InsuranceFeePayment(Payment);
            return result;
        }

        [HttpPost("InsuranceTime")]
        public void InsuranceTime(InsuranceTime_List insuranceTime_List) 
        {
            for(int i = 0; i < insuranceTime_List.InsuranceTimeList.Count(); i++)
            {
                if (insuranceTime_List.InsuranceTimeList[i].Origin.Equals("together"))
                {
                    var result = _schoolcontext.LabInsuranceFee
                        .Where(a => a.FormNo == insuranceTime_List.InsuranceTimeList[i].FormNo)
                        .ToList();
                    for(int j = 0; j < result.Count(); j++)
                    {
                        if (result[j].Origin.Contains("o2-"))
                        {
                            _schoolcontext.LabInsuranceFee.Remove(result[j]);
                            _schoolcontext.SaveChanges();
                        }
                    }
                }
                else
                {
                    var result = _schoolcontext.LabInsuranceFee
                        .Where(a => a.FormNo == insuranceTime_List.InsuranceTimeList[i].FormNo && a.Origin == insuranceTime_List.InsuranceTimeList[i].Origin)
                        .SingleOrDefault();
                    if (result == null)
                    {
                        if (insuranceTime_List.InsuranceTimeList[i].Origin == "0")
                        {
                            var temp = _schoolcontext.LabInsuranceFee
                                .Where(a => a.FormNo == insuranceTime_List.InsuranceTimeList[i].FormNo && a.Origin != "-1");
                            _schoolcontext.LabInsuranceFee.RemoveRange(temp);
                            _schoolcontext.SaveChanges();
                            result = _schoolcontext.LabInsuranceFee
                                .Where(a => a.FormNo == insuranceTime_List.InsuranceTimeList[i].FormNo && a.Origin == "-1")
                                .SingleOrDefault();
                            result.Origin = insuranceTime_List.InsuranceTimeList[i].Origin;
                            _schoolcontext.LabInsuranceFee.Update(result);
                            _schoolcontext.SaveChanges();
                        }
                        else if (insuranceTime_List.InsuranceTimeList[i].Origin == "-1")
                        {
                            result = _schoolcontext.LabInsuranceFee
                                .Where(a => a.FormNo == insuranceTime_List.InsuranceTimeList[i].FormNo && a.Origin == "0")
                                .SingleOrDefault();
                            result.Origin = insuranceTime_List.InsuranceTimeList[i].Origin;
                            _schoolcontext.LabInsuranceFee.Update(result);
                            _schoolcontext.SaveChanges();
                        }else if (insuranceTime_List.InsuranceTimeList[i].Origin == "-2")
                        {
                            var labform = _schoolcontext.LabForm
                                .Where(a => a.No == insuranceTime_List.InsuranceTimeList[i].FormNo)
                                .SingleOrDefault();
                            _labFormService.early_retire(insuranceTime_List.InsuranceTimeList[i],labform.StudentID,labform.Boss);
                        }
                        else
                        {
                            result = _schoolcontext.LabInsuranceFee
                                .Where(a => a.FormNo == insuranceTime_List.InsuranceTimeList[i].FormNo && a.Origin == "-1")
                                .SingleOrDefault();
                            var lab_result = _schoolcontext.LabForm
                                .Where(a => a.No == insuranceTime_List.InsuranceTimeList[i].FormNo)
                                .SingleOrDefault();
                            var foreign = lab_result.IsForeign;
                            var WorkDay = 0;
                            if (lab_result.InsuranceType.Replace(" ", "") == "月保")
                            {
                                WorkDay = insuranceTime_List.InsuranceTimeList[i].EndTime.Day - insuranceTime_List.InsuranceTimeList[i].StartTime.Day + 1;
                            }
                            else
                            {
                                WorkDay = Int32.Parse(lab_result.PartDay.ToString());
                            }
                            string StoredProc = "exec Part_spInsuranceRank " +
                                "@p_TotalSalary = " + result.Salary + "," +
                                "@self_retire_percent = '" + result.SelfRetirePercent + "'," +
                                "@workday= '" + WorkDay + "'," +
                                "@foreign= '" + foreign + "'";
                            var temp = _schoolcontext.SpInsuranceFee.FromSqlRaw(StoredProc).ToList();
                            LabInsuranceFee insuranceFee = new LabInsuranceFee()
                            {
                                StudentID = result.StudentID,
                                LaborRank = result.LaborRank,
                                RetireRank = result.RetireRank,
                                SelfLabor = temp[0].SelfLabor,
                                DepLabor = temp[0].DepLabor,
                                SelfRetire = temp[0].SelfRetire,
                                DepRetire = temp[0].DepRetire,
                                LaborFund = temp[0].LaborFund,
                                Boss = result.Boss,
                                StartTime = insuranceTime_List.InsuranceTimeList[i].StartTime,
                                EndTime = insuranceTime_List.InsuranceTimeList[i].EndTime,
                                Salary = result.Salary,
                                SelfRetirePercent = result.SelfRetirePercent,
                                FormNo = result.FormNo,
                                Origin = insuranceTime_List.InsuranceTimeList[i].Origin
                            };
                            _schoolcontext.LabInsuranceFee.Add(insuranceFee);
                            _schoolcontext.SaveChanges();
                        }
                    }
                }
            }
        }

        [HttpPost("InsurancePayment")]
        public void InsurancePayment(InsurancePayment_List insurancePayment_List)
        {
            var result = _schoolcontext.LabPayment
                .Where(a => a.StudentID == insurancePayment_List.StudentID && a.Paid==false);
            if (result != null)
            {
                _schoolcontext.LabPayment.RemoveRange(result);
                _schoolcontext.SaveChanges();
            }
            for (int i = 0; i < insurancePayment_List.InsurancePaymentList.Count(); i++)
            {
                var temp = insurancePayment_List.InsurancePaymentList[i].Payment.Split(" ");
                LabPayment labPayment = new LabPayment()
                {
                    StudentID = insurancePayment_List.StudentID,
                    StartTime = insurancePayment_List.InsurancePaymentList[i].StartTime,
                    EndTime = insurancePayment_List.InsurancePaymentList[i].EndTime,
                    Payment = temp[0],
                    FormNo = temp[1]
                };
                _schoolcontext.LabPayment.Add(labPayment);
                _schoolcontext.SaveChanges();
            }
        }

        [HttpPost("Paid")]
        public void Paid(Paid_Info paid_Info)
        {
            var result = _schoolcontext.LabPayment
                .Where(a => a.StudentID == paid_Info.StudentID && a.StartTime == paid_Info.StartTime).SingleOrDefault();
            if(result != null)
            {
                result.Paid = true;
                _schoolcontext.LabPayment.Update(result);
                _schoolcontext.SaveChanges();
            }
        }

        [HttpPost("unPaid")]
        public void unPaid(Paid_Info paid_Info)
        {
            var result = _schoolcontext.LabPayment
                .Where(a => a.StudentID == paid_Info.StudentID && a.StartTime == paid_Info.StartTime).SingleOrDefault();
            if (result != null)
            {
                result.Paid = false;
                _schoolcontext.LabPayment.Update(result);
                _schoolcontext.SaveChanges();
            }
        }

        [HttpPatch("{No}")]
        public IActionResult Patch(int No, [FromBody] JsonPatchDocument labForm)
        {
            var update = _labFormService.Patch(No, labForm);
            if (update == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetOne), new { No = update.No }, update);
        }

        [HttpPut]
        public IActionResult Put([FromBody] LabFormPutDto labForm)
        {
            LabForm update = _labFormService.Put(labForm);
            return CreatedAtAction(nameof(GetOne), new { No = update.No }, update);
        }

        [HttpPut("manage")]
        public IActionResult ManagePut([FromBody] LabFormDto labForm)
        {
            _labFormService.ManagePut(labForm);
            return CreatedAtAction(nameof(GetOne), new { No = labForm.TempNo }, labForm);
        }

        [HttpPost("InsuranceFee")]
        public IEnumerable<SpInsuranceFee> InsuranceFee(SpInsuranceFeeDto spInsuranceFee)
        {
            var result = _labFormService.InsuranceFee(spInsuranceFee);
            return result;
        }
        [HttpPost("Studentinfo")]
        public async Task<ActionResult<IEnumerable<Sp_StudentInfo>>> Getstudentouput(Sp_StudentInfoDto Studentinfo)
        {
            string st_id = Studentinfo.Student_ID;
            string StoredProc = "exec sp_StudentInfo @p_Classno = '" + st_id + "'";
            return await _schoolcontext.Sp_StudentInfo.FromSqlRaw(StoredProc).ToListAsync();
        }
        [HttpPost("GetProjectBudget")]
        public async Task<ActionResult<IEnumerable<Sp_GetProjectBudget>>> GetprojectBudgetouput(Sp_GetProjectBudgetDto GetProjectBudget)
        {
            string card = GetProjectBudget.p_UserCard;
            string StoredProc = "exec Pur_spGetProjectBudget @p_UserCard = '" + card + "'";
            return await _schoolcontext.Sp_GetProjectBudget.FromSqlRaw(StoredProc).ToListAsync();
        }
        [HttpPost("GetDepBudget")]
        public async Task<ActionResult<IEnumerable<Sp_GetDepBudget>>> GetdepBudgetouput(Sp_GetDepBudgetDto GetDepBudget)
        {
            string card = GetDepBudget.p_UserCard;
            string StoredProc = "exec Pur_spGetDepBudget @p_UserCard = '" + card + "'";
            return await _schoolcontext.Sp_GetDepBudget.FromSqlRaw(StoredProc).ToListAsync();
        }
        [HttpPost("GetHiredDep")]
        public async Task<ActionResult<IEnumerable<Sp_GetHiredDep>>> GetHiredDepouput(Sp_GetHiredDepDTO GetHiredDep)
        {
            string card = GetHiredDep.p_UserCard;
            string StoredProc = "exec sp_DepHirer @p_UserCard = '" + card + "'";
            return await _schoolcontext.Sp_GetHiredDep.FromSqlRaw(StoredProc).ToListAsync();
        }
        [HttpPost("ExportTotxt_quit")]
        public IActionResult ExportTotxt_quit(export_excel_list_quit temp)
        {
            var writeRecords = temp.ExportExcelList;
            var writeConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
            MemoryStream ms = new MemoryStream();
            using (var writer = new StreamWriter(ms))
            using (var csv = new CsvWriter(writer, writeConfiguration))
            {
                csv.WriteRecords(writeRecords);
            }
            string base64 = Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
            return Content(base64);
        }
        [HttpPost("ExportTotxt_add")]
        public IActionResult ExportTotxt_add(export_excel_list_add temp)
        {
            var writeRecords = temp.ExportExcelList;
            var writeConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };
            MemoryStream ms = new MemoryStream();
            using (var writer = new StreamWriter(ms))
            using (var csv = new CsvWriter(writer, writeConfiguration))
            {
                csv.WriteRecords(writeRecords);
            }
            string base64 = Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length);
            return Content(base64);
        }
        [HttpPost("GetManagement_user")]
        public bool GetManagement(string user_no)
        {
            var result = _schoolcontext.Management_table
                .Where(a => a.User_No == user_no);
            var temp = result.ToList();
            Console.WriteLine(temp);
            if(result.Count() == 0)
            {
                return false;
            }
            return true;
        }
        
    }
}
