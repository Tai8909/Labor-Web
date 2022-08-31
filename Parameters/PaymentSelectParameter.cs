using System;

namespace Checklist.Parameters
{
    public class PaymentSelectParameter
    {
        public string? Boss { get; set; }
        public string? StudentID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CheckDate { get; set; }
        public string? BossID { get; set; }
        public string DateCheckMethod { get; set; }
        public string StartEndCheck { get; set; }
    }
}
