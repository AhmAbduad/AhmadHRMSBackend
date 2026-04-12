using AhmadHRMSBackend.dto.LeaveEmployee;
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

        public async Task<List<LeaveEmployeeDto>> GetEmployeeForPayroll()
        {
            var result = await _unitOfWork.Payroll.GetEmployeeForPayroll();
            return result;
        }

        public async Task<bool> SubmitPayrollRequest(SubmitPayrollRequestDto dto)
        {
            var result = await _unitOfWork.Payroll.SubmitPayrollRequest(dto);
            return result;
        }

        public async Task<bool> ChangePayrollStatus(ChangePayrollStatusDto dto)
        {
            var result = await _unitOfWork.Payroll.ChangePayrollStatus(dto);
            return result;
        }
    }
}
