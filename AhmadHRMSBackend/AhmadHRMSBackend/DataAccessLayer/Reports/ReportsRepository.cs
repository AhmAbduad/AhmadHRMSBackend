using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.Reports;
using AhmadHRMSBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AhmadHRMSBackend.DataAccessLayer.Reports
{
    public class ReportsRepository:IReports
    {
        private readonly AppDbContext _context;

        public ReportsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReportTypesDto>> GetReportTypes()
        {
            var result = await _context.ReportTypes
                .Where(x => !x.IsDeleted)
                .Select(x => new ReportTypesDto
                {
                    id = x.ReportTypeId,
                    value = x.ReportTypeName.ToLower(),  // for dropdown value
                    label = x.ReportTypeName             // for UI display
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<ReportPeriodsDto>> GetReportPeriods()
        {
            var result = await _context.ReportPeriods
            .Where(x => !x.IsDeleted)
            .Select(x => new ReportPeriodsDto
            {
                id = x.ReportPeriodId,
                value = x.ReportPeriodName.ToLower(), // e.g. monthly
                label = x.ReportPeriodName            // e.g. Monthly
            })
            .ToListAsync();
            return result;
        }

        public async Task<List<DepartmentDto>> GetDepartmentForReport()
        {
            var result = await _context.Departments
            .Where(x => !x.IsDeleted)
            .Select(x => new DepartmentDto
            {
                id = x.DepartmentsID,
                Value = x.Value.ToLower(), // e.g. monthly
                Label = x.Label            // e.g. Monthly
            })
            .ToListAsync();

            return result;
        }
    }
}
