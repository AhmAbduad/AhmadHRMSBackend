using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.LeaveRequest;

namespace AhmadHRMSBackend.Interfaces
{
    public interface ILeave
    {
        Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetDepartmentsForLeave();

        Task<List<LeaveRequestDto>> GetLeaveRequest();

        Task<List<AhmadHRMSBackend.Models.LeaveStatus.LeaveStatus>> GetStatusForLeave();
    }
}
