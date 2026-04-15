using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.LeaveEmployee;
using AhmadHRMSBackend.dto.Performance;
using AhmadHRMSBackend.Interfaces;
using AhmadHRMSBackend.Models.Performance;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.Performances
{
    public class PerformancesRepository: IPerformances
    {
        private readonly AppDbContext _context;

        public PerformancesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetPerformancePeriodDto>> GetPerfromancePeriod()
        {
            var data = await _context.PerformancePeriod
            .Where(p => !p.IsDeleted)
            .Select(p => new GetPerformancePeriodDto
            {
                PeriodID = p.PerformancePeriodId,
                PeriodName = p.PeriodName
            })
            .ToListAsync();

            return data;
        }

        public async Task<List<DepartmentDto>> GetDepartmentForPerformance()
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

        public async Task<List<GetPerformanceDataDto>> GetPerformanceData(PeriodnameDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.periodname))
                return new List<GetPerformanceDataDto>();

            var data = await _context.Performance
                .Include(p => p.Employee)
                .ThenInclude(e => e.Departments)
                .Include(p => p.PerformancePeriod)
                .Include(p => p.PerformanceGoal)
                .Include(p => p.PerformanceAcheivement)
                .Where(p =>
                    !p.IsDeleted &&
                    !p.Employee.IsDeleted &&
                    !p.PerformancePeriod.IsDeleted &&
                    p.PerformancePeriod.PeriodName == dto.periodname
                )
                .Select(p => new GetPerformanceDataDto
                {
                    id = p.PerformanceID,
                    employee = p.Employee.Name,
                    department = p.Employee.Departments.Label,
                    position = p.Employee.Position.PositionName, // ⚠️ ensure column exists
                    rating = p.Rating,

                    goals = p.PerformanceGoal
                        .Where(g => !g.IsDeleted)
                        .Select(g => g.GoalText)
                        .ToList(),

                    achievements = p.PerformanceAcheivement
                        .Where(a => !a.IsDeleted)
                        .Select(a => a.AchievementText)
                        .ToList(),

                    reviewDate = p.ReviewDate,
                    nextReview = p.NextReview,
                    avatar = p.Employee.avatar
                })
                .ToListAsync();

            return data;
        }

        public async Task<List<LeaveEmployeeDto>> GetEmployeesForPerformance()
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

        public async Task<bool> SubmitPerformanceData(SubmitPerformanceDataDto dto)
        {
            if (dto == null)
                return false;

            // ✅ Check Employee
            var employee = await _context.EmployeeList
                .FirstOrDefaultAsync(e => e.EmployeeID == dto.employeeId && !e.IsDeleted);

            if (employee == null)
                return false;

            // ✅ Check Period
            var period = await _context.PerformancePeriod
                .FirstOrDefaultAsync(p => p.PerformancePeriodId == dto.periodId && !p.IsDeleted);

            if (period == null)
                return false;

            // 🔥 Check Existing Performance
            var existing = await _context.Performance
                .Include(p => p.PerformanceGoal)
                .Include(p => p.PerformanceAcheivement)
                .FirstOrDefaultAsync(p =>
                    p.EmployeeId == dto.employeeId &&
                    p.PeriodId == dto.periodId &&
                    !p.IsDeleted);

            Performance performance;

            if (existing != null)
            {
                // ✅ UPDATE
                performance = existing;

                performance.Rating = dto.rating;
                performance.ReviewDate = dto.reviewDate;
                performance.NextReview = dto.nextReview;

                // 🔥 Remove old goals & achievements
                _context.PerformanceGoal.RemoveRange(existing.PerformanceGoal);
                _context.PerformanceAchievement.RemoveRange(existing.PerformanceAcheivement);

                await _context.SaveChangesAsync();
            }
            else
            {
                // ✅ INSERT
                performance = new Performance
                {
                    EmployeeId = dto.employeeId,
                    PeriodId = dto.periodId,
                    Rating = dto.rating,
                    ReviewDate = dto.reviewDate,
                    NextReview = dto.nextReview
                };

                _context.Performance.Add(performance);
                await _context.SaveChangesAsync(); // get ID
            }

            // ✅ Insert new Goals
            if (dto.goals != null && dto.goals.Any())
            {
                var goals = dto.goals.Select(g => new PerformanceGoal
                {
                    PerformanceId = performance.PerformanceID,
                    GoalText = g
                }).ToList();

                _context.PerformanceGoal.AddRange(goals);
            }

            // ✅ Insert new Achievements
            if (dto.achievements != null && dto.achievements.Any())
            {
                var achievements = dto.achievements.Select(a => new PerformanceAchievement
                {
                    PerformanceId = performance.PerformanceID,
                    AchievementText = a
                }).ToList();

                _context.PerformanceAchievement.AddRange(achievements);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
