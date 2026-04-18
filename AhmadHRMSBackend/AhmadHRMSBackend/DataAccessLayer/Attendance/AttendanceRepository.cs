using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.GetAttendanceRecord;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.Attendance
{
    public class AttendanceRepository:IAttendance
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AhmadHRMSBackend.Models.AttendanceInfo.AttendanceInfo> GetAttendanceInfo()
        {
            return await _context.AttendanceInfo
            .Where(a => !a.IsDeleted) // 🔹 Only non-deleted
            .OrderBy(a => a.AttendanceInfoId) // 🔹 First record (by Id)
            .FirstOrDefaultAsync();
        }


        public async Task<List<GetAttendanceRecordDto>> GetAttendanceRecord(AttendanceRecordMonthDto dto)
        {
            if (dto == null || dto.month <= 0 || dto.month > 12)
                return new List<GetAttendanceRecordDto>();

            var currentYear = DateTime.Now.Year;

            var data = await _context.AttendanceRecords
                .Where(x => !x.IsDeleted
                    && x.Date.Month == dto.month
                    && x.Date.Year == currentYear) // ✅ filter by current year
                .Include(x => x.Employee)
                    .ThenInclude(e => e.Departments)
                .Select(x => new GetAttendanceRecordDto
                {
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.Name,
                    Department = x.Employee.Departments.Label,
                    Date = x.Date,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut,
                    Status = x.Status,
                    Avatar = x.Employee.avatar
                })
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return data;
        }

        public async Task<AhmadHRMSBackend.Models.AttendanceSummary.AttendanceSummary> GetAttendanceSummary(int id)
        {
            return await _context.AttendanceSummary
            .Include(a => a.Employee) // 🔹 Optional (agar employee data bhi chahiye)
            .Where(a => a.EmployeeId == id && !a.IsDeleted)
            .OrderByDescending(a => a.Year)
            .ThenByDescending(a => a.Month) // 🔹 Latest month first
            .FirstOrDefaultAsync();
        }

        public async Task<List<DepartmentDto>> GetDepartmentForAttendance()
        {
            var data = await _context.Departments
           .Where(d => !d.IsDeleted)
           .Select(d => new DepartmentDto
           {
               id = d.DepartmentsID,
               Value = d.Value,
               Label = d.Label
           })
           .ToListAsync();

            return data;
        }
    }
}
