using AhmadHRMSBackend.dto.LeaveEmployee;
using AhmadHRMSBackend.dto.TimeSheetDetails;

namespace AhmadHRMSBackend.Interfaces
{
    public interface ITimeSheet
    {
        Task<List<TimeSheetDetailsDto>> GetTimeSheetDetail(GetTimesheetDto dto);

        Task<List<LeaveEmployeeDto>> GetEmployeesForTimeSheet();
    }
}
