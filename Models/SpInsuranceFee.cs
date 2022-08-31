using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Checklist.ValidationAttributes;
using System.Linq;
using System.Threading.Tasks;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Models
{
    public partial class SpInsuranceFee
    {

        //勞保級距
        public decimal LaborRank { get; set; }

        //勞退級距
        public decimal RetireRank { get; set; }

        //勞保自提(本國人)
        public double SelfLabor { get; set; }

        //勞保公提(本國人)
        public double DepLabor { get; set; }

        //勞退自提(本國人)
        public double SelfRetire { get; set; }

        //勞退公提(本國人)
        public double DepRetire { get; set; }

        //工資墊償金公提(本國人)
        public double LaborFund { get; set; }
    }
}
