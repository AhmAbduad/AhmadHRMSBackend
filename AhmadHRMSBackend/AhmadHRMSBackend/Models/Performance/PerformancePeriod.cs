using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Performance
{
    [Table("PerformancePeriod")]
    public class PerformancePeriod
    {
        [Key]
        public int PerformancePeriodId { get; set; }

        [Required]
        [StringLength(50)]
        public string PeriodName { get; set; } // Q1 2024

        public bool IsDeleted { get; set; } = false;

        public ICollection<Performance> Performances { get; set; } = new List<Performance>();
    }
}
