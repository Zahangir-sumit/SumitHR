using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.MVC.DHK.Models
{
    public class Shift
    {
        public Guid ShiftId { get; set; }
        [Required]
        [StringLength(30)]
        public string ShiftName { get; set; }
        [Required]
        public TimeSpan ShiftIn { get; set; }
        [Required]
        public TimeSpan ShiftOut { get; set; }
        [Required]
        public TimeSpan ShiftLate { get; set; }

        [Required]
        public Guid ComId { get; set; }

        [ForeignKey("ComId")]
        public virtual Company Company { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
