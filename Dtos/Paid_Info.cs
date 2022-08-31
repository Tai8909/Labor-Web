using Checklist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Checklist.Dtos
{
    public class Paid_Info
    {
        public string StudentID { get; set; }
        public DateTime StartTime { get; set; }
    }
}
