using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.Performance;
using AhmadHRMSBackend.Interfaces;
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
    }
}
