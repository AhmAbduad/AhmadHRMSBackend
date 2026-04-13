using AhmadHRMSBackend.Data;
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
    }
}
