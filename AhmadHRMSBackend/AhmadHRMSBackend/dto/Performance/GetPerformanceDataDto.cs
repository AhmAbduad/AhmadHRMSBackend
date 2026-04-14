namespace AhmadHRMSBackend.dto.Performance
{
    public class GetPerformanceDataDto
    {
        public int id { get; set; }

        public string employee { get; set; }

        public string department { get; set; }

        public string position { get; set; }

        public decimal rating { get; set; }

        public List<string> goals { get; set; }

        public List<string> achievements { get; set; }

        public DateTime reviewDate { get; set; }

        public DateTime nextReview { get; set; }

        public string avatar { get; set; }
    }
}
