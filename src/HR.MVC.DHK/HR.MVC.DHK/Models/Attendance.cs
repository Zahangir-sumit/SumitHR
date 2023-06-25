
using System.ComponentModel.DataAnnotations;

namespace HR.MVC.DHK.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ComId { get; set; }
        [Required]
        public Guid EmpId { get; set; }
        [Required]
        public DateTime DtDate { get; set; }

        public char? AttStatus { get; set; }
        [Required]
        public TimeSpan? InTime { get; set; }
        [Required]
        public TimeSpan? OutTime { get; set; }

        public virtual Company Company { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;
    }
}


