using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Performance
{
    [Table("PerformanceGoal")]
    public class PerformanceGoal
    {
        [Key]
        public int PerformanceGoalId { get; set; }

        [Required]
        public int PerformanceId { get; set; }

        [Required]
        [StringLength(300)]
        public string GoalText { get; set; }

        public bool IsDeleted { get; set; } = false;

        // 🔗 Navigation
        [ForeignKey(nameof(PerformanceId))]
        public Performance Performance { get; set; }

    }
}
