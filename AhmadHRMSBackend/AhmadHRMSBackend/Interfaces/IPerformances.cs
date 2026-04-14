using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.Performance;

namespace AhmadHRMSBackend.Interfaces
{
    public interface IPerformances
    {
        Task<List<GetPerformancePeriodDto>> GetPerfromancePeriod();

        Task<List<DepartmentDto>> GetDepartmentForPerformance();

        Task<List<GetPerformanceDataDto>> GetPerformanceData(PeriodnameDto dto);
    }
}
