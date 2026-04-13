using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.Interfaces;

namespace AhmadHRMSBackend.DataAccessLayer.Performances
{
    public class PerformancesRepository: IPerformances
    {
        private readonly AppDbContext _context;

        public PerformancesRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
