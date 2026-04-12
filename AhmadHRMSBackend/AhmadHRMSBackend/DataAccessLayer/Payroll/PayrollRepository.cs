using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.LeaveEmployee;
using AhmadHRMSBackend.dto.Payroll;
using AhmadHRMSBackend.Interfaces;
using AhmadHRMSBackend.Models.PayrollRequests;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.Payroll
{
    public class PayrollRepository:IPayroll
    {
        private readonly AppDbContext _context;

        public PayrollRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SendPayrollDto>> GetPayrollRequest(GetPayrollRequestDto dto)
        {
            if (dto == null)
                return new List<SendPayrollDto>();

            var query = _context.PayrollRequests
                .Include(p => p.Employee)
                .ThenInclude(e => e.Departments)
                .Include(p => p.PayrollStatus)
                .Where(p => !p.IsDeleted);

            // ✅ Month filter
            if (dto.Month > 0)
                query = query.Where(p => p.RequestDate.Month == dto.Month);

            // ✅ Year filter
            if (dto.Year > 0)
                query = query.Where(p => p.RequestDate.Year == dto.Year);

            var data = await query.ToListAsync();

            // ✅ Mapping
            var result = data.Select(p => new SendPayrollDto
            {
                id = p.PayrollRequestId,
                employee = p.Employee.Name,
                department = p.Employee.Departments.Label, // 👈 make sure exists
                type = p.Type,
                amount = p.Amount,
                requestDate = p.RequestDate,
                processedDate = p.ProcessedDate,
                status = p.PayrollStatus.Label.ToLower(),
                //avatar = GetAvatar(p.Employee.Name)
                avatar = p.Employee.avatar
            }).ToList();

            return result;
        }

        public async Task<List<GetPayrollStatusDto>> GetPayrollStatus()
        {
            var data = await _context.PayrollStatus
            .Where(s => !s.IsDeleted)
            .Select(s => new GetPayrollStatusDto
            {
                PayrollID = s.PayrollStatusId,
                Label = s.Label
            })
            .ToListAsync();

            return data;
        }


        public async Task<List<LeaveEmployeeDto>> GetEmployeeForPayroll()
        {
            var employees = await _context.EmployeeList
           .Where(e => !e.IsDeleted && e.Status.StatusName != "Inactive") // ✅ only active employees
           .Select(e => new LeaveEmployeeDto
           {
               EmployeeID = e.EmployeeID,
               EmployeeName = e.Name
           })
           .ToListAsync();

            return employees;
        }


        public async Task<bool> SubmitPayrollRequest(SubmitPayrollRequestDto dto)
        {
            if (dto == null)
                return false;

            // ✅ Check Employee exists (and not deleted)
            var employee = await _context.EmployeeList
                .FirstOrDefaultAsync(e =>
                    e.EmployeeID == dto.employeeid &&
                    !e.IsDeleted);

            if (employee == null)
                return false;

            // ✅ Get Pending Status Id
            var pendingStatus = await _context.PayrollStatus
                .FirstOrDefaultAsync(s =>
                    s.Label.ToLower() == "pending" &&
                    !s.IsDeleted);

            if (pendingStatus == null)
                return false;

            // ✅ Create Payroll Request
            var payroll = new PayrollRequests
            {
                EmployeeId = dto.employeeid,
                Type = dto.requesttype,
                Amount = dto.amount,
                RequestDate = dto.requestdate,
                StatusId = pendingStatus.PayrollStatusId,
                ProcessedDate = null, // pending hai
                IsDeleted = false
            };

            _context.PayrollRequests.Add(payroll);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePayrollStatus(ChangePayrollStatusDto dto)
        {
            if (dto == null)
                return false;

            var payroll = await _context.PayrollRequests
                .FirstOrDefaultAsync(p => p.PayrollRequestId == dto.payrollRequestId && !p.IsDeleted);

            if (payroll == null)
                return false;

            payroll.StatusId = dto.statusid;
            payroll.ProcessedDate = dto.processeddate;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
