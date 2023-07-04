namespace HR.MVC.DHK.Models.DTO
{
    public class AttendanceParameters
    {
        public Guid ComId { get; set; }
        public DateTime Date { get; set; }
        public string AttendanceType { get; set; }
        public string AttStatus { get; set; }
        public Guid? DepartmentID { get; set; }
        public Guid? EmpId { get; set; }
    }
}
