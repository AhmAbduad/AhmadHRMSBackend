using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.Reports;

namespace AhmadHRMSBackend.Interfaces
{
    public interface IReports
    {
        Task<List<ReportTypesDto>> GetReportTypes();

        Task<List<ReportPeriodsDto>> GetReportPeriods();

        Task<List<DepartmentDto>> GetDepartmentForReport();

        Task<List<ReturnReportListDto>> GetReportsList(GetReportListDto dto);
    }
}
