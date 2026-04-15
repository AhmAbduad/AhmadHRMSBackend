using AhmadHRMSBackend.UnitofWork;

namespace AhmadHRMSBackend.Services.Reports
{
    public class ReportsService
    {
        private readonly IUnitofWork _unitOfWork;

        public ReportsService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
