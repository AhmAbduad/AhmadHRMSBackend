namespace AhmadHRMSBackend.dto.TimeSheetDetails
{
    public class SaveTimeSheetDto
    {
        public int EmployeeId { get; set; }

        // Week Info
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }

        // Days
        public DayDto Monday { get; set; }
        public DayDto Tuesday { get; set; }
        public DayDto Wednesday { get; set; }
        public DayDto Thursday { get; set; }
        public DayDto Friday { get; set; }
        public DayDto Saturday { get; set; }
        public DayDto Sunday { get; set; }
    }
}
