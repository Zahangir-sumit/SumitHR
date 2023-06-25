namespace HR.MVC.DHK.Models.VM
{
    public class SalarySummaryReport
    {
        public long Id { get; set; }
        public string ComName { get; set; }
        public string DeptName { get; set; }
        public int dtYear { get; set; }
        public int dtMonth { get; set; }
        public decimal GrossPayment { get; set; }
        public decimal AbsentAmount { get; set; }
        public decimal NetPayment { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DuePayment { get; set; }
    }
}
