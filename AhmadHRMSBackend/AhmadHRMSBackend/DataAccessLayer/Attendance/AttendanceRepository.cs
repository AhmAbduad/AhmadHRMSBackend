using AhmadHRMSBackend.Data;
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


        public async Task<List<AhmadHRMSBackend.Models.AttendanceRecord.AttendanceRecord>> GetAttendanceRecord()
        {
             return await _context.AttendanceRecords
            .Include(a => a.Employee) // 🔹 Join with Employee table
            .ThenInclude(e => e.Departments) 
            .Where(a => !a.IsDeleted)
            .OrderByDescending(a => a.Date)
            .ToListAsync();
        }
    }
}
