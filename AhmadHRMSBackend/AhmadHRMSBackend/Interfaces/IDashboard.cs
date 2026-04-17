using AhmadHRMSBackend.dto.Dashboard;

namespace AhmadHRMSBackend.Interfaces
{
    public interface IDashboard
    {
        Task<List<EmployeeDashboardDto>> GetEmployeesForDashboard();

        Task<List<LeaveRequestDashboardDto>> GetLeaveRequestForDashboard();

        Task<ReturnAttendanceMonthDto> GetAttendanceDataForDashboard(AttendanceDataMonthDto dto);

        Task<PerformanceDataDto> GetPerformanceDataForDashboard();
    }
}
