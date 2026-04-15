using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Reports
{
    [Table("ReportTypes")]
    public class ReportTypes
    {
        [Key]
        public int ReportTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReportTypeName { get; set; }


        public bool IsDeleted { get; set; } = false;

        public ICollection<Reports> Reports { get; set; } = new List<Reports>();
    }
}
