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

    }
}
