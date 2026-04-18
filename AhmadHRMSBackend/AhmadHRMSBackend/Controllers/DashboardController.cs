using AhmadHRMSBackend.dto.Dashboard;
using AhmadHRMSBackend.Services.Dashboard;
using AhmadHRMSBackend.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        public readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;
        }

        [HttpGet("GetEmployeesForDashboard")]
        public async Task<IActionResult> GetEmployeesForDashboard()
        {
            var result = await _service.GetEmployeesForDashboard();
            return Ok(result);
        }

        [HttpGet("GetLeaveRequestForDashboard")]
        public async Task<IActionResult> GetLeaveRequestForDashboard()
        {
            var result = await _service.GetLeaveRequestForDashboard();
            return Ok(result);
        }

        [HttpPost("GetAttendanceDataForDashboard")]
        public async Task<IActionResult> GetAttendanceDataForDashboard([FromBody] AttendanceDataMonthDto dto)
        {
            var result = await _service.GetAttendanceDataForDashboard(dto);
            return Ok(result);
        }

        [HttpGet("GetPerformanceDataForDashboard")]
        public async Task<IActionResult> GetPerformanceDataForDashboard()
        {
            var result = await _service.GetPerformanceDataForDashboard();
            return Ok(result);
        }


    }
}
