using AhmadHRMSBackend.Models.TimesheetDetails;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.Timesheets
{
    [Table("Timesheets")]
    public class Timesheets
    {
        [Key]
        public int TimesheetId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime WeekStartDate { get; set; }

        [Required]
        public DateTime WeekEndDate { get; set; }

        public decimal TotalHours { get; set; }

        public decimal RegularHours { get; set; }

        public decimal OvertimeHours { get; set; }

        public int DaysWorked { get; set; }

        public bool IsDeleted { get; set; } = false;

        // 🔗 Navigation
        [ForeignKey(nameof(EmployeeId))]
        public EmployeeList.EmployeeList Employee { get; set; }

        public ICollection<TimesheetDetails.TimesheetDetails> TimesheetDetails { get; set; }= new List<TimesheetDetails.TimesheetDetails>();
    }
}
