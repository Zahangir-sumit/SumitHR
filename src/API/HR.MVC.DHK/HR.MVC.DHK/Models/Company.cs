using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace HR.MVC.DHK.Models 
{
    public class Company
    {
        [Key]
        public Guid ComId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string ComName { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Basic { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Hrent { get; set; }

        [Required]
        [Range(0, 9999999999.99)]
        public decimal Medical { get; set; }

        [Required]
        public bool IsInactive { get; set; }

        public virtual ICollection<AttendanceSummary> AttendanceSummaries { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        public virtual ICollection<Designation> Designations { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Salary> Salaries { get; set; }

        public virtual ICollection<Shift> Shifts { get; set; }
    }
}



