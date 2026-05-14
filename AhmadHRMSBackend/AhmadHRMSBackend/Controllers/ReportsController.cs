using AhmadHRMSBackend.dto.Reports;
using AhmadHRMSBackend.Services.Performances;
using AhmadHRMSBackend.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        public readonly ReportsService _service;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(ReportsService service, ILogger<ReportsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetReportTypes")]
        public async Task<IActionResult> GetReportTypes()
        {
            _logger.LogInformation("GetReportTypes request received");

            try
            {
                var result = await _service.GetReportTypes();
                _logger.LogInformation("Successfully retrieved report types");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetReportTypes");
                throw;
            }
        }

        [HttpGet("GetReportPeriods")]
        public async Task<IActionResult> GetReportPeriods()
        {
            _logger.LogInformation("GetReportPeriods request received");

            try
            {
                var result = await _service.GetReportPeriods();
                _logger.LogInformation("Successfully retrieved report periods");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetReportPeriods");
                throw;
            }
        }

        [HttpGet("GetDepartmentForReport")]
        public async Task<IActionResult> GetDepartmentForReport()
        {
            _logger.LogInformation("GetDepartmentForReport request received");

            try
            {
                var result = await _service.GetDepartmentForReport();
                _logger.LogInformation("Successfully retrieved departments for report");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDepartmentForReport");
                throw;
            }
        }

        [HttpPost("GetReportsList")]
        public async Task<IActionResult> GetReportsList([FromBody] GetReportListDto dto)
        {
            _logger.LogInformation("GetReportsList request received for report type: {ReportType}", dto.reporttype);

            try
            {
                var result = await _service.GetReportsList(dto);
                _logger.LogInformation("Successfully retrieved reports list");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetReportsList for report type: {ReportType}", dto.reporttype);
                throw;
            }
        }

        [HttpGet("GetReportStatus")]
        public async Task<IActionResult> GetReportStatus()
        {
            _logger.LogInformation("GetReportStatus request received");

            try
            {
                var result = await _service.GetReportStatus();
                _logger.LogInformation("Successfully retrieved report status");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetReportStatus");
                throw;
            }
        }

        [HttpPost("SubmitReportList")]
        public async Task<IActionResult> SubmitReportList([FromForm] SubmitReportListDto dto)
        {
            _logger.LogInformation("SubmitReportList request received for report type: {ReportType}", dto.reportTypeId);

            try
            {
                var result = await _service.SubmitReportList(dto);
                _logger.LogInformation("Successfully submitted report list for report type: {ReportType}", dto.reportTypeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubmitReportList for report type: {ReportType}", dto.reportTypeId);
                throw;
            }
        }
    }
}
