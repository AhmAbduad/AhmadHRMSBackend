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
    }
}
