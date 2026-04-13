using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Performance
{
    [Table("Performance")]
    public class Performance
    {
        [Key]
        public int PerformanceID { get; set; }

        // Foreign Key
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int PeriodId { get; set; }

        [Required]
        public decimal Rating { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        [Required]
        public DateTime NextReview { get; set; }


        public bool IsDeleted { get; set; } = false;

        // 🔗 Navigation
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeList.EmployeeList Employee { get; set; }

        [ForeignKey(nameof(PeriodId))]
        public PerformancePeriod PerformancePeriod { get; set; }

        public ICollection<PerformanceGoal> PerformanceGoal { get; set; } = new List<PerformanceGoal>();

        public ICollection<PerformanceAchievement> PerformanceAcheivement { get; set; } = new List<PerformanceAchievement>();
    }
}
