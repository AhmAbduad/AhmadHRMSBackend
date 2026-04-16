using AhmadHRMSBackend.dto.Department;
using AhmadHRMSBackend.dto.Reports;
using AhmadHRMSBackend.UnitofWork;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Services.Reports
{
    public class ReportsService
    {
        private readonly IUnitofWork _unitOfWork;

        public ReportsService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ReportTypesDto>> GetReportTypes()
        {
            var result = await _unitOfWork.Reports.GetReportTypes();
            return result;
        }

        public async Task<List<ReportPeriodsDto>> GetReportPeriods()
        {
            var result = await _unitOfWork.Reports.GetReportPeriods();
            return result;
        }

        public async Task<List<DepartmentDto>> GetDepartmentForReport()
        {
            var result = await _unitOfWork.Reports.GetDepartmentForReport();
            return result;
        }

        public async Task<List<ReturnReportListDto>> GetReportsList(GetReportListDto dto)
        {
            var result = await _unitOfWork.Reports.GetReportsList(dto);
            return result;
        }

        public async Task<List<ReportStatusDto>> GetReportStatus()
        {
            var result = await _unitOfWork.Reports.GetReportStatus();
            return result;
        }

        public async Task<bool> SubmitReportList(SubmitReportListDto dto)
        {
            var result = await _unitOfWork.Reports.SubmitReportList(dto);
            return result;
        }
    }
}
