
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.MVC.DHK.Models
{
    public class Department
    {
        public Guid DeptId { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(30)]
        public string DeptName { get; set; }

        [Required]
        public Guid ComId { get; set; }

        [ForeignKey("ComId")]
        public virtual Company Company { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }

}

