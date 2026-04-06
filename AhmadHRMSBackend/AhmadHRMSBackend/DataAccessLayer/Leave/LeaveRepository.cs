using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.LeaveRequest;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.Leave
{
    public class LeaveRepository:ILeave
    {
        private readonly AppDbContext _context;

        public LeaveRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetDepartmentsForLeave()
        {
            return await _context.Departments
          .Where(d => !d.IsDeleted) // 🔹 Only non-deleted departments
          .ToListAsync();
        }

        public async Task<List<LeaveRequestDto>> GetLeaveRequest()
        {
            var data = await _context.LeaveRequests
            .Include(l => l.Employee)
                .ThenInclude(e => e.Departments)
            .Include(l => l.LeaveType)
            .Include(l => l.LeaveStatus)
            .Where(l => !l.IsDeleted)
            .Select(l => new LeaveRequestDto
            {
                id = l.LeaveRequestsID,
                employee = l.Employee.Name,
                department = l.Employee.Departments.Label,
                leaveType = l.LeaveType.LeaveTypeName,
                startDate = l.StartDate,
                endDate = l.EndDate,
                days = l.Days,
                reason = l.Reason,
                status = l.LeaveStatus.StatusName,
                appliedDate = l.AppliedDate,
                avatar = l.Employee.avatar
            })
            .ToListAsync();

            return data;
        }

        public async Task<List<AhmadHRMSBackend.Models.LeaveStatus.LeaveStatus>> GetStatusForLeave()
        {
            return await _context.LeaveStatus
             .Where(d => !d.IsDeleted) // 🔹 Only non-deleted departments
             .ToListAsync();
        }
    }
}
