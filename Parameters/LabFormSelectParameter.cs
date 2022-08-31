using System;

namespace Checklist.Parameters
{
    public class LabFormSelectParameter
    {
        public string? FormStatus { get; set; }
        public string? StudentID { get; set; }
        public string? Bugeno { get; set; }
        public string? BossID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CheckDate { get; set; }
        public string DateCheckMethod { get; set; }
    }
}
