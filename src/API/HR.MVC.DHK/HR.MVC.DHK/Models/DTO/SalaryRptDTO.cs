namespace HR.MVC.DHK.Models.DTO
{
    public class SalaryRptDTO
    {
        public Guid ComId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public Guid? DeptId { get; set; }
        public Guid? DesigId { get; set; }
        public bool IsPaid { get; set; }
    }
}
