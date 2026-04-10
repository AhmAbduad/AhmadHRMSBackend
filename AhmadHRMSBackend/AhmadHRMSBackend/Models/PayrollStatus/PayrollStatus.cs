using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.PayrollStatus
{
    [Table("PayrollStatus")]
    public class PayrollStatus
    {
        [Key]
        public int PayrollStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string Label { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<PayrollRequests.PayrollRequests> PayrollRequests { get; set; } = new List<PayrollRequests.PayrollRequests>();
    }
}
