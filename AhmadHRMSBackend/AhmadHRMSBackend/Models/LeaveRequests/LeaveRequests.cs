using AhmadHRMSBackend.Models.LeaveTypes;
using AhmadHRMSBackend.Models.Status;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.LeaveRequests
{
    [Table("LeaveRequests")]
    public class LeaveRequests
    {
        [Key]
        public int LeaveRequestsID { get; set; }

        // 🔗 FK → Employee
        [Required]
        public int EmployeeId { get; set; }

        // 🔗 FK → LeaveType
        [Required]
        public int LeaveTypeId { get; set; }

        // 🔗 FK → LeaveStatus
        [Required]
        public int LeaveStatusId { get; set; }


        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int Days { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }

        [Required]
        public DateTime AppliedDate { get; set; }

        public bool IsDeleted { get; set; } = false;


        // 🔗 Navigation Property
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeList.EmployeeList Employee { get; set; }

        // 🔗 Navigation Property
        [ForeignKey(nameof(LeaveTypeId))]
        public LeaveTypes.LeaveTypes LeaveType { get; set; }


        // 🔗 Navigation Property
        [ForeignKey(nameof(LeaveStatusId))]
        public LeaveStatus.LeaveStatus LeaveStatus { get; set; }
    }
}
