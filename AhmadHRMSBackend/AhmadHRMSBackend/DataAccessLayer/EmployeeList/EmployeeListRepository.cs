using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.EmployeeList
{
    public class EmployeeListRepository: IEmployeeList
    {
        private readonly AppDbContext _context;

        public EmployeeListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AhmadHRMSBackend.Models.EmployeeList.EmployeeList>> GetAllEmployees()
        {
            return await _context.EmployeeList
              .Include(e => e.Departments)
              .Include(e => e.Position)
              .Include(e => e.Status)
              .ToListAsync();
        }

    }
}
