
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.MVC.DHK.Models
{
    public class Salary
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
        [Required]
        public decimal Gross { get; set; }
        [Range(0, 9999999999.99)]
        public decimal Basic { get; set; }
        [Range(0, 9999999999.99)]
        public decimal Hrent { get; set; }
        [Range(0, 9999999999.99)]
        public decimal Medical { get; set; }
        [Range(0, 9999999999.99)]
        public decimal AbsentAmount { get; set; }
        [Range(0, 9999999999.99)]
        public decimal PayableAmount { get; set; }

        public bool IsPaid { get; set; } = false;
        [Range(0, 9999999999.99)]
        public decimal PaidAmount { get; set; }

        [ForeignKey("ComId")]
        public virtual Company? Company { get; set; } = null!;

        [ForeignKey("EmpId")]
        public virtual Employee? Employee { get; set; } = null!;
    }
}


