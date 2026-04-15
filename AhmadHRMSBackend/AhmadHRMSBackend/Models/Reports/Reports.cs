using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Reports
{
    [Table("Reports")]
    public class Reports
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReportName { get; set; }

        // 🔗 Foreign Keys
        [Required]
        public int ReportTypeId { get; set; }

        [Required]
        public int ReportPeriodId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int ReportStatusId { get; set; }

        public DateTime? GeneratedDate { get; set; }

        [StringLength(150)]
        public string? GeneratedBy { get; set; }

        [StringLength(50)]
        public string? FileSize { get; set; }

        [StringLength(20)]
        public string? Format { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public byte[]? FileData { get; set; }

        public bool IsDeleted { get; set; } = false;

        // 🔗 Navigation Properties

        [ForeignKey(nameof(ReportTypeId))]
        public ReportTypes ReportType { get; set; }


        [ForeignKey(nameof(ReportPeriodId))]
        public ReportPeriods ReportPeriod { get; set; }


        [ForeignKey(nameof(DepartmentId))]
        public Departments.Departments Departments { get; set; }


        [ForeignKey(nameof(ReportStatusId))]
        public ReportStatus ReportStatus { get; set; }
    }
}
