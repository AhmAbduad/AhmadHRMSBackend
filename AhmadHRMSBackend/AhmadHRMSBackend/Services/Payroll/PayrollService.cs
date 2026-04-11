using AhmadHRMSBackend.dto.Payroll;
using AhmadHRMSBackend.UnitofWork;

namespace AhmadHRMSBackend.Services.Payroll
{
    public class PayrollService
    {
        private readonly IUnitofWork _unitOfWork;

        public PayrollService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SendPayrollDto>> GetPayrollRequest(GetPayrollRequestDto dto)
        {
            var result = await _unitOfWork.Payroll.GetPayrollRequest(dto);
            return result;
        }

        public async Task<List<GetPayrollStatusDto>> GetPayrollStatus()
        {
            var result = await _unitOfWork.Payroll.GetPayrollStatus();
            return result;
        }
    }
}
