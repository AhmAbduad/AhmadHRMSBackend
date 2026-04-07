namespace AhmadHRMSBackend.dto.SubmitLeaveRequest
{
    public class SubmitLeaveRequestDto
    {
        public int employeeId { get; set; }

        public int leaveTypeId { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int days { get; set; }

        public string reason { get; set; }

        
    }
}
