using AhmadHRMSBackend.dto.GetAttendanceRecord;
using AhmadHRMSBackend.dto.SaveAttendance;

namespace AhmadHRMSBackend.Interfaces
{
    public interface IMarkAttendance
    {
        Task<List<GetAttendanceRecordDto>> GetMarkAttendanceRecord(DateTime date,int departmentId);

        Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetDepartments();

        Task<bool> SaveMarkAttendance(SaveAttendanceDto dto);
    }
}
