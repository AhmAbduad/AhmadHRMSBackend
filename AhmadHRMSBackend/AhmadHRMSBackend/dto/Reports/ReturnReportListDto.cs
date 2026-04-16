namespace AhmadHRMSBackend.dto.Reports
{
    public class ReturnReportListDto
    {
        public int id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string period { get; set; }

        public string department { get; set; }

        public DateTime generatedDate { get; set; }

        public string generatedBy { get; set; }

        public string status { get; set; }

        public string fileSize { get; set; }

        public string format { get; set; }

        public string description { get; set; }
    }
}
