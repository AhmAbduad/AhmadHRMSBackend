using AhmadHRMSBackend.dto.ChangeStatus;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.LeaveEmployee;
using AhmadHRMSBackend.dto.LeaveRequest;
using AhmadHRMSBackend.dto.LeaveStats;
using AhmadHRMSBackend.dto.LeaveTypes;
using AhmadHRMSBackend.dto.SubmitLeaveRequest;

namespace AhmadHRMSBackend.Interfaces
{
    public interface ILeave
    {
        Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetDepartmentsForLeave();

        Task<List<LeaveRequestDto>> GetLeaveRequest();

        Task<List<AhmadHRMSBackend.Models.LeaveStatus.LeaveStatus>> GetStatusForLeave();

        Task<LeaveStatsDto> GetLeaveStats();

        Task<List<LeaveTypesDto>>  GetLeaveTypes();

        Task<bool> SaveMarkAttendance(SubmitLeaveRequestDto dto);

        Task<List<LeaveEmployeeDto>> GetEmployeesForLeave();

        Task<bool> ChangeLeaveRequestStatus(ChangeStatusDto dto);
    }
}
