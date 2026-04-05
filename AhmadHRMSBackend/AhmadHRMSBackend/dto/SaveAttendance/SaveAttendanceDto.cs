namespace AhmadHRMSBackend.dto.SaveAttendance
{
    public class SaveAttendanceDto
    {
        public DateTime Date { get; set; }
        public int DepartmentId { get; set; }

        public List<SaveAttendanceEmployeeDto> Employees { get; set; }
    }

    public class SaveAttendanceEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Status { get; set; }

        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
