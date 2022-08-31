using System;
using System.Text;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Dtos
{
    public partial class export_txt_quit
    {
        private string num;
        private string in_num;
        private string in_check;
        private string coun;
        private string na;
        private string ID;
        private string foreign_ID;
        private string birth;

        [CsvHelper.Configuration.Attributes.Name("異動別")]
        public string change_num { get => num; set => num = value; }
        [CsvHelper.Configuration.Attributes.Name("勞工保險證號")]
        public string insurance_num { get => in_num; set => in_num = value; }
        [CsvHelper.Configuration.Attributes.Name("勞工保險證號檢查碼")]
        public string insurance_check { get => in_check; set => in_check = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人外籍")]
        public string country { get => coun; set => coun = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人姓名（外籍含全名）")]
        public string name { get => na; set => na = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人身份證號(居留證統一證號或護照號碼)")]
        public string id { get => ID; set => ID = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人身分證號")]
        public string foreign_id { get => foreign_ID; set => foreign_ID = value; }
        [CsvHelper.Configuration.Attributes.Name("被保險人出生日期")]
        public string birthday { get => birth; set => birth = value; }
    }
}
