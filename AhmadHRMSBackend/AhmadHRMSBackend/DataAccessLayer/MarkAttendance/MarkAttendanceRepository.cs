using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.MarkAttendance
{
    public class MarkAttendanceRepository:IMarkAttendance
    {
        private readonly AppDbContext _context;

        public MarkAttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AhmadHRMSBackend.Models.AttendanceRecord.AttendanceRecord>> GetMarkAttendanceRecord(DateTime date, int departmentId)
        {
            return await _context.AttendanceRecords
             .Include(a => a.Employee)
                 .ThenInclude(e => e.Departments)
             .Where(a => !a.IsDeleted
                 && a.Date.Date == date.Date
                 && a.Employee.Departments != null
                 && a.Employee.Departments.DepartmentsID == departmentId) // 🔥 FIX
             .OrderByDescending(a => a.Date)
             .ToListAsync();
        }

        public async Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetDepartments()
        {
            return await _context.Departments
           .Where(d => !d.IsDeleted) // 🔹 Only non-deleted departments
           .ToListAsync();
        }

    }
}
