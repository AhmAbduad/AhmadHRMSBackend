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


    }
}
