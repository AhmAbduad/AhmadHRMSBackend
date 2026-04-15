using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Reports
{
    [Table("ReportStatus")]
    public class ReportStatus
    {
        [Key]
        public int ReportStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReportStatusName { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Reports> Reports { get; set; } = new List<Reports>();
    }
}
