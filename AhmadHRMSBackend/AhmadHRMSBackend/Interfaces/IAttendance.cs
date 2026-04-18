using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.GetAttendanceRecord;

namespace AhmadHRMSBackend.Interfaces
{
    public interface IAttendance
    {
        Task<AhmadHRMSBackend.Models.AttendanceInfo.AttendanceInfo> GetAttendanceInfo();

        Task<List<GetAttendanceRecordDto>> GetAttendanceRecord(AttendanceRecordMonthDto dto);

        Task<AhmadHRMSBackend.Models.AttendanceSummary.AttendanceSummary> GetAttendanceSummary(int id);

        Task<List<DepartmentDto>> GetDepartmentForAttendance();
    }
}
