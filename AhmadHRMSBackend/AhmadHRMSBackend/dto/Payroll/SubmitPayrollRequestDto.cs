namespace AhmadHRMSBackend.dto.Payroll
{
    public class SubmitPayrollRequestDto
    {
        public int employeeid { get; set; }

        public string requesttype { get; set; }

        public DateTime requestdate { get; set; }

        public decimal amount { get; set; }
        
    }
}
