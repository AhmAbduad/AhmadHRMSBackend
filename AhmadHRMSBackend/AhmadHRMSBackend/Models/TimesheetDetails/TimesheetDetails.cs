using AhmadHRMSBackend.Models.Timesheets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhmadHRMSBackend.Models.TimesheetDetails
{
    [Table("TimesheetDetails")]
    public class TimesheetDetails
    {
        [Key]
        public int TimesheetDetailId { get; set; }

        [Required]
        public int TimesheetId { get; set; }

        [Required]
        public DateTime WorkDate { get; set; }

        [Required]
        [StringLength(20)]
        public string DayName { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // regular, overtime, leave, off

        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(TimesheetId))]
        public Timesheets.Timesheets Timesheet { get; set; }
    }
}
