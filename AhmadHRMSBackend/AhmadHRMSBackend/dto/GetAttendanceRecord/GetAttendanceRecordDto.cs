namespace AhmadHRMSBackend.dto.GetAttendanceRecord
{
    public class GetAttendanceRecordDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public DateTime Date { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public TimeSpan TotalHours => CheckOut - CheckIn;
        public string Status { get; set; }

        public string Avatar { get; set; }
 
    }
}
