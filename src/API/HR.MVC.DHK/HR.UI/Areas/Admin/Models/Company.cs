using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [ValidateNever]
        public virtual ICollection<AttendanceSummary>? AttendanceSummaries { get; set; }
        [ValidateNever]

        public virtual ICollection<Attendance>? Attendances { get; set; }
        [ValidateNever]

        public virtual ICollection<Department>? Departments { get; set; }
        [ValidateNever]

        public virtual ICollection<Designation>? Designations { get; set; }
        [ValidateNever]

        public virtual ICollection<Employee>? Employees { get; set; }
        [ValidateNever]

        public virtual ICollection<Salary>? Salaries { get; set; }
        [ValidateNever]

        public virtual ICollection<Shift>? Shifts { get; set; }
    }
}



