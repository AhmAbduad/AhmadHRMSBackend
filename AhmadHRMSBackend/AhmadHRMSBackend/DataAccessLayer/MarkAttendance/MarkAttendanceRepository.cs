using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.GetAttendanceRecord;
using AhmadHRMSBackend.dto.SaveAttendance;
using AhmadHRMSBackend.Interfaces;
using AhmadHRMSBackend.Models.AttendanceRecord;
using AhmadHRMSBackend.Models.AttendanceSummary;
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

        public async Task<List<GetAttendanceRecordDto>> GetMarkAttendanceRecord(DateTime date, int departmentId)
        {
            var employees = await _context.EmployeeList
                    .Include(e => e.Departments) // ✅ FIX
                    .Where(e => !e.IsDeleted && e.DepartmentID == departmentId)
                    .ToListAsync();

            var attendance = await _context.AttendanceRecords
                .Where(a => a.Date.Date == date.Date)
                .ToListAsync();

            var result = employees.Select(emp =>
            {
                var att = attendance.FirstOrDefault(a => a.EmployeeId == emp.EmployeeID);

                return new GetAttendanceRecordDto
                {
                    EmployeeId = emp.EmployeeID,
                    EmployeeName = emp.Name,
                    Department = emp.Departments?.Label ?? "",

                    Status = att?.Status ?? "",

                    CheckIn = att?.CheckIn,   // ✅ safe
                    CheckOut = att?.CheckOut, // ✅ safe

                    Avatar = emp.avatar,

                    Date = att?.Date ?? date  // ✅ fallback
                };
            }).ToList();

            return result;
        }

        public async Task<List<AhmadHRMSBackend.Models.Departments.Departments>> GetDepartments()
        {
            return await _context.Departments
           .Where(d => !d.IsDeleted) // 🔹 Only non-deleted departments
           .ToListAsync();
        }

        public async Task<bool> SaveMarkAttendance(SaveAttendanceDto dto)
        {
            foreach (var emp in dto.Employees)
            {
                var existing = await _context.AttendanceRecords
                    .FirstOrDefaultAsync(a =>
                        a.EmployeeId == emp.EmployeeId &&
                        a.Date.Date == dto.Date.Date);

                string oldStatus = existing?.Status;

                if (existing != null)
                {
                    // 🔁 UPDATE
                    existing.Status = emp.Status;
                    existing.CheckIn = emp.CheckIn;
                    existing.CheckOut = emp.CheckOut;
                }
                else
                {
                    // ➕ INSERT
                    existing = new AttendanceRecord
                    {
                        EmployeeId = emp.EmployeeId,
                        Date = dto.Date,
                        Status = emp.Status,
                        CheckIn = emp.CheckIn,
                        CheckOut = emp.CheckOut
                    };

                    _context.AttendanceRecords.Add(existing);
                }

                // 🔥 SUMMARY FIXED UPDATE
                await UpdateAttendanceSummary(emp.EmployeeId, dto.Date, oldStatus, emp.Status);
            }

            await _context.SaveChangesAsync();
            return true; //
        }

        private async Task UpdateAttendanceSummary(int employeeId, DateTime date, string oldStatus, string newStatus)
        {
            int month = date.Month;
            int year = date.Year;

            var summary = await _context.AttendanceSummary
                .FirstOrDefaultAsync(s =>
                    s.EmployeeId == employeeId &&
                    s.Month == month &&
                    s.Year == year);

            if (summary == null)
            {
                summary = new AttendanceSummary
                {
                    EmployeeId = employeeId,
                    Month = month,
                    Year = year,
                    Present = 0,
                    Absent = 0,
                    Late = 0,
                    Leave = 0
                };

                _context.AttendanceSummary.Add(summary);
            }

            // 🔻 REMOVE OLD STATUS
            if (!string.IsNullOrEmpty(oldStatus))
            {
                switch (oldStatus.ToLower())
                {
                    case "present":
                        summary.Present--;
                        break;
                    case "absent":
                        summary.Absent--;
                        break;
                    case "late":
                        summary.Late--;
                        break;
                    case "leave":
                        summary.Leave--;
                        break;
                }
            }

            // 🔺 ADD NEW STATUS
            if (!string.IsNullOrEmpty(newStatus))
            {
                switch (newStatus.ToLower())
                {
                    case "present":
                        summary.Present++;
                        break;
                    case "absent":
                        summary.Absent++;
                        break;
                    case "late":
                        summary.Late++;
                        break;
                    case "leave":
                        summary.Leave++;
                        break;
                }
            }

            // 🛡️ SAFETY (never go negative)
            summary.Present = Math.Max(0, summary.Present);
            summary.Absent = Math.Max(0, summary.Absent);
            summary.Late = Math.Max(0, summary.Late);
            summary.Leave = Math.Max(0, summary.Leave);
        }

    }
}
