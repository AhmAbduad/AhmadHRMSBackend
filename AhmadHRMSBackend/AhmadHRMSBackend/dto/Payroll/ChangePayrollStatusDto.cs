namespace AhmadHRMSBackend.dto.Payroll
{
    public class ChangePayrollStatusDto
    {
        public int payrollRequestId { get; set; }

        public DateTime processeddate { get; set; }

        public int statusid { get; set; }
    }
}
