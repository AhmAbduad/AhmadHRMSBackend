using AhmadHRMSBackend.dto.Performance;
using AhmadHRMSBackend.Services.Payroll;
using AhmadHRMSBackend.Services.Performances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        public readonly PerformancesService _service;

        private readonly ILogger<PerformancesController> _logger;

        public PerformancesController(PerformancesService service, ILogger<PerformancesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetPerfromancePeriod")]
        public async Task<IActionResult> GetPerfromancePeriod()
        {
            _logger.LogInformation("GetPerfromancePeriod request received");

            try
            {
                var result = await _service.GetPerfromancePeriod();
                _logger.LogInformation("Successfully retrieved performance periods");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPerfromancePeriod");
                throw;
            }
        }

        [HttpGet("GetDepartmentForPerformance")]
        public async Task<IActionResult> GetDepartmentForPerformance()
        {
            _logger.LogInformation("GetDepartmentForPerformance request received");

            try
            {
                var result = await _service.GetDepartmentForPerformance();
                _logger.LogInformation("Successfully retrieved departments for performance");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDepartmentForPerformance");
                throw;
            }
        }

        [HttpPost("GetPerformanceData")]
        public async Task<IActionResult> GetPerformanceData([FromBody] PeriodnameDto dto )
        {
            _logger.LogInformation("GetPerformanceData request received for period: {Period}", dto.periodname);

            try
            {
                var result = await _service.GetPerformanceData(dto);
                _logger.LogInformation("Successfully retrieved performance data for period: {Period}", dto.periodname);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPerformanceData for period: {Period}", dto.periodname);
                throw;
            }
        }

        [HttpGet("GetEmployeesForPerformance")]
        public async Task<IActionResult> GetEmployeesForPerformance()
        {
            _logger.LogInformation("GetEmployeesForPerformance request received");

            try
            {
                var result = await _service.GetEmployeesForPerformance();
                _logger.LogInformation("Successfully retrieved employees for performance");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployeesForPerformance");
                throw;
            }
        }

        [HttpPost("SubmitPerformanceData")]
        public async Task<IActionResult> SubmitPerformanceData([FromBody] SubmitPerformanceDataDto dto)
        {
            _logger.LogInformation("SubmitPerformanceData request received for employee ID: {EmployeeId}", dto.employeeId);

            try
            {
                var result = await _service.SubmitPerformanceData(dto);
                _logger.LogInformation("Successfully submitted performance data for employee ID: {EmployeeId}", dto.employeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubmitPerformanceData for employee ID: {EmployeeId}", dto.employeeId);
                throw;
            }
        }
    }
}
