using Checklist.Dtos;
using Checklist.Models;
using Checklist.Parameters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checklist.Services
{
    public class LabFormService
    {
        private readonly schoolContext _schoolcontext;
        public LabFormService(schoolContext schoolcontext)
        {
            _schoolcontext = schoolcontext;
        }

        public IEnumerable<LabFormSearch> GetSearch()
        {
            var result = from a in _schoolcontext.LabForm
                         join b in _schoolcontext.LabInsuranceFee
                         on a.No equals b.FormNo
                         into data_A
                         from data_B in data_A.DefaultIfEmpty()
                         where data_B.Origin == "-1" || data_B.Origin == "0" || data_B.Origin == null
                         select new LabFormSearch()
                         {
                             No = a.No.ToString(),
                             StudentID = a.StudentID,
                             HiredName = a.HiredName,
                             Bugeno = a.Bugeno,
                             StartTime = data_B.StartTime == null ? a.StartTime : data_B.StartTime,
                             EndTime = data_B.EndTime == null ? a.EndTime : data_B.EndTime,
                             FormStatus = a.FormStatus,
                             Item = a.ApplyItem,
                             ID = a.ID,
                             Birthday = a.Birthday,
                             Is_foreign = a.IsForeign,
                             BossID = a.BossID
                         };
            result = result.OrderBy(a=>a.No);
            return result;
        }

        public IEnumerable<LabFormSearch> GetManage()
        {
            var result = from a in _schoolcontext.LabForm
                         join b in _schoolcontext.LabInsuranceFee
                         on a.No equals b.FormNo
                         into data_A
                         from data_B in data_A.DefaultIfEmpty()
                         where data_B.Origin != "-1"
                         select new LabFormSearch()
                         {
                             No = data_B.Origin == "0" || data_B.Origin == null ? a.No.ToString() : data_B.Origin,
                             StudentID = a.StudentID,
                             HiredName = a.HiredName,
                             Bugeno = a.Bugeno,
                             StartTime = data_B.StartTime == null ? a.StartTime : data_B.StartTime,
                             EndTime = data_B.EndTime == null ? a.EndTime : data_B.EndTime,
                             FormStatus = a.FormStatus,
                             Item = a.ApplyItem,
                             ID = a.ID,
                             Birthday = a.Birthday,
                             Is_foreign = a.IsForeign,
                             BossID = a.BossID,
                             SelfRetirePercent = a.SelfRetirePercent,
                             Salary = a.Salary
                         };
            return result;
        }

        public IEnumerable<LabFormSearch> Get([FromQuery] LabFormSelectParameter labForm, IEnumerable<LabFormSearch> result)
        {
            if (labForm.StudentID != null)
            {
                result = result.Where(a => a.StudentID.Replace(" ","") == labForm.StudentID);
            }
            if (labForm.Bugeno != null)
            {
                result = result.Where(a => a.Bugeno.Replace(" ", "") == labForm.Bugeno);
            }
            if(labForm.DateCheckMethod == "over")
            {
                if (labForm.StartTime != null)
                {
                    result = result.Where(a => a.EndTime >= labForm.StartTime);
                }
                if (labForm.EndTime != null)
                {
                    result = result.Where(a => a.StartTime <= labForm.EndTime);
                }
            }else
            {
                if (labForm.StartTime != null)
                {
                    result = result.Where(a => a.StartTime >= labForm.StartTime);
                }
                if (labForm.EndTime != null)
                {
                    result = result.Where(a => a.EndTime <= labForm.EndTime);
                }
            }
            if (labForm.FormStatus != null)
            {
                result = result.Where(a => a.FormStatus.Replace(" ", "") == labForm.FormStatus);
            }
            if (labForm.CheckDate != null)
            {
                result = result.Where(a => a.StartTime == labForm.CheckDate || a.EndTime == labForm.CheckDate);
            }
            if(labForm.StartTime == null && labForm.EndTime == null && labForm.CheckDate == null)
            {
                result = result.Where(a => a.EndTime >= DateTime.Now);
            }
            if (labForm.BossID != null)
            {
                result = result.Where(a => a.BossID == labForm.BossID);
            }
            return result;
        }
        public IEnumerable<LabFormSearch> New_Get(string No ,[FromQuery] IEnumerable<LabFormSearch> result)
        {
            if (No != null)
            {
                result = result.Where(a => a.No.Replace(" ", "").StartsWith(No));
            }
            return result;
        }

        public IEnumerable<SpInsuranceFee> InsuranceFee(SpInsuranceFeeDto spInsuranceFee)
        {
            int WorkDay = 0;
            if (spInsuranceFee.PartDay == "null")
            {
                int m = spInsuranceFee.EndTime.Month - spInsuranceFee.StartTime.Month;
                int temp;
                if (m == 0)
                {
                    temp = spInsuranceFee.EndTime.Day - spInsuranceFee.StartTime.Day + 1;
                    WorkDay = temp > 30 ? 30 : temp;
                }
                else if (m > 0)
                {
                    WorkDay += (m - 1) * 30;
                    temp = DateTime.Parse(spInsuranceFee.StartTime.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).Day - spInsuranceFee.StartTime.Day + 1;
                    WorkDay += spInsuranceFee.EndTime.Day > 30 ? 30 : spInsuranceFee.EndTime.Day;
                    WorkDay += temp > 30 ? 30 : temp;
                }
            }
            else
            {
                WorkDay = Int32.Parse(spInsuranceFee.PartDay);
            }
            string StoredProc = "exec Part_spInsuranceRank " +
                    "@p_TotalSalary = " + spInsuranceFee.Salary + "," +
                    "@self_retire_percent = '" + spInsuranceFee.SelfRetirePercent + "'," +
                    "@workday= '" + WorkDay + "'," +
                    "@foreign= '" + spInsuranceFee.Foreign + "'";
            var result = _schoolcontext.SpInsuranceFee.FromSqlRaw(StoredProc).ToList();
            return result;
        }

        public IEnumerable<PaymentSearch> InsuranceFeePayment([FromQuery] PaymentSelectParameter Payment)
        {
            var result = from x in (from a in _schoolcontext.LabForm
                             join b in _schoolcontext.LabInsuranceFee
                             on a.No equals b.FormNo
                             into data_A
                             from data_B in data_A.DefaultIfEmpty()
                             where data_B.Origin != "-1"
                             select new LabFormDetail()
                             {
                                 ApplyItem = a.ApplyItem,
                                 StartTime = data_B.StartTime,
                                 EndTime = data_B.EndTime,
                                 Boss = data_B.Boss,
                                 StudentID = data_B.StudentID,
                                 LaborRank = data_B.LaborRank,
                                 RetireRank = data_B.RetireRank,
                                 SelfLabor = data_B.SelfLabor,
                                 DepLabor = data_B.DepLabor,
                                 SelfRetire = data_B.SelfRetire,
                                 DepRetire = data_B.DepRetire,
                                 LaborFund = data_B.LaborFund,
                                 PartDay = a.PartDay,
                                 Salary = data_B.Salary,
                                 IsForeign = a.IsForeign,
                                 SelfRetirePercent = a.SelfRetirePercent,
                                 InsuranceType = a.InsuranceType,
                                 Origin = (data_B.Origin=="0")?a.No.ToString():data_B.Origin.Replace(" ",""),
                                 BossID = a.BossID,
                                 HiredName = a.HiredName,
                                 ID = a.ID,
                                 Birthday = a.Birthday
                             })
                         join y in _schoolcontext.LabPayment
                         on new { test1 = x.StudentID, test2 = x.Boss, test3 = x.Origin } equals new { test1 = y.StudentID, test2 = y.Payment,test3 = y.FormNo }
                         into data_C
                         from data_D in data_C.DefaultIfEmpty()
                         where x.Origin != null && x.StartTime <= data_D.StartTime && x.EndTime >= data_D.EndTime
                         orderby x.Boss, data_D.StartTime
                         select new PaymentSearch()
                         {
                             StartTime = x.StartTime,
                             EndTime = x.EndTime,
                             Boss = x.Boss,
                             StudentID = x.StudentID,
                             PaymentStartTime = data_D.StartTime,
                             PaymentEndTime = data_D.EndTime,
                             LaborRank = x.LaborRank,
                             RetireRank = x.RetireRank,
                             SelfLabor = x.SelfLabor,
                             DepLabor = x.DepLabor,
                             SelfRetire = x.SelfRetire,
                             DepRetire = x.DepRetire,
                             LaborFund = x.LaborFund,
                             PartDay = x.PartDay,
                             Salary = x.Salary,
                             IsForeign = x.IsForeign,
                             SelfRetirePercent = x.SelfRetirePercent,
                             InsuranceType = x.InsuranceType,
                             BossID = x.BossID,
                             FormNo = x.Origin,
                             ApplyItem = x.ApplyItem,
                             Hiredname = x.HiredName,
                             ID = x.ID,
                             Birthday = x.Birthday,
                             Paid = data_D.Paid
                         };
            var ttt = result.Where(a => a.BossID != null) ;
            if (Payment.StudentID != null)
            {
                result = result.Where(a => a.StudentID.Replace(" ", "") == Payment.StudentID);
            }
            if (Payment.Boss != null)
            {
                result = result.Where(a => a.Boss.Replace(" ", "") == Payment.Boss);
            }
            if(Payment.DateCheckMethod == "over")
            {
                if (Payment.StartTime != null)
                {
                    result = result.Where(a => a.EndTime >= Payment.StartTime);
                }
                if (Payment.EndTime != null)
                {
                    result = result.Where(a => a.StartTime <= Payment.EndTime);
                }
            }else
            {
                if (Payment.StartTime != null)
                {
                    result = result.Where(a => a.PaymentStartTime >= Payment.StartTime);
                }
                if (Payment.EndTime != null)
                {
                    result = result.Where(a => a.PaymentEndTime <= Payment.EndTime);
                }
            }
            if (Payment.CheckDate != null)
            {
                result = result.Where(a => a.PaymentStartTime == Payment.CheckDate || a.PaymentEndTime == Payment.CheckDate);
            }
            if (Payment.StartEndCheck == "add")
            {
                if (Payment.CheckDate != null)
                {
                    result = result.Where(a => a.PaymentStartTime == Payment.CheckDate);
                }
            }
            else
            {
                if (Payment.CheckDate != null)
                {
                    result = result.Where(a => a.PaymentEndTime == Payment.CheckDate);
                }
            }
            if (Payment.StartTime == null && Payment.EndTime == null && Payment.CheckDate == null)
            {
                result = result.Where(a => a.PaymentEndTime >= DateTime.Now);
            }
            if (Payment.BossID != null)
            {
                result = result.Where(a => a.BossID == Payment.BossID);
            }
            var temp = result.ToList();
            for (int i = 0; i < temp.Count(); i++)
            {
                if (temp[i].InsuranceType.Replace(" ", "") == "月保")
                {
                    SpInsuranceFeeDto spInsuranceFee = new SpInsuranceFeeDto
                    {
                        InsuranceType = temp[i].InsuranceType,
                        Salary = temp[i].Salary,
                        SelfRetirePercent = temp[i].SelfRetirePercent,
                        StartTime = temp[i].PaymentStartTime,
                        EndTime = temp[i].PaymentEndTime,
                        PartDay = temp[i].PartDay == null ? "null" : temp[i].PartDay.ToString(),
                        Foreign = temp[i].IsForeign
                    };
                    var spinsurancefee = InsuranceFee(spInsuranceFee).ToList();
                    temp[i].SelfLabor = spinsurancefee[0].SelfLabor;
                    temp[i].DepLabor = spinsurancefee[0].DepLabor;
                    temp[i].SelfRetire = spinsurancefee[0].SelfRetire;
                    temp[i].DepRetire = spinsurancefee[0].DepRetire;
                    temp[i].LaborFund = spinsurancefee[0].LaborFund;
                }
            }
            return temp;
        }

        public IEnumerable<LabInsuranceFee> GetStudentID()
        {
            var result = _schoolcontext.LabInsuranceFee
                .ToList()
                .GroupBy(a => a.StudentID)
                .Select(a => a.First());
            return result;
        }
        public IEnumerable<LabInsuranceFee> GetStudentID_Search(string StudentID)
        {
            var result = _schoolcontext.LabInsuranceFee
                .Where(a=>a.StudentID == StudentID)
                .ToList()
                .GroupBy(a => a.StudentID)
                .Select(a => a.First());
            return result;
        }
        public IEnumerable<PaymentTimeFee> ManageFee(string StudentID)
        {
            var result = from a in _schoolcontext.LabInsuranceFee
                         join b in _schoolcontext.LabPayment
                         on a.Origin=="0"?a.FormNo.ToString():a.Origin equals b.FormNo
                         into data_A
                         from data_B in data_A.DefaultIfEmpty()
                         where a.StudentID == StudentID
                         select new PaymentTimeFee()
                         {
                             StartTime = a.StartTime,
                             EndTime = a.EndTime,
                             Boss = a.Boss,
                             StudentID = a.StudentID,
                             LaborRank = a.LaborRank,
                             RetireRank = a.RetireRank,
                             SelfLabor = a.SelfLabor,
                             DepLabor = a.DepLabor,
                             SelfRetire = a.SelfRetire,
                             DepRetire = a.DepRetire,
                             LaborFund = a.LaborFund,
                             Salary = a.Salary,
                             SelfRetirePercent = a.SelfRetirePercent,
                             Origin = a.Origin,
                             FormNo = a.FormNo,
                             No = a.No,
                             Paid = data_B.Paid
                         };
            result = result.Distinct()
                           .OrderBy(a => a.FormNo)
                           .ThenBy(a => a.Origin)
                           .ThenByDescending(a =>a.Paid);
            //var result = _schoolcontext.LabInsuranceFee
            //    .Where(a => a.StudentID == StudentID)
            //    .OrderBy(a => a.FormNo)
            //    .ThenBy(a => a.Origin);
            return result;
        }
        public IEnumerable<PaymentTime> ManagePaymentTime(string StudentID)
        {
            var result = _schoolcontext.LabInsuranceFee
                .Where(a => a.StudentID == StudentID && a.Origin != "-1")
                .OrderBy(a => a.StartTime)
                .ToList();
            List<AllTime> temp = new List<AllTime>();
            for (int i = 0; i < result.Count(); i++)
            {
                var start = new AllTime
                {
                    Time = result[i].StartTime,
                    se = "s",
                    Boss = result[i].Boss.Replace(" ", ""),
                    No = (result[i].Origin.Replace(" ","") == "0")?result[i].FormNo.ToString():result[i].Origin.Replace(" ", "")
                };
                temp.Add(start);
                var end = new AllTime
                {
                    Time = result[i].EndTime,
                    se = "e",
                    Boss = result[i].Boss.Replace(" ", ""),
                    No = (result[i].Origin.Replace(" ", "") == "0") ? result[i].FormNo.ToString() : result[i].Origin.Replace(" ", "")
                };
                temp.Add(end);
            }
            temp = temp.OrderBy(a => a.Time)
                //.ThenBy(a => a.se)
                .ThenByDescending(a => a.se)
                .ToList();
            List<PaymentTime> payment_time = new List<PaymentTime>();
            DateTime? starttime = new DateTime();
            DateTime? endtime = new DateTime();
            string bo = "";
            int num = 0;
            string boss = "";
            for (int i = 0; i < temp.Count(); i++)
            {
                if(temp[i].se == "s")
                {
                    num++;
                    starttime = temp[i].Time;
                    boss += temp[i].Boss + " " + temp[i].No + ",";
                }
                if(temp[i].se == "e")
                {
                    num--;
                    endtime = temp[i].Time;
                }
                if(num==0||(i > 0 && temp[i - 1].se == "s" && temp[i].se == "e" && i<temp.Count()-1 && temp[i].Time!=temp[i+1].Time))
                {
                    var All_Payment = _schoolcontext.LabPayment
                        .Where(a => a.StudentID == StudentID && a.StartTime <= starttime && a.EndTime >= endtime)
                        .SingleOrDefault();
                    payment_time.Add(new PaymentTime
                    {
                        StartTime = starttime,
                        EndTime = endtime,
                        Boss = boss.Substring(0,boss.Length-1),
                        Payment = (All_Payment == null) ? "" : All_Payment.Payment.Replace(" ", "") + " " + All_Payment.FormNo.Replace(" ", ""),
                        Paid = (All_Payment == null) ? false : All_Payment.Paid
                    });
                    if (num != 0)
                    {
                        starttime = Convert.ToDateTime(temp[i].Time).AddDays(1);
                    }
                }
                else if(i<temp.Count()-1 && temp[i].se == temp[i+1].se && temp[i].Time != temp[i + 1].Time)
                {
                    if(temp[i].se == "s")
                    {
                        endtime = Convert.ToDateTime(temp[i+1].Time).AddDays(-1);
                        var All_Payment = _schoolcontext.LabPayment
                            .Where(a => a.StudentID == StudentID && a.StartTime <= starttime && a.EndTime >= endtime)
                            .SingleOrDefault();
                        payment_time.Add(new PaymentTime
                        {
                            StartTime = starttime,
                            EndTime = endtime,
                            Boss = boss.Substring(0, boss.Length - 1),
                            Payment = (All_Payment == null) ? "" : All_Payment.Payment.Replace(" ", "") + " " + All_Payment.FormNo.Replace(" ", ""),
                            Paid = (All_Payment == null) ? false : All_Payment.Paid
                        });
                    }
                    if(temp[i].se == "e")
                    {
                        var All_Payment = _schoolcontext.LabPayment
                            .Where(a => a.StudentID == StudentID && a.StartTime <= starttime && a.EndTime >= endtime)
                            .SingleOrDefault();
                        payment_time.Add(new PaymentTime
                        {
                            StartTime = starttime,
                            EndTime = endtime,
                            Boss = boss.Substring(0, boss.Length - 1),
                            Payment = (All_Payment == null) ? "" : All_Payment.Payment.Replace(" ", "") + " " + All_Payment.FormNo.Replace(" ", ""),
                            Paid = (All_Payment == null) ? false : All_Payment.Paid
                        });
                        starttime = Convert.ToDateTime(temp[i].Time).AddDays(1);
                    }
                }
                else if(i > 0 && num >1 && temp[i-1].se == "e" && temp[i].se == "s")
                {
                    var st = Convert.ToDateTime(temp[i - 1].Time).AddDays(1);
                    var ed = Convert.ToDateTime(temp[i].Time).AddDays(-1);
                    if (st < ed)
                    {
                        var b = boss.Replace(temp[i].Boss + " " + temp[i].No + ",","");
                        var All_Payment = _schoolcontext.LabPayment
                                .Where(a => a.StudentID == StudentID && a.StartTime <= st && a.EndTime >= ed)
                                .SingleOrDefault();
                        payment_time.Add(new PaymentTime
                        {
                            StartTime = st,
                            EndTime = ed,
                            Boss = b.Substring(0, b.Length - 1),
                            Payment = (All_Payment == null) ? "" : All_Payment.Payment.Replace(" ","") + " " + All_Payment.FormNo.Replace(" ", ""),
                            Paid = (All_Payment == null) ? false : All_Payment.Paid
                        });
                    }
                }
                if (temp[i].se == "e")
                {
                    if (i < temp.Count() - 1 && temp[i + 1].se == "e" && temp[i + 1].Time == temp[i].Time)
                    {
                        bo += temp[i].Boss + " " + temp[i].No + ",";
                    }
                    else
                    {
                        boss = boss.Replace(temp[i].Boss + " " + temp[i].No + ",", "");
                        if (bo != "")
                        {
                            var bosses = bo.Split(",");
                            for(int j = 0; j < bosses.Length-1; j++)
                            {
                                boss = boss.Replace(bosses[j] + ",", "");
                            }
                            bo = "";
                        }
                    }
                }
            }
            return payment_time;
        }

        public IQueryable<LabFormDetail> GetOne(string No)
        {
            var no1 = No.Split("-")[0];
            var no2 = Int32.Parse(No.Split("-")[0]);
            var result = from a in _schoolcontext.LabForm
                         join b in _schoolcontext.LabInsuranceFee
                         on a.No equals b.FormNo
                         into data_A
                         from data_B in data_A.DefaultIfEmpty()
                         where No == no1 ? a.No == no2 : data_B.Origin.Equals(No)
                         select new LabFormDetail()
                         {
                             ApplyItem = a.ApplyItem,
                             Class = a.Class,
                             HiredName = a.HiredName,
                             School = a.School,
                             InsuranceType = a.InsuranceType,
                             HiredDep = a.HiredDep,
                             Boss = a.Boss,
                             StartTime = a.StartTime,
                             EndTime = a.EndTime,
                             ReceiveTime = a.ReceiveTime,
                             No = No,
                             IsForeign = a.IsForeign,
                             Salary = a.Salary,
                             ID = a.ID,
                             Birthday = a.Birthday,
                             WorkType = a.WorkType,
                             Email = a.Email,
                             Phone = a.Phone,
                             FormStatus = a.FormStatus,
                             PartSalary = a.PartSalary,
                             PartDay = a.PartDay,
                             Disability = a.Disability,
                             Bossdep = a.Bossdep,
                             Bossemail = a.Bossemail,
                             Bugeno = a.Bugeno,
                             Budgettype = a.Budgettype,
                             Fundname = a.Fundname,
                             ReceivePerson = a.ReceivePerson,
                             RejectReason = a.RejectReason,
                             StudentID = a.StudentID,
                             SelfRetirePercent = a.SelfRetirePercent,
                             ActualStartTime = data_B.StartTime == null ? a.ActualStartTime : data_B.StartTime,
                             ActualEndTime = data_B.EndTime == null ? a.EndTime : data_B.EndTime,
                             LaborRank = data_B.LaborRank,
                             RetireRank = data_B.RetireRank,
                             SelfLabor = data_B.SelfLabor,
                             DepLabor = data_B.DepLabor,
                             SelfRetire = data_B.SelfRetire,
                             DepRetire = data_B.DepRetire,
                             LaborFund = data_B.LaborFund,
                             Origin = data_B.Origin,
                             pclass = a.pclass,
                             Is_Nuk = a.Is_Nuk,
                             BossID = a.BossID
                         };
            return result;
        }

        public IEnumerable<LabFormDetail> GetOneSearch(int No)
        {
            //var no1 = No.Split("-")[0];
            //var no2 = Int32.Parse(No.Split("-")[0]);
            var result = from a in _schoolcontext.LabForm
                         join b in _schoolcontext.LabInsuranceFee
                         on a.No equals b.FormNo
                         into data_A
                         from data_B in data_A.DefaultIfEmpty()
                         where a.No == No && data_B.Origin !="-1"
                         select new LabFormDetail()
                         {
                             ApplyItem = a.ApplyItem,
                             Class = a.Class,
                             HiredName = a.HiredName,
                             School = a.School,
                             InsuranceType = a.InsuranceType,
                             HiredDep = a.HiredDep,
                             Boss = a.Boss,
                             StartTime = a.StartTime,
                             EndTime = a.EndTime,
                             ReceiveTime = a.ReceiveTime,
                             No = No.ToString(),
                             IsForeign = a.IsForeign,
                             Salary = a.Salary,
                             ID = a.ID,
                             Birthday = a.Birthday,
                             WorkType = a.WorkType,
                             Email = a.Email,
                             Phone = a.Phone,
                             FormStatus = a.FormStatus,
                             PartSalary = a.PartSalary,
                             PartDay = a.PartDay,
                             Disability = a.Disability,
                             Bossdep = a.Bossdep,
                             Bossemail = a.Bossemail,
                             Bugeno = a.Bugeno,
                             Budgettype = a.Budgettype,
                             Fundname = a.Fundname,
                             ReceivePerson = a.ReceivePerson,
                             RejectReason = a.RejectReason,
                             StudentID = a.StudentID,
                             SelfRetirePercent = a.SelfRetirePercent,
                             ActualStartTime = data_B.StartTime == null ? a.ActualStartTime : data_B.StartTime,
                             ActualEndTime = data_B.EndTime == null ? a.EndTime : data_B.EndTime,
                             LaborRank = data_B.LaborRank,
                             RetireRank = data_B.RetireRank,
                             SelfLabor = data_B.SelfLabor,
                             DepLabor = data_B.DepLabor,
                             SelfRetire = data_B.SelfRetire,
                             DepRetire = data_B.DepRetire,
                             LaborFund = data_B.LaborFund,
                             Origin = data_B.Origin,
                             pclass = a.pclass,
                             Is_Nuk = a.Is_Nuk,
                             BossID = a.BossID
                         };
            return result;
        }
        public LabForm Post([FromBody] LabFormPostDto labForm)
        {
            LabForm insert = new LabForm();
            labForm.FormStatus = "審核中";
            _schoolcontext.LabForm.Add(insert).CurrentValues.SetValues(labForm);
            _schoolcontext.SaveChanges();
            return insert;
        }

        //labForm格式範例
        //[
        //    {
        //        "op": "replace",
        //        "path": "/formStatus",
        //        "value": "審核通過"
        //    }
        //]
        public LabForm Patch(int No, [FromBody] JsonPatchDocument labForm)
        {
            var update = _schoolcontext.LabForm
                .Where(a => a.No == No).SingleOrDefault();
            labForm.ApplyTo(update);
            _schoolcontext.SaveChanges();
            return update;

        }
        public LabForm Put([FromBody] LabFormPutDto labForm)
        {
            labForm.ReceiveTime = DateTime.Now;
            LabForm update = _schoolcontext.LabForm
                .Where(a => a.No == labForm.No).SingleOrDefault();
            _schoolcontext.LabForm.Update(update).CurrentValues.SetValues(labForm);
            _schoolcontext.SaveChanges();
            if (labForm.FormStatus == "審核通過")
            {
                LabInsuranceFee insuranceFee = new LabInsuranceFee()
                {
                    StudentID = update.StudentID,
                    LaborRank = labForm.LaborRank,
                    RetireRank = labForm.RetireRank,
                    SelfLabor = labForm.SelfLabor,
                    DepLabor = labForm.DepLabor,
                    SelfRetire = labForm.SelfRetire,
                    DepRetire = labForm.DepRetire,
                    LaborFund = labForm.LaborFund,
                    Boss = update.Boss,
                    StartTime = labForm.ActualStartTime,
                    EndTime = update.EndTime,
                    Salary = update.Salary,
                    SelfRetirePercent = update.SelfRetirePercent,
                    FormNo = update.No,
                    Origin = "0"
                };
                _schoolcontext.LabInsuranceFee.Add(insuranceFee);
                _schoolcontext.SaveChanges();
                if (update.EndTime.Month - Convert.ToDateTime(labForm.ActualStartTime).Month > 1)
                {
                    Broken_Month(update.No);
                }
            }
            return update;
        }

        public void Broken_Month(int FormNo)
        {
            var update = _schoolcontext.LabInsuranceFee
                        .Where(a=>a.FormNo == FormNo)
                        .SingleOrDefault();
            if (update != null)
            {
                DateTime LastDay = new DateTime(update.EndTime.AddMonths(1).Year, update.EndTime.AddMonths(1).Month, 1).AddDays(-1);
                Boolean start = false;
                Boolean end = false;
                DateTime stTime = new DateTime();
                DateTime edTime = new DateTime();
                int j = 0;
                DateTime StartTime = Convert.ToDateTime(update.StartTime);
                if (StartTime.Day != 1)
                {
                    start = true;
                    j++;
                }
                if(update.EndTime.Day != LastDay.Day)
                {
                    end = true;
                    j++;
                }
                if (start || end)
                {
                    update.Origin = "-1";
                    _schoolcontext.LabInsuranceFee.Update(update);
                    _schoolcontext.SaveChanges();
                    var temp = _schoolcontext.LabForm
                            .Where(a => a.No == FormNo)
                            .SingleOrDefault();
                    Boolean insert = false;
                    for(int i = 1; i < 4;i++)
                    {
                        if(i==1 && start)
                        {
                            insert = true;
                            stTime = StartTime;
                            edTime = new DateTime(StartTime.AddMonths(1).Year, StartTime.AddMonths(1).Month, 1).AddDays(-1);
                        }
                        if(i==2 && update.EndTime.Month- StartTime.Month >= j)
                        {
                            insert = true;
                            stTime = start ? new DateTime(StartTime.AddMonths(1).Year, StartTime.AddMonths(1).Month, 1) : StartTime;
                            edTime = end ? new DateTime(update.EndTime.Year, update.EndTime.Month, 1).AddDays(-1): update.EndTime;
                        }
                        if(i==3 && end)
                        {
                            insert=true;
                            edTime = update.EndTime;
                            stTime = new DateTime(edTime.Year, edTime.Month, 1);
                        }
                        if (insert)
                        {
                            SpInsuranceFeeDto spInsuranceFee = new SpInsuranceFeeDto()
                            {
                                Salary = update.Salary,
                                SelfRetirePercent = update.SelfRetirePercent,
                                StartTime = stTime,
                                EndTime = edTime,
                                PartDay = (temp.PartDay==null) ? "null" : temp.PartDay.ToString(),
                                Foreign = temp.IsForeign,
                                InsuranceType = temp.InsuranceType
                            };
                            var result = InsuranceFee(spInsuranceFee).SingleOrDefault();
                            LabInsuranceFee insuranceFee = new LabInsuranceFee()
                            {
                                StudentID = update.StudentID,
                                LaborRank = result.LaborRank,
                                RetireRank = result.RetireRank,
                                SelfLabor = result.SelfLabor,
                                DepLabor = result.DepLabor,
                                SelfRetire = result.SelfRetire,
                                DepRetire = result.DepRetire,
                                LaborFund = result.LaborFund,
                                Boss = update.Boss,
                                StartTime = stTime,
                                EndTime = edTime,
                                Salary = update.Salary,
                                SelfRetirePercent = update.SelfRetirePercent,
                                FormNo = FormNo,
                                Origin = FormNo + "o" + i
                            };
                            _schoolcontext.LabInsuranceFee.Add(insuranceFee);
                            _schoolcontext.SaveChanges();
                            insert = false;
                        }
                    }
                }
            }
        }

        public void ManagePut([FromBody] LabFormDto labForm)
        {
            var tempNo = labForm.TempNo.Split("-");
            LabForm update = _schoolcontext.LabForm
                .Where(a => a.No == Int32.Parse(tempNo[0])).SingleOrDefault();
            if (labForm.ReceivePerson == "")
            {
                labForm.ReceivePerson = null;
            }
            if(labForm.RejectReason == "")
            {
                labForm.RejectReason = null;
            }
            _schoolcontext.LabForm.Update(update).CurrentValues.SetValues(labForm);
            _schoolcontext.SaveChanges();
            if (labForm.FormStatus == "審核通過")
            {
                LabInsuranceFee insuranceFee = new LabInsuranceFee();
                if(tempNo.Length == 1)
                {
                    insuranceFee = _schoolcontext.LabInsuranceFee
                        .Where(a => a.FormNo == Int32.Parse(labForm.TempNo)).SingleOrDefault();
                    if(insuranceFee != null)
                    {
                        insuranceFee.Origin = "0";
                    }
                    else
                    {
                        insuranceFee=new LabInsuranceFee();
                        insuranceFee.Origin = "0";
                    }
                }
                else
                {
                    insuranceFee = _schoolcontext.LabInsuranceFee
                        .Where(a => a.Origin == labForm.TempNo).SingleOrDefault();
                    insuranceFee.Origin = labForm.TempNo;
                }
                //if (insuranceFee == null)
                //{
                //    insuranceFee = new LabInsuranceFee();
                //}
                var salary = update.PartSalary;
                LabInsuranceFee te = insuranceFee;
                if (update.Class.Replace(" ", "") != "月薪")
                {
                    if(update.Class.Replace(" ", "") == "時薪")
                    {
                        salary = salary * 8;
                    }
                    salary = salary * update.PartDay;
                }
                insuranceFee.StudentID = update.StudentID;
                insuranceFee.LaborRank = labForm.LaborRank;
                insuranceFee.RetireRank = labForm.RetireRank;
                insuranceFee.SelfLabor = labForm.SelfLabor;
                insuranceFee.DepLabor = labForm.DepLabor;
                insuranceFee.SelfRetire = labForm.SelfRetire;
                insuranceFee.DepRetire = labForm.DepRetire;
                insuranceFee.LaborFund = labForm.LaborFund;
                insuranceFee.Boss = update.Boss;
                insuranceFee.StartTime = labForm.ActualStartTime;
                insuranceFee.EndTime = update.EndTime;
                insuranceFee.Salary = update.Class.Replace(" ", "") == "月薪" ? update.Salary : Int32.Parse(salary.ToString());
                insuranceFee.SelfRetirePercent = update.SelfRetirePercent;
                insuranceFee.FormNo = update.No;

                _schoolcontext.LabInsuranceFee.Update(insuranceFee);
                _schoolcontext.SaveChanges();
                if(update.EndTime.Month - Convert.ToDateTime(labForm.ActualStartTime).Month > 1 && insuranceFee.Origin == "0")
                {
                    Broken_Month(update.No);
                }
            }
        }

        public LabInsuranceFee Alter_Fee(LabForm labForm,LabInsuranceFee labInsuranceFee)
        {
            SpInsuranceFeeDto spInsuranceFee = new SpInsuranceFeeDto()
            {
                Salary = labForm.Salary,
                SelfRetirePercent = labForm.SelfRetirePercent,
                StartTime = Convert.ToDateTime(labInsuranceFee.StartTime),
                EndTime = labInsuranceFee.EndTime,
                PartDay = (labForm.PartDay == null) ? "null" : labForm.PartDay.ToString(),
                Foreign = labForm.IsForeign,
                InsuranceType = labForm.InsuranceType
            };
            var result = InsuranceFee(spInsuranceFee).SingleOrDefault();
            labInsuranceFee.LaborRank = result.LaborRank;
            labInsuranceFee.RetireRank = result.RetireRank;
            labInsuranceFee.SelfLabor = result.SelfLabor;
            labInsuranceFee.DepLabor = result.DepLabor;
            labInsuranceFee.SelfRetire = result.SelfRetire;
            labInsuranceFee.DepRetire = result.DepRetire;
            labInsuranceFee.LaborFund = result.LaborFund;
            return labInsuranceFee;
        }

        public void early_retire(InsuranceTime insuranceTime,string studentid,string boss)
        {
            //刪除付款人
            var payment_cancel = _schoolcontext.LabPayment
                .Where(a => a.StudentID == studentid && a.Payment == boss)
                .ToList();
            for(int i = 0; i < payment_cancel.Count(); i++)
            {
                ;
            }
            //刪除開始時間超過離職時間的資料
            var exceedendtime = _schoolcontext.LabInsuranceFee
                .Where(a => a.FormNo == insuranceTime.FormNo && a.StartTime > insuranceTime.EndTime);
            if (exceedendtime != null)
            {
                _schoolcontext.LabInsuranceFee.RemoveRange(exceedendtime);
                _schoolcontext.SaveChanges();
            }
            //變更原始保單LabForm、LabInsuranceFee的資料
            var alter_ins = _schoolcontext.LabInsuranceFee
                .Where(a => a.FormNo == insuranceTime.FormNo)
                .OrderBy(a => a.StartTime)
                .ThenByDescending(a => a.EndTime)
                .ToList();
            alter_ins[0].EndTime = insuranceTime.EndTime;
            alter_ins[0].Origin = "-2";

            var alter_form = _schoolcontext.LabForm
                .Where(a => a.No == insuranceTime.FormNo)
                .SingleOrDefault();
            alter_form.EndTime = insuranceTime.EndTime;
            int ins_num = alter_ins.Count();
            if (ins_num == 2||(ins_num == 3 && alter_ins[2].Origin.Contains("o2-1")))
            {
                alter_ins[0].StartTime = alter_ins[ins_num - 1].StartTime;
                alter_form.StartTime = Convert.ToDateTime(alter_ins[ins_num - 1].StartTime);
            }
            alter_ins[0] = Alter_Fee(alter_form, alter_ins[0]);
            _schoolcontext.LabInsuranceFee.Update(alter_ins[0]);
            _schoolcontext.LabForm.Update(alter_form);
            _schoolcontext.SaveChanges();

            //處理剩餘資料
            var remaining = _schoolcontext.LabInsuranceFee
                .Where(a => a.FormNo == insuranceTime.FormNo && a.EndTime > insuranceTime.EndTime)
                .OrderBy(a => a.StartTime)
                .ThenByDescending(a => a.EndTime)
                .ToList();
            if (remaining.Count() == 1)
            {
                if (remaining[0].Origin.Contains("-1") || remaining[0].Origin.Contains("o1"))
                {
                    _schoolcontext.LabInsuranceFee.Remove(remaining[0]);
                    _schoolcontext.SaveChanges();
                }
                else if (remaining[0].Origin.Contains("o2"))
                {
                    if (insuranceTime.EndTime.Month - Convert.ToDateTime(remaining[0].StartTime).Month > 0)
                    {
                        remaining[0].EndTime = new DateTime(Convert.ToDateTime(remaining[0].StartTime).Year, Convert.ToDateTime(remaining[0].StartTime).Month, 1).AddDays(-1);
                        remaining[0] = Alter_Fee(alter_form, remaining[0]);
                        _schoolcontext.LabInsuranceFee.Update(remaining[0]);
                        _schoolcontext.SaveChanges();
                    }
                    remaining[0].StartTime = new DateTime(insuranceTime.EndTime.Year, insuranceTime.EndTime.Month, 1);
                    remaining[0].EndTime = insuranceTime.EndTime;
                    remaining[0].Origin = remaining[0].Origin.Replace("2", "3");
                    remaining[0] = Alter_Fee(alter_form, remaining[0]);
                    _schoolcontext.LabInsuranceFee.Add(remaining[0]);
                    _schoolcontext.SaveChanges();
                }
                else
                {
                    remaining[0].EndTime = insuranceTime.EndTime;
                    remaining[0] = Alter_Fee(alter_form, remaining[0]);
                    _schoolcontext.LabInsuranceFee.Update(remaining[0]);
                    _schoolcontext.SaveChanges();
                }
            }
            else if (remaining.Count() == 2)
            {
                int num = Int32.Parse(remaining[1].Origin.Split("-")[1]);
                if (remaining[1].Origin.Contains("o2-1"))
                {
                    if(ins_num == 3)
                    {
                        _schoolcontext.LabInsuranceFee.RemoveRange(remaining);
                        _schoolcontext.SaveChanges();
                    }
                    else
                    {
                        remaining[0].Origin = remaining[1].Origin.Replace("2", "3");
                        remaining[0].StartTime = remaining[1].StartTime;
                        remaining[0].EndTime = insuranceTime.EndTime;
                        remaining[0] = Alter_Fee(alter_form, remaining[0]);
                        _schoolcontext.Update(remaining[0]);
                        _schoolcontext.Remove(remaining[1]);
                        _schoolcontext.SaveChanges();
                    }
                }
                else
                {
                    remaining[1].Origin = remaining[0].Origin.Replace("2", "3");
                    remaining[0].EndTime = new DateTime(remaining[1].EndTime.Year, remaining[1].EndTime.Month, 1).AddDays(-1);
                    remaining[1].EndTime = insuranceTime.EndTime;
                    remaining[0] = Alter_Fee(alter_form, remaining[0]);
                    remaining[1] = Alter_Fee(alter_form, remaining[1]);
                    _schoolcontext.UpdateRange(remaining);
                    if (remaining[1].Origin.Contains("o2-2"))
                    {
                        _schoolcontext.Remove(alter_ins[ins_num - 2]);
                    }
                    _schoolcontext.SaveChanges();
                }
            }
        }
    }
}