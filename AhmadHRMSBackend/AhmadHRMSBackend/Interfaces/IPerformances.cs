using AhmadHRMSBackend.dto.Performance;

namespace AhmadHRMSBackend.Interfaces
{
    public interface IPerformances
    {
        Task<List<GetPerformancePeriodDto>> GetPerfromancePeriod();
    }
}
