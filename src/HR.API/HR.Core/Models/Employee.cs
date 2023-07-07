
using HR.MVC.DHK.Models.IEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.MVC.DHK.Models
{
    public class Employee 
    {

        public Guid EmpId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string EmpCode { get; set; }

        [Required]
        [StringLength(50)]
        public string EmpName { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        public DateTime dtJoin { get; set; }

        [Required]
        [Range(10000, double.MaxValue)]
        public decimal Gross { get; set; }

        [Range(0, 9999999999.99)]
        public decimal? Basic { get; set; }

        [Range(0, 9999999999.99)]
        public decimal? HRent { get; set; }

        [Range(0, 9999999999.99)]
        public decimal? Medical { get; set; }

        [Range(0, 9999999999.99)]
        public decimal? Others { get; set; }

        [Required]
        public Guid ComId { get; set; }

        [Required]
        public Guid DeptId { get; set; }

        [Required]
        public Guid DesigId { get; set; }

        [Required]
        public Guid ShiftId { get; set; }

        [ForeignKey("ComId")]
        public Company? Company { get; set; }

        [ForeignKey("DeptId")]
        public Department? Department { get; set; }

        [ForeignKey("DesigId")]
        public Designation? Designation { get; set; }

        [ForeignKey("ShiftId")]
        public Shift? Shift { get; set; }

        public virtual ICollection<AttendanceSummary>? AttendanceSummaries { get; set; } 
        public virtual ICollection<Attendance>? Attendances { get; set; } 
        public virtual ICollection<Salary>? Salaries { get; set; } 
    }
}


