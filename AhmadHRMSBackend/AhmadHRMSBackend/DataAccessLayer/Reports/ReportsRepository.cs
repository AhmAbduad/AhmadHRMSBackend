using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.Interfaces;

namespace AhmadHRMSBackend.DataAccessLayer.Reports
{
    public class ReportsRepository:IReports
    {
        private readonly AppDbContext _context;

        public ReportsRepository(AppDbContext context)
        {
            _context = context;
        }


    }
}
