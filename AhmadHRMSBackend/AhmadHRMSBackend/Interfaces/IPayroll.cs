using AhmadHRMSBackend.dto.LeaveEmployee;
using AhmadHRMSBackend.dto.Payroll;

namespace AhmadHRMSBackend.Interfaces
{
    public interface IPayroll
    {

        Task<List<SendPayrollDto>> GetPayrollRequest(GetPayrollRequestDto dto);

        Task<List<GetPayrollStatusDto>> GetPayrollStatus();

        Task<List<LeaveEmployeeDto>> GetEmployeeForPayroll();

        Task<bool> SubmitPayrollRequest(SubmitPayrollRequestDto dto);

        Task<bool> ChangePayrollStatus(ChangePayrollStatusDto dto);
    }
}
