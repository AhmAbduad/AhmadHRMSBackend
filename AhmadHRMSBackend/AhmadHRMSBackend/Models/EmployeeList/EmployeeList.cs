using AhmadHRMSBackend.Models.AttendanceSummary;
using AhmadHRMSBackend.Models.Departments;
using AhmadHRMSBackend.Models.LeaveRequests;
using AhmadHRMSBackend.Models.Login;
using AhmadHRMSBackend.Models.PayrollRequests;
using AhmadHRMSBackend.Models.Performance;
using AhmadHRMSBackend.Models.Position;
using AhmadHRMSBackend.Models.Timesheets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace AhmadHRMSBackend.Models.EmployeeList
{
    [Table("EmployeeList")]
    public class EmployeeList
    {
        [Key]
        public int EmployeeID { get; set; }

        // FK
        [Required]
        public int DepartmentID { get; set; }

        // FK
        [Required]
        public int PositionID { get; set; }

        // FK
        [Required]
        public int StatusID { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }


        [Required]
        public DateTime JoinDate { get; set; }

        [Required]
        [StringLength(100)]
        public string avatar { get; set; }



        public bool IsDeleted { get; set; } = false;




        // 🔗 Navigation Property
        [ForeignKey(nameof(DepartmentID))]
        public Departments.Departments Departments { get; set; }



        // 🔗 Navigation Property
        [ForeignKey(nameof(PositionID))]
        public Position.Position Position { get; set; }


        [ForeignKey(nameof(StatusID))]
        public Status.Status Status { get; set; }


        public ICollection<AttendanceRecord.AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord.AttendanceRecord>();

        public ICollection<AttendanceSummary.AttendanceSummary> AttendanceSummary { get; set; } = new List<AttendanceSummary.AttendanceSummary>();


        public ICollection<LeaveRequests.LeaveRequests> LeaveRequests { get; set; }= new List<LeaveRequests.LeaveRequests>();


        public ICollection<Timesheets.Timesheets> Timesheets { get; set; } = new List<Timesheets.Timesheets>();

        public ICollection<PayrollRequests.PayrollRequests> PayrollRequests { get; set; } = new List<PayrollRequests.PayrollRequests>();

        public ICollection<Performance.Performance> Performance { get; set; } = new List<Performance.Performance>();

        public Users Users { get; set; }
    }
}
