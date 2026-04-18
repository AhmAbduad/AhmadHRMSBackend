using AhmadHRMSBackend.dto.Reports;
using AhmadHRMSBackend.Services.Performances;
using AhmadHRMSBackend.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        public readonly ReportsService _service;

        public ReportsController(ReportsService service)
        {
            _service = service;
        }

        [HttpGet("GetReportTypes")]
        public async Task<IActionResult> GetReportTypes()
        {
            var result = await _service.GetReportTypes();
            return Ok(result);
        }

        [HttpGet("GetReportPeriods")]
        public async Task<IActionResult> GetReportPeriods()
        {
            var result = await _service.GetReportPeriods();
            return Ok(result);
        }

        [HttpGet("GetDepartmentForReport")]
        public async Task<IActionResult> GetDepartmentForReport()
        {
            var result = await _service.GetDepartmentForReport();
            return Ok(result);
        }

        [HttpPost("GetReportsList")]
        public async Task<IActionResult> GetReportsList([FromBody] GetReportListDto dto)
        {
            var result = await _service.GetReportsList(dto);
            return Ok(result);
        }

        [HttpGet("GetReportStatus")]
        public async Task<IActionResult> GetReportStatus()
        {
            var result = await _service.GetReportStatus();
            return Ok(result);
        }

        [HttpPost("SubmitReportList")]
        public async Task<IActionResult> SubmitReportList([FromForm] SubmitReportListDto dto)
        {
            var result = await _service.SubmitReportList(dto);
            return Ok(result);
        }
    }
}
