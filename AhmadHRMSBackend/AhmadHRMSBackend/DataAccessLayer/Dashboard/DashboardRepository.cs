using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.Dashboard;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.Dashboard
{
    public class DashboardRepository:IDashboard
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDashboardDto>> GetEmployeesForDashboard()
        {
            var employees = await _context.EmployeeList
            .Include(e => e.Position)
            .Include(e => e.Status)
            .Where(e => !e.IsDeleted)
            .Select(e => new EmployeeDashboardDto
            {
                id = e.EmployeeID,
                name = e.Name,
                position = e.Position.PositionName,   // ⚠️ make sure property name correct ho
                status = e.Status.StatusName,         // ⚠️ make sure property name correct ho
                avatar = e.avatar
            })
            .ToListAsync();

            return employees;
        }

        public async Task<List<LeaveRequestDashboardDto>> GetLeaveRequestForDashboard()
        {
            var data = await _context.LeaveRequests
            .Include(l => l.Employee)
            .Include(l => l.LeaveType)
            .Include(l => l.LeaveStatus)
            .Where(l => !l.IsDeleted)
            .OrderByDescending(l => l.AppliedDate) // latest first (dashboard ke liye best)
            .Select(l => new LeaveRequestDashboardDto
            {
                id = l.LeaveRequestsID,
                employee = l.Employee.Name,
                type = l.LeaveType.LeaveTypeName,        // ⚠️ property name check karo
                status = l.LeaveStatus.StatusName,  // ⚠️ property name check karo
                days = l.Days
            })
            .ToListAsync();

            return data;
        }

        public async Task<ReturnAttendanceMonthDto> GetAttendanceDataForDashboard(AttendanceDataMonthDto dto)
        {
            if (dto == null)
                return new ReturnAttendanceMonthDto();

            var data = await _context.AttendanceSummary
                .Where(a => !a.IsDeleted && a.Month == dto.month)
                .GroupBy(a => 1) // 🔥 single group for total sum
                .Select(g => new ReturnAttendanceMonthDto
                {
                    present = g.Sum(x => x.Present),
                    absent = g.Sum(x => x.Absent),
                    late = g.Sum(x => x.Late)
                })
                .FirstOrDefaultAsync();

            // 🔹 If no data found
            if (data == null)
            {
                return new ReturnAttendanceMonthDto
                {
                    present = 0,
                    absent = 0,
                    late = 0
                };
            }

            return data;
        }

        public async Task<PerformanceDataDto> GetPerformanceDataForDashboard()
        {
            var latestPerEmployee = await _context.Performance
            .Include(p => p.Employee)
            .Where(p => !p.IsDeleted)
            .GroupBy(p => p.EmployeeId)
            .Select(g => g.OrderByDescending(x => x.ReviewDate).First())
            .ToListAsync();

            // 🔹 Step 2: Un mein se highest rating wala select karo
            var topPerformer = latestPerEmployee
                .OrderByDescending(p => p.Rating)
                .FirstOrDefault();

            // 🔹 Step 3: Latest overall rating (optional)
            var latestRating = latestPerEmployee
                .OrderByDescending(p => p.ReviewDate)
                .FirstOrDefault();

            return new PerformanceDataDto
            {
                recentRating = latestRating != null ? latestRating.Rating : 0,

                topPerformer = topPerformer != null
                    ? topPerformer.Employee.Name
                    : "N/A"
            };
        }
    }
}
