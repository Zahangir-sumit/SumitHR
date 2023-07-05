
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.MVC.DHK.Models
{
    public class AttendanceSummary
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ComId { get; set; }

        [Required]
        public Guid EmpId { get; set; }

        [Required]
        public int DtYear { get; set; }

        [Required]
        public int DtMonth { get; set; }

        public int? Present { get; set; }

        public int? Late { get; set; }

        public int? Absent { get; set; }

        [ForeignKey("ComId")]
        public virtual Company Company { get; set; } = null!;
        [ForeignKey("EmpId")]
        public virtual Employee Employee { get; set; } = null!;
    }
}


