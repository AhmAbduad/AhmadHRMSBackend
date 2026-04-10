using AhmadHRMSBackend.Models.PayrollStatus;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.PayrollRequests
{
    [Table("PayrollRequests")]
    public class PayrollRequests
    {
        [Key]
        public int PayrollRequestId { get; set; }

        // FK
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Type { get; set; }


        [Required]
        public decimal Amount   { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }

        public DateTime? ProcessedDate { get; set; }

        // Foreign Key
        [Required]
        public int StatusId { get; set; }


        public bool IsDeleted { get; set; } = false;


        // 🔗 Navigation
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeList.EmployeeList Employee { get; set; }


        [ForeignKey(nameof(StatusId))]
        public PayrollStatus.PayrollStatus PayrollStatus { get; set; }
    }
}
