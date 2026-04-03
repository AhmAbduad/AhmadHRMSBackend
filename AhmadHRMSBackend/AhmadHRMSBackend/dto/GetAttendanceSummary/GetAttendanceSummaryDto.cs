namespace AhmadHRMSBackend.dto.GetAttendanceSummary
{
    public class GetAttendanceSummaryDto
    {
        public string EmployeeName { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }

        public int Late { get; set; }
        public int Leave { get; set; }

    }
}
