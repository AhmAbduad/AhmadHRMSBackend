using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Performance
{
    [Table("PerformanceAchievement")]
    public class PerformanceAchievement
    {
        [Key]
        public int AchievementId { get; set; }

        [Required]
        public int PerformanceId { get; set; }

        [Required]
        [StringLength(500)]
        public string AchievementText { get; set; }

        public bool IsDeleted { get; set; } = false;

        // 🔗 Navigation
        [ForeignKey(nameof(PerformanceId))]
        public Performance Performance { get; set; }
    }
}
