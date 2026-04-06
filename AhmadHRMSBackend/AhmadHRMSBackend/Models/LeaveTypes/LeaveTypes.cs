using AhmadHRMSBackend.Models.LeaveRequests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.LeaveTypes
{
    [Table("LeaveTypes")]
    public class LeaveTypes
    {
        [Key]
        public int LeaveTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string LeaveTypeName { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<LeaveRequests.LeaveRequests> LeaveRequests { get; set; }
            = new List<LeaveRequests.LeaveRequests>();
    }
}
