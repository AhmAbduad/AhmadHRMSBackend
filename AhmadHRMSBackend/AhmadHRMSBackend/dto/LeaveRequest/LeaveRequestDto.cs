namespace AhmadHRMSBackend.dto.LeaveRequest
{
    public class LeaveRequestDto
    {
        public int id { get; set; }

        public string employee { get; set; }

        public string department { get; set; }

        public string leaveType { get; set; }
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int days { get; set; }

        public string reason { get; set; }

        public string status { get; set; }

        public DateTime appliedDate { get; set; }

        public string avatar   { get; set; }
    }
}
