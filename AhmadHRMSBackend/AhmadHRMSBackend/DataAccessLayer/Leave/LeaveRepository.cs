using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.ChangeStatus;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.LeaveEmployee;
using AhmadHRMSBackend.dto.LeaveRequest;
using AhmadHRMSBackend.dto.LeaveStats;
using AhmadHRMSBackend.dto.LeaveTypes;
using AhmadHRMSBackend.dto.SubmitLeaveRequest;
using AhmadHRMSBackend.Interfaces;
using AhmadHRMSBackend.Models.LeaveRequests;
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
                .Include(l => l.Employee)
                    .ThenInclude(e => e.Status) // ✅ IMPORTANT
                .Include(l => l.LeaveType)
                .Include(l => l.LeaveStatus)
                .Where(l => !l.IsDeleted
                    && !l.Employee.IsDeleted
                    && l.Employee.Status.StatusName != "Inactive") // ✅ FILTER
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

        public async Task<LeaveStatsDto> GetLeaveStats()
        {
            var query = _context.LeaveRequests
            .Include(l => l.Employee)
                .ThenInclude(e => e.Status)
            .Include(l => l.LeaveStatus)
            .Where(l => !l.IsDeleted
            && !l.Employee.IsDeleted
           && l.Employee.Status.StatusName != "Inactive"); // ❌ inactive exclude

            var total = await query.CountAsync();

            var pending = await query.CountAsync(l => l.LeaveStatus.StatusName == "pending");
            var approved = await query.CountAsync(l => l.LeaveStatus.StatusName == "approved");
            var rejected = await query.CountAsync(l => l.LeaveStatus.StatusName == "rejected");

            var result = new LeaveStatsDto
            {
                totalRequests = total,
                pending = pending,
                approved = approved,
                rejected = rejected
            };

            return result;
        }

        public async Task<List<LeaveTypesDto>> GetLeaveTypes()
        {
            var data = await _context.LeaveTypes
            .Where(l => !l.IsDeleted) // ✅ only active records
            .Select(l => new LeaveTypesDto
            {
                leavetypeid = l.LeaveTypeId,
                leavetypename = l.LeaveTypeName
            })
            .ToListAsync();

            return data;
        }


        public async Task<bool> SaveMarkAttendance(SubmitLeaveRequestDto dto)
        {
            if (dto == null)
                return false;

            // ✅ Optional: Validate employee exists & active
            var employee = await _context.EmployeeList
             .Include(e => e.Status) // ✅ important
             .FirstOrDefaultAsync(e =>
                 e.EmployeeID == dto.employeeId
                 && !e.IsDeleted
                 && e.Status.StatusName != "Inactive");

            if (employee == null)
                return false;

            // ✅ Auto calculate days (avoid frontend dependency)
            var totalDays = (dto.endDate.Date - dto.startDate.Date).Days + 1;

            var leaveRequest = new LeaveRequests
            {
                EmployeeId = dto.employeeId,
                LeaveTypeId = dto.leaveTypeId,

                // 🔥 Default pending (recommended)
                LeaveStatusId = 1,

                StartDate = dto.startDate,
                EndDate = dto.endDate,

                Days = totalDays > 0 ? totalDays : dto.days,

                Reason = dto.reason,

                // 🔥 Always set from backend
                AppliedDate = DateTime.Now,

                IsDeleted = false
            };

            await _context.LeaveRequests.AddAsync(leaveRequest);
            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<List<LeaveEmployeeDto>> GetEmployeesForLeave()
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

        public async Task<bool> ChangeLeaveRequestStatus(ChangeStatusDto dto)
        {
            if (dto == null)
                return false;

            // ✅ Get leave request
            var leaveRequest = await _context.LeaveRequests
                .FirstOrDefaultAsync(l =>
                    l.LeaveRequestsID == dto.LeaveRequestId
                    && !l.IsDeleted);

            if (leaveRequest == null)
                return false;

            // ✅ Update status
            leaveRequest.LeaveStatusId = dto.statusID;

            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
