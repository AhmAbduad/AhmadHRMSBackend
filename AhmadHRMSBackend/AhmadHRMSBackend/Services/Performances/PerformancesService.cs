using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.LeaveEmployee;
using AhmadHRMSBackend.dto.Performance;
using AhmadHRMSBackend.UnitofWork;

namespace AhmadHRMSBackend.Services.Performances
{
    public class PerformancesService
    {
        private readonly IUnitofWork _unitOfWork;

        public PerformancesService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetPerformancePeriodDto>> GetPerfromancePeriod()
        {
            var result = await _unitOfWork.Performances.GetPerfromancePeriod();
            return result;
        }

        public async Task<List<DepartmentDto>> GetDepartmentForPerformance()
        {
            var result = await _unitOfWork.Performances.GetDepartmentForPerformance();
            return result;
        }

        public async Task<List<GetPerformanceDataDto>> GetPerformanceData(PeriodnameDto dto)
        {
            var result = await _unitOfWork.Performances.GetPerformanceData(dto);
            return result;
        }

        public async Task<List<LeaveEmployeeDto>> GetEmployeesForPerformance()
        {
            var result = await _unitOfWork.Performances.GetEmployeesForPerformance();
            return result;
        }

        public async Task<bool> SubmitPerformanceData(SubmitPerformanceDataDto dto)
        {
            var result = await _unitOfWork.Performances.SubmitPerformanceData(dto);
            return result;
        }
    }
}
