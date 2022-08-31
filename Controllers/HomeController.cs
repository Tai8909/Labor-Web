using Checklist.Models;
using Checklist.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.HtmlConverter;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Checklist.Services;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Checklist.Parameters;

namespace Checklist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly LabFormService _labFormService;
        private readonly schoolContext _schoolcontext;

        [Obsolete]
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment, LabFormService labFormService, schoolContext schoolcontext)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _labFormService = labFormService;
            _schoolcontext = schoolcontext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Employment(string No)
        {
            var result = _labFormService.GetOne(No).ToList();
            if (result != null)
            {
                ViewData["No"] = No;
                ViewData["applyItem"] = result[0].ApplyItem;
                ViewData["hiredName"] = result[0].HiredName;
                ViewData["id"] = result[0].ID;
                ViewData["student_id"] = result[0].StudentID;
                if (result[0].School.Contains('/'))
                {
                    string[] arr = result[0].School.Split('/');
                    ViewData["SchoolName"] = arr[0];
                    ViewData["studentDep"] = arr[1];
                }
                else
                {
                    ViewData["studentDep"] = result[0].School;
                }
                ViewData["level"] = result[0].pclass;
                ViewData["hiredDep"] = result[0].HiredDep;
                ViewData["birthday"] = result[0].Birthday.ToString().Split(" ")[0];
                ViewData["workType"] = result[0].WorkType;
                ViewData["phone"] = result[0].Phone;
                ViewData["email"] = result[0].Email;
                ViewData["startTime"] = result[0].ActualStartTime == null ? result[0].StartTime.ToString().Split(" ")[0] : result[0].ActualStartTime.ToString().Split(" ")[0];
                ViewData["endTime"] = result[0].ActualEndTime.ToString().Split(" ")[0];
                string str = "";
                if (result[0].Class.Replace(" ", "") == "日薪")
                {
                    str = "日薪型，每日" + result[0].PartSalary + "元，";
                }
                else if (result[0].Class.Replace(" ", "") == "時薪")
                {
                    str = "時薪型，每小時" + result[0].PartSalary + "元，";
                }
                else
                {
                    str = "月薪型，";
                }
                if (result[0].InsuranceType != null)
                {
                    ViewData["insuranceType"] = result[0].InsuranceType;
                }
                str += "預估每月平均薪資為：" + result[0].Salary + "元";
                ViewData["salarystring"] = str;
                ViewData["selfRetirePercent"] = result[0].SelfRetirePercent + "%";
                string budget_no = result[0].Bugeno;
                string budget_name = result[0].Fundname;
                ViewData["bugeno"] = budget_no;
                ViewData["fundname"] = budget_name;
                string Budget_type = result[0].Budgettype;
                string ins_type = Budget_type == null ? null : Budget_type.Split(" ")[0];
                //ViewData["boss_email"] = result[0].
                ViewData["Boss_dep"] = result[0].Bossdep;
                if (ins_type == "1")//部門
                {
                    string StoredProc = "exec sp_getdepbudget @project_num = '" + budget_no + "',@fund_name = " + budget_name;
                    var dep_result = _schoolcontext.Sp_SearchDepBudget.FromSqlRaw(StoredProc).ToList();
                    if (dep_result != null)
                    {
                        int byear = Convert.ToInt32(dep_result[0].byear);
                        byear += 1911;
                        string budgetname = dep_result[0].bugname;
                        decimal source_type = dep_result[0].source_type;
                        string fund_name = dep_result[0].fund_name;
                        ViewData["bugename"] = budgetname;
                        string startstr = "自" + byear.ToString() + "/01/01起";
                        string endstr = "至" + byear.ToString() + "/12/31止";
                        ViewData["sourcetype"] = source_type;
                        ViewData["type"] = "部門經費";
                        ViewData["bugestarttime"] = startstr;
                        ViewData["bugeendtime"] = endstr;
                    }
                }
                else if (ins_type == "2")//計畫
                {
                    string StoredProc = "exec sp_getprojectbudget @project_num = '" + budget_no + "',@fund_name = " + budget_name;
                    var project_result = _schoolcontext.Sp_SearchProjectBudget.FromSqlRaw(StoredProc).ToList();
                    if (project_result != null)
                    {
                        string budgetname = project_result[0].bugname;
                        string start_year = project_result[0].start_time.ToString();

                        //char to datetime
                        string year = (Convert.ToInt32(start_year.Substring(0, 3)) + 1911).ToString();
                        var month = start_year.Substring(3, 2);
                        var date = start_year.Substring(5, 2);
                        start_year = year + '/' + month + '/' + date;
                        //char to datetime
                        string end_year = project_result[0].end_time.ToString();
                        year = (Convert.ToInt32(end_year.Substring(0, 3)) + 1911).ToString();
                        month = end_year.Substring(3, 2);
                        date = end_year.Substring(5, 2);
                        end_year = year + '/' + month + '/' + date;

                        decimal source_type = project_result[0].source_type;
                        string fund_name = project_result[0].fund_name;
                        ViewData["bugename"] = budgetname;
                        string startstr = "自" + start_year.Split(" ")[0] + "起";
                        string endstr = "至" + end_year.Split(" ")[0] + "止";
                        ViewData["type"] = "計畫經費";
                        ViewData["sourcetype"] = source_type;
                        ViewData["bugestarttime"] = startstr;
                        ViewData["bugeendtime"] = endstr;
                        ViewData["ProjNum"] = project_result[0].Proj_Num;
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Employment_show(string No)
        {
            var result = _labFormService.GetOne(No).ToList();
            if (result != null)
            {
                ViewData["No"] = No;
                ViewData["applyItem"] = result[0].ApplyItem;
                ViewData["hiredName"] = result[0].HiredName;
                ViewData["id"] = result[0].ID;
                ViewData["student_id"] = result[0].StudentID;
                if (result[0].School.Contains('/'))
                {
                    string[] arr = result[0].School.Split('/');
                    ViewData["SchoolName"] = arr[0];
                    ViewData["studentDep"] = arr[1];
                }
                else
                {
                    ViewData["studentDep"] = result[0].School;
                }
                ViewData["level"] = result[0].pclass;
                ViewData["hiredDep"] = result[0].HiredDep;
                ViewData["birthday"] = result[0].Birthday.ToString().Split(" ")[0];
                ViewData["workType"] = result[0].WorkType;
                ViewData["phone"] = result[0].Phone;
                ViewData["email"] = result[0].Email;
                ViewData["startTime"] = result[0].ActualStartTime == null ? result[0].StartTime.ToString().Split(" ")[0] : result[0].ActualStartTime.ToString().Split(" ")[0];
                ViewData["endTime"] = result[0].ActualEndTime.ToString().Split(" ")[0];
                string str = "";
                if (result[0].Class.Replace(" ", "") == "日薪")
                {
                    str = "日薪型，每日" + result[0].PartSalary + "元，";
                }
                else if (result[0].Class.Replace(" ", "") == "時薪")
                {
                    str = "時薪型，每小時" + result[0].PartSalary + "元，";
                }
                else
                {
                    str = "月薪型，";
                }
                if (result[0].InsuranceType != null)
                {
                    ViewData["insuranceType"] = result[0].InsuranceType;
                }
                str += "預估每月平均薪資為：" + result[0].Salary + "元";
                ViewData["salarystring"] = str;
                ViewData["selfRetirePercent"] = result[0].SelfRetirePercent + "%";
                string budget_no = result[0].Bugeno;
                string budget_name = result[0].Fundname;
                ViewData["bugeno"] = budget_no;
                ViewData["fundname"] = budget_name;
                string Budget_type = result[0].Budgettype;
                string ins_type = Budget_type == null ? null : Budget_type.Split(" ")[0];
                //ViewData["boss_email"] = result[0].
                ViewData["Boss_dep"] = result[0].Bossdep;
                if (ins_type == "1")//部門
                {
                    string StoredProc = "exec sp_getdepbudget @project_num = '" + budget_no + "',@fund_name = " + budget_name;
                    var dep_result = _schoolcontext.Sp_SearchDepBudget.FromSqlRaw(StoredProc).ToList();
                    if (dep_result != null)
                    {
                        int byear = Convert.ToInt32(dep_result[0].byear);
                        byear += 1911;
                        string budgetname = dep_result[0].bugname;
                        decimal source_type = dep_result[0].source_type;
                        string fund_name = dep_result[0].fund_name;
                        ViewData["bugename"] = budgetname;
                        string startstr = "自" + byear.ToString() + "/01/01起";
                        string endstr = "至" + byear.ToString() + "/12/31止";
                        ViewData["sourcetype"] = source_type;
                        ViewData["type"] = "部門經費";
                        ViewData["bugestarttime"] = startstr;
                        ViewData["bugeendtime"] = endstr;
                    }
                }
                else if (ins_type == "2")//計畫
                {
                    string StoredProc = "exec sp_getprojectbudget @project_num = '" + budget_no + "',@fund_name = " + budget_name;
                    var project_result = _schoolcontext.Sp_SearchProjectBudget.FromSqlRaw(StoredProc).ToList();
                    if (project_result != null)
                    {
                        string budgetname = project_result[0].bugname;
                        string start_year = project_result[0].start_time.ToString();

                        //char to datetime
                        string year = (Convert.ToInt32(start_year.Substring(0, 3)) + 1911).ToString();
                        var month = start_year.Substring(3, 2);
                        var date = start_year.Substring(5, 2);
                        start_year = year + '/' + month + '/' + date;
                        //char to datetime
                        string end_year = project_result[0].end_time.ToString();
                        year = (Convert.ToInt32(end_year.Substring(0, 3)) + 1911).ToString();
                        month = end_year.Substring(3, 2);
                        date = end_year.Substring(5, 2);
                        end_year = year + '/' + month + '/' + date;

                        decimal source_type = project_result[0].source_type;
                        string fund_name = project_result[0].fund_name;
                        ViewData["bugename"] = budgetname;
                        string startstr = "自" + start_year.Split(" ")[0] + "起";
                        string endstr = "至" + end_year.Split(" ")[0] + "止";
                        ViewData["type"] = "計畫經費";
                        ViewData["sourcetype"] = source_type;
                        ViewData["bugestarttime"] = startstr;
                        ViewData["bugeendtime"] = endstr;
                        ViewData["ProjNum"] = project_result[0].Proj_Num;
                    }
                }
                //取得表單list
                var temp = _labFormService.GetManage();
                var list_result = _labFormService.New_Get(No, temp).ToList();
                ViewData["list_result"] = list_result;
            }
            return View();
        }
        [HttpGet]
        public IActionResult Search()
        {
            LabFormSelectParameter labForm = new LabFormSelectParameter();
            string card = TempData["card"].ToString();
            if (!TempData.ContainsKey("card"))
            {
                return Redirect("https://localhost:44313/");
            }
            if (card.Length == 8)
            {
                labForm.StudentID = card;
            }
            else
            {
                if (TempData["ismanager"].ToString() == "false")
                {
                    labForm.BossID = card;
                }
            }
            var temp = _labFormService.GetSearch();
            var result = _labFormService.Get(labForm, temp).ToList();
            ViewData["resultlist"] = result;
            return View();
        }
        [HttpPost]
        public IActionResult Search(LabFormSelectParameter labForm)
        {
            ViewData["StudentID"] = labForm.StudentID;
            ViewData["TeacherID"] = labForm.BossID;
            ViewData["Bugeno"] = labForm.Bugeno;
            ViewData["FormStatus"] = labForm.FormStatus;
            ViewData["DateCheckMethod"] = labForm.DateCheckMethod;
            if (labForm.StartTime != null)
            {
                ViewData["StartTime"] = converdate((DateTime)labForm.StartTime);
            }
            if (labForm.EndTime != null)
            {
                ViewData["EndTime"] = converdate((DateTime)labForm.EndTime);
            }
            if (labForm.CheckDate != null)
            {
                ViewData["CheckTime"] = converdate((DateTime)labForm.CheckDate);
            }
            var temp = _labFormService.GetSearch();
            var result = _labFormService.Get(labForm, temp).ToList();
            ViewData["resultlist"] = result;
            return View();
        }
        private String converdate(DateTime temp)
        {
            string str = temp.ToString().Split(" ")[0];
            var str_cut = str.Split("/");
            String year = str_cut[0];
            string month = str_cut[1].PadLeft(2, '0');
            String day = str_cut[2].PadLeft(2, '0');
            string final_str = year + '-' + month + '-' + day;
            return final_str;
        }

        [HttpGet]
        public IActionResult Detail(string no)
        {
            var result = _labFormService.GetOne(no).ToList();
            if (result != null)
            {
                ViewData["No"] = no;
                ViewData["applyItem"] = result[0].ApplyItem;
                ViewData["hiredName"] = result[0].HiredName;
                ViewData["id"] = result[0].ID;
                ViewData["hiredDep"] = result[0].HiredDep;
                ViewData["birthday"] = result[0].Birthday.ToString().Split(" ")[0];
                ViewData["workType"] = result[0].WorkType;
                ViewData["phone"] = result[0].Phone;
                ViewData["email"] = result[0].Email;
                ViewData["startTime"] = result[0].ActualStartTime == null ? result[0].StartTime.ToString().Split(" ")[0] : result[0].ActualStartTime.ToString().Split(" ")[0];
                ViewData["endTime"] = result[0].ActualEndTime.ToString().Split(" ")[0];
                string str = "";
                if (result[0].Class.Replace(" ", "") == "日薪")
                {
                    str = "日薪型，每日" + result[0].PartSalary + "元，";
                }
                else if (result[0].Class.Replace(" ", "") == "時薪")
                {
                    str = "時薪型，每小時" + result[0].PartSalary + "元，";
                }
                else
                {
                    str = "月薪型，";
                }
                if (result[0].InsuranceType != null)
                {
                    ViewData["insuranceType"] = result[0].InsuranceType;
                }
                str += "預估每月平均薪資為：" + result[0].Salary + "元";
                ViewData["salarystring"] = str;
                ViewData["selfRetirePercent"] = result[0].SelfRetirePercent + "%";
                string budget_no = result[0].Bugeno;
                string budget_name = result[0].Fundname;
                ViewData["bugeno"] = budget_no;
                string Budget_type = result[0].Budgettype;
                string ins_type = Budget_type == null ? null : Budget_type.Split(" ")[0];
                ViewData["boss_email"] = result[0].Bossemail;
                ViewData["Boss_dep"] = result[0].Bossdep;
                if (ins_type == "1")//部門
                {
                    string StoredProc = "exec sp_getdepbudget @project_num = '" + budget_no + "',@fund_name = " + budget_name;
                    var dep_result = _schoolcontext.Sp_SearchDepBudget.FromSqlRaw(StoredProc).ToList();
                    if (dep_result != null)
                    {
                        int byear = Convert.ToInt32(dep_result[0].byear);
                        byear += 1911;
                        string budgetname = dep_result[0].bugname;
                        decimal source_type = dep_result[0].source_type;
                        string fund_name = dep_result[0].fund_name;
                        ViewData["bugename"] = budgetname;
                        string startstr = "自" + byear.ToString() + "/01/01起";
                        string endstr = "至" + byear.ToString() + "/12/31止";
                        ViewData["bugestarttime"] = startstr;
                        ViewData["bugeendtime"] = endstr;
                    }
                }
                else if (ins_type == "2")//計畫
                {
                    string StoredProc = "exec sp_getprojectbudget @project_num = '" + budget_no + "',@fund_name = " + budget_name;
                    var project_result = _schoolcontext.Sp_SearchProjectBudget.FromSqlRaw(StoredProc).ToList();
                    if (project_result != null)
                    {
                        string budgetname = project_result[0].bugname;
                        string start_year = project_result[0].start_time.ToString();

                        //char to datetime
                        string year = (Convert.ToInt32(start_year.Substring(0, 3)) + 1911).ToString();
                        var month = start_year.Substring(3, 2);
                        var date = start_year.Substring(5, 2);
                        start_year = year + '/' + month + '/' + date;
                        //char to datetime
                        string end_year = project_result[0].end_time.ToString();
                        year = (Convert.ToInt32(end_year.Substring(0, 3)) + 1911).ToString();
                        month = end_year.Substring(3, 2);
                        date = end_year.Substring(5, 2);
                        end_year = year + '/' + month + '/' + date;

                        decimal source_type = project_result[0].source_type;
                        string fund_name = project_result[0].fund_name;
                        ViewData["bugename"] = budgetname;
                        string startstr = "自" + start_year.Split(" ")[0] + "起";
                        string endstr = "至" + end_year.Split(" ")[0] + "止";
                        ViewData["bugestarttime"] = startstr;
                        ViewData["bugeendtime"] = endstr;
                        ViewData["ProjNum"] = project_result[0].Proj_Num;
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Audit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Alter()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update(String No)
        {

            return View();
        }

        [HttpGet]
        public IActionResult DownloadPDF(string No)
        {
            var result = _labFormService.GetOne(No).ToList();
            if (result != null)
            {
                ViewData["applyItem"] = result[0].ApplyItem;
                ViewData["hiredName"] = result[0].HiredName;
                ViewData["id"] = result[0].ID;
                ViewData["hiredDep"] = result[0].HiredDep;
                ViewData["birthday"] = result[0].Birthday.ToString().Split(" ")[0];
                ViewData["workType"] = result[0].WorkType;
                ViewData["phone"] = result[0].Phone;
                ViewData["email"] = result[0].Email;
                ViewData["startTime"] = result[0].ActualStartTime == null ? result[0].StartTime.ToString().Split(" ")[0] : result[0].ActualStartTime.ToString().Split(" ")[0];
                ViewData["endTime"] = result[0].ActualEndTime.ToString().Split(" ")[0];
                string str = "";
                if (result[0].Class.Replace(" ", "") == "日薪")
                {
                    str = "日薪型，每日" + result[0].PartSalary + "元，";
                }
                else if (result[0].Class.Replace(" ", "") == "時薪")
                {
                    str = "時薪型，每小時" + result[0].PartSalary + "元，";
                }
                else
                {
                    str = "月薪型，";
                }
                if (result[0].InsuranceType != null)
                {
                    ViewData["insuranceType"] = result[0].InsuranceType;
                }
                str += "預估每月平均薪資為：" + result[0].Salary + "元";
                ViewData["salarystring"] = str;
                ViewData["selfRetirePercent"] = result[0].SelfRetirePercent + "%";
                string budget_no = result[0].Bugeno;
                string budget_name = result[0].Fundname;
                ViewData["bugeno"] = budget_no;
                string Budget_type = result[0].Budgettype;
                string ins_type = Budget_type == null ? null : Budget_type.Split(" ")[0];
                ViewData["boss_email"] = result[0].Bossemail;
                ViewData["Boss_dep"] = result[0].Bossdep;
                if (ins_type == "1")//部門
                {
                    string StoredProc = "exec sp_getdepbudget @project_num = '" + budget_no + "',@fund_name = " + budget_name;
                    var dep_result = _schoolcontext.Sp_SearchDepBudget.FromSqlRaw(StoredProc).ToList();
                    if (dep_result != null)
                    {
                        int byear = Convert.ToInt32(dep_result[0].byear);
                        byear += 1911;
                        string budgetname = dep_result[0].bugname;
                        decimal source_type = dep_result[0].source_type;
                        string fund_name = dep_result[0].fund_name;
                        ViewData["bugename"] = budgetname;
                        string startstr = "自" + byear.ToString() + "/01/01起";
                        string endstr = "至" + byear.ToString() + "/12/31止";
                        ViewData["bugestarttime"] = startstr;
                        ViewData["bugeendtime"] = endstr;
                    }
                }
                else if (ins_type == "2")//計畫
                {
                    string StoredProc = "exec sp_getprojectbudget @project_num = '" + budget_no + "',@fund_name = " + budget_name;
                    var project_result = _schoolcontext.Sp_SearchProjectBudget.FromSqlRaw(StoredProc).ToList();
                    if (project_result != null)
                    {
                        string budgetname = project_result[0].bugname;
                        string start_year = project_result[0].start_time.ToString();

                        //char to datetime
                        string year = (Convert.ToInt32(start_year.Substring(0, 3)) + 1911).ToString();
                        var month = start_year.Substring(3, 2);
                        var date = start_year.Substring(5, 2);
                        start_year = year + '/' + month + '/' + date;
                        //char to datetime
                        string end_year = project_result[0].end_time.ToString();
                        year = (Convert.ToInt32(end_year.Substring(0, 3)) + 1911).ToString();
                        month = end_year.Substring(3, 2);
                        date = end_year.Substring(5, 2);
                        end_year = year + '/' + month + '/' + date;

                        decimal source_type = project_result[0].source_type;
                        string fund_name = project_result[0].fund_name;
                        ViewData["bugename"] = budgetname;
                        string startstr = "自" + start_year.Split(" ")[0] + "起";
                        string endstr = "至" + end_year.Split(" ")[0] + "止";
                        ViewData["bugestarttime"] = startstr;
                        ViewData["bugeendtime"] = endstr;
                        ViewData["ProjNum"] = project_result[0].Proj_Num;
                    }
                }


            }
            return View();
        }

        [HttpGet]
        public IActionResult InsuranceFee()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Output()
        {
            PaymentSelectParameter Payment = new PaymentSelectParameter();
            string card = TempData["card"].ToString();
            if (card.Length == 8)
            {
                Payment.StudentID = card;
            }
            else
            {
                if (TempData["ismanager"].ToString() == "false")
                {
                    Payment.BossID = card;
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Output(PaymentSelectParameter Payment)
        {
            ViewData["StudentID"] = Payment.StudentID;
            ViewData["TeacherID"] = Payment.BossID;
            ViewData["StartEndCheck"] = Payment.StartEndCheck;
            if (Payment.StartTime != null)
            {
                ViewData["StartTime"] = converdate((DateTime)Payment.StartTime);
            }
            if (Payment.EndTime != null)
            {
                ViewData["EndTime"] = converdate((DateTime)Payment.EndTime);
            }
            if (Payment.CheckDate != null)
            {
                ViewData["CheckTime"] = converdate((DateTime)Payment.CheckDate);
            }
            return View();
        }

        [HttpGet]
        public IActionResult InsuranceFeeManage()
        {
            return View();
        }

        public IActionResult InsuranceFeePayment()
        {
            PaymentSelectParameter Payment = new PaymentSelectParameter();
            string card = TempData["card"].ToString();
            if (card.Length == 8)
            {
                Payment.StudentID = card;
            }
            else
            {
                if (TempData["ismanager"].ToString() == "false")
                {
                    Payment.BossID = card;
                }
            }
            var result = _labFormService.InsuranceFeePayment(Payment).ToList();
            ViewData["resultlist"] = result;
            return View();
        }
        [HttpPost]
        public IActionResult InsuranceFeePayment(PaymentSelectParameter Payment)
        {
            ViewData["StudentID"] = Payment.StudentID;
            ViewData["TeacherID"] = Payment.BossID;
            ViewData["DateCheckMethod"] = Payment.DateCheckMethod;
            if (Payment.StartTime != null)
            {
                ViewData["StartTime"] = converdate((DateTime)Payment.StartTime);
            }
            if (Payment.EndTime != null)
            {
                ViewData["EndTime"] = converdate((DateTime)Payment.EndTime);
            }
            if (Payment.CheckDate != null)
            {
                ViewData["CheckTime"] = converdate((DateTime)Payment.CheckDate);
            }
            var result = _labFormService.InsuranceFeePayment(Payment).ToList();
            ViewData["resultlist"] = result;
            return View();
        }
        [HttpGet]
        public IActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        //HTML to pdf
        public IActionResult ExportToPDF(string No, string url)
        {
            //Initialize HTML to PDF converter with Blink rendering engine 
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.Blink);

            BlinkConverterSettings settings = new BlinkConverterSettings();

            //Set the BlinkBinaries folder path 
            settings.BlinkPath = Path.Combine(_hostingEnvironment.ContentRootPath, "BlinkBinariesWindows");

            //Assign Blink settings to HTML converter
            htmlConverter.ConverterSettings = settings;

            //Convert URL to PDF

            var URL = url + No;

            PdfDocument document = htmlConverter.Convert(URL);

            //Saving the PDF to the MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Download the PDF document in the browser
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Output.pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost("GetManagement_user")]
        public bool GetManagement(string user_no)
        {
            var result = _schoolcontext.Management_table
                .Where(a => a.User_No == user_no);
            var temp = result.ToList();
            Console.WriteLine(temp);
            if (result.Count() == 0)
            {
                return false;
            }
            return true;
        }

    }
}
