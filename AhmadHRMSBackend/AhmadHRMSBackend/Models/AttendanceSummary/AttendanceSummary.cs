using AhmadHRMSBackend.Models.Departments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.AttendanceSummary
{

    [Table("AttendanceSummary")]
    public class AttendanceSummary
    {
        [Key]
        public int AttendanceSummaryId { get; set; }


        // 🔹 FK (important)
        public int EmployeeId { get; set; }

        // 🔹 Month / Year
        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }



        [Required]
        public int Present { get; set; }

        [Required]
        public int Absent { get; set; }

        [Required]
        public int Late { get; set; }

        [Required]
        public int Leave { get; set; }

        public bool IsDeleted { get; set; } = false;



        // 🔗 Navigation Property
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeList.EmployeeList Employee { get; set; }
    }
}
