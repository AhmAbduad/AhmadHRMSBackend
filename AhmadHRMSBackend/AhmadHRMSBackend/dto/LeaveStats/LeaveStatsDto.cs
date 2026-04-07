namespace AhmadHRMSBackend.dto.LeaveStats
{
    public class LeaveStatsDto
    {
        public int totalRequests { get; set; }

        public int pending { get; set; }
        public int rejected { get; set; }

        public int approved { get; set; }
    }
}
