using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.Payroll;
using AhmadHRMSBackend.Interfaces;
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
    }
}
