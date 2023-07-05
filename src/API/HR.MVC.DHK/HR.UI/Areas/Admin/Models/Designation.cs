
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.MVC.DHK.Models
{
    public class Designation
    {
        public Guid DesigId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(30)]
        public string DesigName { get; set; } = null!;

        [Required]
        public Guid ComId { get; set; }

        [ForeignKey("ComId")]
        public virtual Company Company { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }

}

