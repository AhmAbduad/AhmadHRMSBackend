using AhmadHRMSBackend.Models.Departments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.AttendanceRecord
{
    [Table("AttendanceRecords")]
    public class AttendanceRecord
    {
        [Key]
        public int Id { get; set; }


        // FK
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string CheckIn { get; set; }

        [Required]
        public string CheckOut { get; set; }

        [Required]
        public string TotalHours { get; set; }

        [Required]
        public string Status { get; set; }

        public bool IsDeleted { get; set; } = false;



        // 🔗 Navigation Property
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeList.EmployeeList Employee { get; set; }

    }
}
