namespace HR.MVC.DHK.Models.VM
{
    public class SalaryReport
    {
        public long Id { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }
        public string DesigName { get; set; }
        public int dtYear { get; set; }
        public int dtMonth { get; set; }
        public decimal Gross { get; set; }
        public decimal Basic { get; set; }
        public decimal Hrent { get; set; }
        public decimal Medical { get; set; }
        public decimal AbsentAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public bool IsPaid { get; set; }
    }
}
