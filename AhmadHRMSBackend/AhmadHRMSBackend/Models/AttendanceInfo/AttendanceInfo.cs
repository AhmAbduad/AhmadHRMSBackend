using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.AttendanceInfo
{
    [Table("AttendanceInfo")]
    public class AttendanceInfo
    {

        [Key]
        public int AttendanceInfoId { get; set; }

        [Required]
        [StringLength(200)]
        public DateTime CheckInTime { get; set; }
        [Required]
        [StringLength(200)]
        public DateTime CheckOutTime { get; set; }
        [Required]
        [StringLength(200)]
        public TimeSpan TotalHours => CheckOutTime - CheckInTime;
        [Required]
        [StringLength(200)]
        public string Status { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
