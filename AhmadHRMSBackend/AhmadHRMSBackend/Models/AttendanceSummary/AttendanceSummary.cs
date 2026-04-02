using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.AttendanceSummary
{

    [Table("AttendanceSummary")]
    public class AttendanceSummary
    {
        [Key]
        public int AttendanceSummaryId { get; set; }


        [Required]
        public int Present { get; set; }

        [Required]
        public int Absent { get; set; }

        [Required]
        public int Late { get; set; }

        [Required]
        public int Leave { get; set; }

        public bool IsDeleted { get; set; } = false;


    }
}
