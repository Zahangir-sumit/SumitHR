using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Models.VM
{
    [Keyless]
    public class DailyAttendance
    {
        public Guid EmpId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }
        public DateTime dtDate { get; set; }
        public string AttStatus { get; set; }
        public TimeSpan? InTime { get; set; }
        public TimeSpan? OutTime { get; set; }
    }
}
