namespace AhmadHRMSBackend.dto.TimeSheetDetails
{
    public class TimeSheetDetailsDto
    {
        public int TimeSheetDetailID { get; set; }

        public string employee { get; set; }

        public DayDto monday { get; set; }
        public DayDto tuesday { get; set; }
        public DayDto wednesday { get; set; }
        public DayDto thursday { get; set; }
        public DayDto friday { get; set; }
        public DayDto saturday { get; set; }
        public DayDto sunday { get; set; }

        public string total { get; set; }

        public string avatar { get; set; }
    }
}
