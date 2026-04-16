namespace AhmadHRMSBackend.dto.Reports
{
    public class SubmitReportListDto
    {
        public string reportName { get; set; }

        public int reportTypeId { get; set; }

        public int reportPeriodId { get; set; }

        public int departmentId { get; set; }

        public int reportStatusId { get; set; }

        public DateTime generatedDate { get; set; }

        public string generatedBy { get; set; }

        public string description { get; set; }

        //public byte[] fileData { get; set; }

        public IFormFile fileData { get; set; }
    }
}
