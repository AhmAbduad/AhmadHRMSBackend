namespace AhmadHRMSBackend.dto.TimeSheetDetails
{
    public class GetTimesheetDto
    {
        //public int? TimesheetId { get; set; }
        public int? EmployeeId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Week { get; set; }
    }
}
