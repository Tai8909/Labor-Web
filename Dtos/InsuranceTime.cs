﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Checklist.Dtos
{
    public partial class InsuranceTime
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FormNo { get; set; }
        public string Origin { get; set; }
    }
}
