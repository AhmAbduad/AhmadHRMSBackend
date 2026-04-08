using AhmadHRMSBackend.dto.TimeSheetDetails;

namespace AhmadHRMSBackend.Interfaces
{
    public interface ITimeSheet
    {
        Task<List<TimeSheetDetailsDto>> GetTimeSheetDetail(GetTimesheetDto dto);
    }
}
