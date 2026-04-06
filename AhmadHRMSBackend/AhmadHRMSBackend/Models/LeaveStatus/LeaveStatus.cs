using AhmadHRMSBackend.Models.LeaveRequests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.LeaveStatus
{
    [Table("LeaveStatus")]
    public class LeaveStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; } // pending, approved, rejected

        public bool IsDeleted { get; set; } = false;

        public ICollection<LeaveRequests.LeaveRequests> LeaveRequests { get; set; }
            = new List<LeaveRequests.LeaveRequests>();
    }
}
