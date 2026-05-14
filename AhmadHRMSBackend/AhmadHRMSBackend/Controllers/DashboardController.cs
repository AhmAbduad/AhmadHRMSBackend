using AhmadHRMSBackend.dto.Dashboard;
using AhmadHRMSBackend.Services.Dashboard;
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
    public class DashboardController : ControllerBase
    {
        public readonly DashboardService _service;
        private readonly ILogger<DashboardController> _logger;


        public DashboardController(DashboardService service, ILogger<DashboardController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetEmployeesForDashboard")]
        public async Task<IActionResult> GetEmployeesForDashboard()
        {
            _logger.LogInformation("GetEmployeesForDashboard request received");

            try
            {
                var result = await _service.GetEmployeesForDashboard();
                _logger.LogInformation("Successfully retrieved employees for dashboard");
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployeesForDashboard");
                throw;
            }          
        }

        [HttpGet("GetLeaveRequestForDashboard")]
        public async Task<IActionResult> GetLeaveRequestForDashboard()
        {
            _logger.LogInformation("GetLeaveRequestForDashboard request received");

            try
            {
                var result = await _service.GetLeaveRequestForDashboard();
                _logger.LogInformation("Successfully retrieved leave requests for dashboard");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetLeaveRequestForDashboard");
                throw;
            }
        }

        [HttpPost("GetAttendanceDataForDashboard")]
        public async Task<IActionResult> GetAttendanceDataForDashboard([FromBody] AttendanceDataMonthDto dto)
        {
            _logger.LogInformation("GetAttendanceDataForDashboard request received for month");

            try
            {
                var result = await _service.GetAttendanceDataForDashboard(dto);
                _logger.LogInformation("Successfully retrieved attendance data for dashboard");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAttendanceDataForDashboard for month");
                throw;
            }
        }

        [HttpGet("GetPerformanceDataForDashboard")]
        public async Task<IActionResult> GetPerformanceDataForDashboard()
        {
            _logger.LogInformation("GetPerformanceDataForDashboard request received");

            try
            {
                var result = await _service.GetPerformanceDataForDashboard();
                _logger.LogInformation("Successfully retrieved performance data for dashboard");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPerformanceDataForDashboard");
                throw;
            }
        }


    }
}
