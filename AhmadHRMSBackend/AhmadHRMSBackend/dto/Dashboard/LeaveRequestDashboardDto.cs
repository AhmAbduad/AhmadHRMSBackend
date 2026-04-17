namespace AhmadHRMSBackend.dto.Dashboard
{
    public class LeaveRequestDashboardDto
    {
        public int id { get; set; }

        public string employee { get; set; }

        public string type { get; set; }

        public string status { get; set; }

        public int days { get; set; }
    }
}
