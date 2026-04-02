using System.ComponentModel.DataAnnotations;

namespace AhmadHRMSBackend.dto.GetAttendanceInfo
{
    public class GetAttendanceInfoDto
    {
        public int AttendanceInfoId { get; set; }

        public DateTime CheckInTime { get; set; }

        public DateTime CheckOutTime { get; set; }

        public TimeSpan TotalHours => CheckOutTime - CheckInTime;

        public string Status { get; set; }

    }
}   
