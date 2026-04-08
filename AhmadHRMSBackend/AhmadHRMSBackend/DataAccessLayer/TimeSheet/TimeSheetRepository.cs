using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.TimeSheetDetails;
using AhmadHRMSBackend.Interfaces;
using AhmadHRMSBackend.Models.TimesheetDetails;
using AhmadHRMSBackend.Models.Timesheets;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.TimeSheet
{
    public class TimeSheetRepository:ITimeSheet
    {

        private readonly AppDbContext _context;

        public TimeSheetRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<TimeSheetDetailsDto>> GetTimeSheetDetail(GetTimesheetDto dto)
        {
            if (dto == null)
                return new List<TimeSheetDetailsDto>();

            IQueryable<Timesheets> query = _context.Timesheets
                .Include(t => t.Employee)
                .Include(t => t.TimesheetDetails)
                .Where(t => !t.IsDeleted);

            // 🔥 Dynamic filters (IMPORTANT)

            if (dto.TimesheetId.HasValue)
                query = query.Where(t => t.TimesheetId == dto.TimesheetId.Value);

            if (dto.EmployeeId.HasValue)
                query = query.Where(t => t.EmployeeId == dto.EmployeeId.Value);

            if (dto.Year.HasValue)
                query = query.Where(t => t.WeekStartDate.Year == dto.Year.Value);

            if (dto.Month.HasValue)
                query = query.Where(t => t.WeekStartDate.Month == dto.Month.Value);

            if (dto.Week.HasValue)
            {
                query = query
                    .OrderBy(t => t.WeekStartDate)
                    .Skip((dto.Week.Value - 1))
                    .Take(1);
            }

            var timesheets = await query.ToListAsync();

            // 🔹 Helper
            DayDto GetDay(List<TimesheetDetails> details, string day)
            {
                var record = details
                    .FirstOrDefault(d => d.DayName.ToLower() == day.ToLower() && !d.IsDeleted);

                return record != null
                    ? new DayDto
                    {
                        hours = record.Hours,
                        minutes = record.Minutes,
                        status = record.Status
                    }
                    : new DayDto
                    {
                        hours = 0,
                        minutes = 0,
                        status = "off"
                    };
            }

            // 🔹 Mapping
            var result = timesheets.Select(t => new TimeSheetDetailsDto
            {
                TimeSheetDetailID = t.TimesheetId,
                employee = t.Employee.Name,
                avatar = t.Employee.avatar,

                monday = GetDay(t.TimesheetDetails.ToList(), "Monday"),
                tuesday = GetDay(t.TimesheetDetails.ToList(), "Tuesday"),
                wednesday = GetDay(t.TimesheetDetails.ToList(), "Wednesday"),
                thursday = GetDay(t.TimesheetDetails.ToList(), "Thursday"),
                friday = GetDay(t.TimesheetDetails.ToList(), "Friday"),
                saturday = GetDay(t.TimesheetDetails.ToList(), "Saturday"),
                sunday = GetDay(t.TimesheetDetails.ToList(), "Sunday"),

                total = $"{t.TotalHours}h"
            }).ToList();

            return result;
        }
    }
}
