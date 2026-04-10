using AhmadHRMSBackend.dto.Payroll;

namespace AhmadHRMSBackend.Interfaces
{
    public interface IPayroll
    {

        Task<List<SendPayrollDto>> GetPayrollRequest(GetPayrollRequestDto dto);
    }
}
