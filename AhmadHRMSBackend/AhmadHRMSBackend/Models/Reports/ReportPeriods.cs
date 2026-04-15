using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Reports
{
    [Table("ReportPeriods")]
    public class ReportPeriods
    {
        [Key]
        public int ReportPeriodId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReportPeriodName { get; set; }

        public bool IsDeleted { get; set; } = false;


        public ICollection<Reports> Reports { get; set; } = new List<Reports>();
    }
}
