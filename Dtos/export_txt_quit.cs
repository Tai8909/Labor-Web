using System;
using System.Text;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Dtos
{
    public partial class export_txt_add
    {
        private string num;
        private string f_num;
        private string in_num;
        private string in_check;
        private string coun;
        private string na;
        private string ID;
        private string birth;
        private string salary;
        private string sp_id;
        private string ins_id;
        private string ins_con;
        private string s;
        private string sub_id;
        private string h_percent;
        private string s_percent;
        private string q_date;

        [CsvHelper.Configuration.Attributes.Name("異動別")]
        public string change_num { get => num; set => num = value; }
        [CsvHelper.Configuration.Attributes.Name("格式別")]
        public string format_num { get => f_num; set => f_num = value; }
        [CsvHelper.Configuration.Attributes.Name("保險證號")]
        public string insurance_num { get => in_num; set => in_num = value; }
        [CsvHelper.Configuration.Attributes.Name("保險證號檢查碼")]
        public string insurance_check { get => in_check; set => in_check = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人外籍")]
        public string country { get => coun; set => coun = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人姓名")]
        public string name { get => na; set => na = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人身份證號")]
        public string id { get => ID; set => ID = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人出生日期")]
        public string birthday { get => birth; set => birth = value; }
        [CsvHelper.Configuration.Attributes.Name("月薪資總額")]
        public string mounth_salary { get => salary; set => salary = value; }
        [CsvHelper.Configuration.Attributes.Name("特殊身分別")]
        public string special_identity { get => sp_id; set => sp_id = value; }
        [CsvHelper.Configuration.Attributes.Name("勞基法特殊身分別")]
        public string ins_identity { get => ins_id; set => ins_id = value; }
        [CsvHelper.Configuration.Attributes.Name("僅參加「職保」條件")]
        public string ins_condition{ get => ins_con; set => ins_con = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人性別")]
        public string sex { get => s; set => s = value; }
        [CsvHelper.Configuration.Attributes.Name("提繳身分別")]
        public string sub_identity { get => sub_id; set => sub_id = value; }
        [CsvHelper.Configuration.Attributes.Name("雇主提繳率(%)")]
        public string hired_percent{ get => h_percent; set => h_percent = value; }
        [CsvHelper.Configuration.Attributes.Name("個人自願提繳率(%)")]
        public string self_percent { get => s_percent; set => s_percent = value; }
        [CsvHelper.Configuration.Attributes.Name("勞退提繳日期(與加保日不同才輸入)")]
        public string quit_date { get => q_date; set => q_date = value; }

    }
}
