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

        public async Task<List<ReturnReportListDto>> GetReportsList(GetReportListDto dto)
        {
            var query = _context.Reports
            .Include(r => r.ReportType)
            .Include(r => r.ReportPeriod)
            .Include(r => r.ReportStatus)
            .Include(r => r.Departments)
            .Where(r => !r.IsDeleted)
            .AsQueryable();

            // 🔥 Dynamic Filters

            if (!string.IsNullOrEmpty(dto.reporttype) && dto.reporttype.ToLower() != "all")
            {
                query = query.Where(r => r.ReportType.ReportTypeName.ToLower() == dto.reporttype.ToLower());
            }

            if (!string.IsNullOrEmpty(dto.reportperiod) && dto.reportperiod.ToLower() != "all")
            {
                query = query.Where(r => r.ReportPeriod.ReportPeriodName.ToLower() == dto.reportperiod.ToLower());
            }

            if (!string.IsNullOrEmpty(dto.reportdepartment) && dto.reportdepartment.ToLower() != "all")
            {
                query = query.Where(r => r.Departments.Value.ToLower() == dto.reportdepartment.ToLower());
            }

            var data = await query.ToListAsync();

            // 🔹 Mapping
            var result = data.Select(r => new ReturnReportListDto
            {
                id = r.ReportId,
                name = r.ReportName,
                type = r.ReportType?.ReportTypeName,
                period = r.ReportPeriod?.ReportPeriodName,
                department = r.Departments?.Label,
                generatedDate = r.GeneratedDate ?? DateTime.MinValue,
                generatedBy = r.GeneratedBy ?? "--",
                status = r.ReportStatus?.ReportStatusName,
                fileSize = r.FileSize ?? "--",
                format = r.Format ?? "--",
                description = r.Description ?? "--"
            }).ToList();

            return result;
        }
    }
}
