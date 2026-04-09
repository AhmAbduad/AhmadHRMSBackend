using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.LeaveEmployee;
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

            //if (dto.TimesheetId.HasValue)
            //    query = query.Where(t => t.TimesheetId == dto.TimesheetId.Value);

            if (dto.EmployeeId.HasValue)
                query = query.Where(t => t.EmployeeId == dto.EmployeeId.Value);

            if (dto.Year.HasValue)
                query = query.Where(t => t.WeekStartDate.Year == dto.Year.Value);

            if (dto.Month.HasValue)
                query = query.Where(t => t.WeekStartDate.Month == dto.Month.Value);

            if (dto.Week.HasValue)
            {
                //query = query
                //    .OrderBy(t => t.WeekStartDate)
                //    .Skip((dto.Week.Value - 1))
                //    .Take(1);

                query = query.Where(t =>
                    t.TimesheetDetails.Any(d =>
                    d.Week == dto.Week.Value && !d.IsDeleted));
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

        public async Task<List<LeaveEmployeeDto>> GetEmployeesForTimeSheet()
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

        public async Task<bool> SaveTimeSheet(SaveTimeSheetDto dto)
        {
            if (dto == null)
                return false;

            // ✅ Check Employee exists
            var employee = await _context.EmployeeList
                .FirstOrDefaultAsync(e => e.EmployeeID == dto.EmployeeId && !e.IsDeleted);

            if (employee == null)
                return false;

            // ✅ Prevent duplicate timesheet for same week
            var existingTimesheet = await _context.Timesheets
                .Include(t => t.TimesheetDetails)
                .FirstOrDefaultAsync(t =>
                    t.EmployeeId == dto.EmployeeId &&
                    t.WeekStartDate == dto.WeekStartDate &&
                    !t.IsDeleted);

            if (existingTimesheet != null)
            {
                // 🔥 Update existing (optional - clean approach)
                _context.TimesheetDetails.RemoveRange(existingTimesheet.TimesheetDetails);
                _context.Timesheets.Remove(existingTimesheet);
                await _context.SaveChangesAsync();
            }

            // 🔹 Helper function
            List<TimesheetDetails> details = new List<TimesheetDetails>();

            decimal totalHours = 0;
            decimal regularHours = 0;
            decimal overtimeHours = 0;
            int daysWorked = 0;

            void AddDay(string dayName, DateTime date, DayDto day)
            {
                if (day == null)
                    return;

                decimal hoursDecimal = day.hours + (day.minutes / 60.0m);

                totalHours += hoursDecimal;

                if (day.status?.ToLower() == "regular")
                    regularHours += hoursDecimal;

                if (day.status?.ToLower() == "overtime")
                    overtimeHours += hoursDecimal;

                if (day.status?.ToLower() != "off")
                    daysWorked++;

                details.Add(new TimesheetDetails
                {
                    WorkDate = date,
                    DayName = dayName,
                    Hours = day.hours,
                    Minutes = day.minutes,
                    Status = day.status,
                    Week = GetWeekOfMonth(date)
                });
            }

            // ✅ Create Timesheet
            var timesheet = new Timesheets
            {
                EmployeeId = dto.EmployeeId,
                WeekStartDate = dto.WeekStartDate,
                WeekEndDate = dto.WeekEndDate,
                TotalHours = 0,
                RegularHours = 0,
                OvertimeHours = 0,
                DaysWorked = 0
            };

            _context.Timesheets.Add(timesheet);
            await _context.SaveChangesAsync(); // 🔥 get TimesheetId

            // ✅ Add Days
            AddDay("Monday", dto.WeekStartDate.AddDays(0), dto.Monday);
            AddDay("Tuesday", dto.WeekStartDate.AddDays(1), dto.Tuesday);
            AddDay("Wednesday", dto.WeekStartDate.AddDays(2), dto.Wednesday);
            AddDay("Thursday", dto.WeekStartDate.AddDays(3), dto.Thursday);
            AddDay("Friday", dto.WeekStartDate.AddDays(4), dto.Friday);
            AddDay("Saturday", dto.WeekStartDate.AddDays(5), dto.Saturday);
            AddDay("Sunday", dto.WeekStartDate.AddDays(6), dto.Sunday);

            // ✅ Assign FK
            details.ForEach(d => d.TimesheetId = timesheet.TimesheetId);

            _context.TimesheetDetails.AddRange(details);

            // ✅ Update totals
            timesheet.TotalHours = totalHours;
            timesheet.RegularHours = regularHours;
            timesheet.OvertimeHours = overtimeHours;
            timesheet.DaysWorked = daysWorked;

            _context.Timesheets.Update(timesheet);

            await _context.SaveChangesAsync();

            return true;
        }


        private int GetWeekOfMonth(DateTime date)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);

            // Offset for first week
            int offset = (int)firstDayOfMonth.DayOfWeek;

            return ((date.Day + offset - 1) / 7) + 1;
        }
    }
}
