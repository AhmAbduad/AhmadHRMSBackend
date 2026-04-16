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

        public async Task<List<ReportStatusDto>> GetReportStatus()
        {
            var result = await _context.ReportStatus
            .Where(x => !x.IsDeleted)
            .Select(x => new ReportStatusDto
            {
                id = x.ReportStatusId,
                value = x.ReportStatusName.ToLower(), // e.g. monthly
                label = x.ReportStatusName            // e.g. Monthly
            })
            .ToListAsync();

            return result;
        }

        public async Task<bool> SubmitReportList(SubmitReportListDto dto)
        {
            if (dto == null)
                return false;

            // ✅ Validate Foreign Keys (optional but best practice)
            var typeExists = await _context.ReportTypes
                .AnyAsync(x => x.ReportTypeId == dto.reportTypeId && !x.IsDeleted);

            var periodExists = await _context.ReportPeriods
                .AnyAsync(x => x.ReportPeriodId == dto.reportPeriodId && !x.IsDeleted);

            var deptExists = await _context.Departments
                .AnyAsync(x => x.DepartmentsID == dto.departmentId && !x.IsDeleted);

            var statusExists = await _context.ReportStatus
                .AnyAsync(x => x.ReportStatusId == dto.reportStatusId && !x.IsDeleted);

            if (!typeExists || !periodExists || !deptExists || !statusExists)
                return false;

            //// 🔹 Calculate File Size & Format
            //string fileSize = null;
            //string format = null;

            //if (dto.fileData != null && dto.fileData.Length > 0)
            //{
            //    fileSize = $"{(dto.fileData.Length / 1024.0):0.00} KB";

            //    // Optional: simple format detection (basic)
            //    format = "FILE";
            //}

            // ✅ Convert file to byte[]
            byte[] fileBytes = null;

            if (dto.fileData != null && dto.fileData.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await dto.fileData.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
            }

            // 🔥 File Size (Direct from IFormFile)
            string fileSize = dto.fileData != null
                ? GetFileSize(dto.fileData.Length)
                : "0 KB";

            // 🔥 File Format (BEST way from ContentType)
            string format = dto.fileData != null
                ? GetFileFormatFromContentType(dto.fileData.ContentType)
                : "unknown";

            // ✅ Create Report
            var report = new AhmadHRMSBackend.Models.Reports.Reports
            {
                ReportName = dto.reportName,
                ReportTypeId = dto.reportTypeId,
                ReportPeriodId = dto.reportPeriodId,
                DepartmentId = dto.departmentId,
                ReportStatusId = dto.reportStatusId,

                GeneratedDate = dto.generatedDate,
                GeneratedBy = dto.generatedBy,
                Description = dto.description,

                FileData = fileBytes,
                FileSize = fileSize,
                Format = format,
                IsDeleted = false
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return true;
        }

        private string GetFileSize(long length)
        {
            if (length < 1024)
                return $"{length} B";

            if (length < 1024 * 1024)
                return $"{(length / 1024.0):0.00} KB";

            return $"{(length / (1024.0 * 1024)):0.00} MB";
        }

        private string GetFileFormatFromContentType(string contentType)
        {
            return contentType switch
            {
                "application/pdf" => "PDF",
                "image/png" => "PNG",
                "image/jpeg" => "JPG",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => "Excel",
                "application/vnd.ms-excel" => "Excel",
                _ => "unknown"
            };
        }
    }
}
