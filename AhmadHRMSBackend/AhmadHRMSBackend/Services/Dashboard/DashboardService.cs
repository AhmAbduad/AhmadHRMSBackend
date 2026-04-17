using AhmadHRMSBackend.dto.Dashboard;
using AhmadHRMSBackend.UnitofWork;

namespace AhmadHRMSBackend.Services.Dashboard
{
    public class DashboardService
    {
        private readonly IUnitofWork _unitOfWork;

        public DashboardService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EmployeeDashboardDto>> GetEmployeesForDashboard()
        {
            var result = await _unitOfWork.Dashboard.GetEmployeesForDashboard();
            return result;
        }

        public async Task<List<LeaveRequestDashboardDto>> GetLeaveRequestForDashboard()
        {
            var result = await _unitOfWork.Dashboard.GetLeaveRequestForDashboard();
            return result;
        }

        public async Task<ReturnAttendanceMonthDto> GetAttendanceDataForDashboard(AttendanceDataMonthDto dto)
        {
            var result = await _unitOfWork.Dashboard.GetAttendanceDataForDashboard(dto);
            return result;
        }

        public async Task<PerformanceDataDto> GetPerformanceDataForDashboard()
        {
            var result = await _unitOfWork.Dashboard.GetPerformanceDataForDashboard();
            return result;
        }
    }
}
