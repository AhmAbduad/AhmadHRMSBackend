using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace AhmadHRMSBackend.dto.Payroll
{
    public class SendPayrollDto
    {
        public int id { get; set; }

        public string employee { get; set; }

        public string department { get; set; }

        public string type { get; set; }

        public decimal amount { get; set; }

        public DateTime requestDate { get; set; }

        public string status { get; set; }

        public DateTime? processedDate { get; set; }

        public string avatar { get; set; }  

    }
}
