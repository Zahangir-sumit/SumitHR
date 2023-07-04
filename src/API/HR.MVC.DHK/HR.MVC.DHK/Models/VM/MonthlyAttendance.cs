using Microsoft.EntityFrameworkCore;

namespace HR.MVC.DHK.Models.VM
{

    [Keyless]
    public class MonthlyAttendance
    {
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }
        public int dtYear { get; set; }
        public int dtMonth { get; set; }
        public int Present { get; set; }
        public int Late { get; set; }
        public int Absent { get; set; }
    }
}
