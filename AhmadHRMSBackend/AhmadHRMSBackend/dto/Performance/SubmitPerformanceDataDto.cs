namespace AhmadHRMSBackend.dto.Performance
{
    public class SubmitPerformanceDataDto
    {
        public int employeeId { get; set; }

        public int periodId { get; set; }

        public decimal rating { get; set; }

        public DateTime reviewDate { get; set; }

        public DateTime nextReview { get; set; }

        public List<string> goals { get; set; }

        public List<string> achievements { get; set; }
    }
}
