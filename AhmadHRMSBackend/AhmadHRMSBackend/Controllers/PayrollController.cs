using AhmadHRMSBackend.dto.Payroll;
using AhmadHRMSBackend.Services.Payroll;
using AhmadHRMSBackend.Services.TimeSheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        public readonly PayrollService _service;
        private readonly ILogger<PayrollController> _logger;

        public PayrollController(PayrollService service, ILogger<PayrollController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("GetPayrollRequest")]
        public async Task<IActionResult> GetPayrollRequest([FromBody] GetPayrollRequestDto dto)
        {
            _logger.LogInformation("GetPayrollRequest request received for month: {Month}", dto.Month);

            try
            {
                var result = await _service.GetPayrollRequest(dto);
                _logger.LogInformation("Successfully retrieved payroll request");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPayrollRequest for month: {Month}", dto.Month);
                throw;
            }
        }

        [HttpGet("GetPayrollStatus")]
        public async Task<IActionResult> GetPayrollStatus()
        {
            _logger.LogInformation("GetPayrollStatus request received");

            try
            {
                var result = await _service.GetPayrollStatus();
                _logger.LogInformation("Successfully retrieved payroll status");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPayrollStatus");
                throw;
            }
        }

        [HttpGet("GetEmployeeForPayroll")]
        public async Task<IActionResult> GetEmployeeForPayroll()
        {
            _logger.LogInformation("GetEmployeeForPayroll request received");

            try
            {
                var result = await _service.GetEmployeeForPayroll();
                _logger.LogInformation("Successfully retrieved employees for payroll");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployeeForPayroll");
                throw;
            }
        }

        [HttpPost("SubmitPayrollRequest")]
        public async Task<IActionResult> SubmitPayrollRequest([FromBody] SubmitPayrollRequestDto dto)
        {
            _logger.LogInformation("SubmitPayrollRequest request received for month");

            try
            {
                var result = await _service.SubmitPayrollRequest(dto);
                _logger.LogInformation("Successfully submitted payroll request for month");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubmitPayrollRequest for month:");
                throw;
            }
        }

        [HttpPost("ChangePayrollStatus")]
        public async Task<IActionResult> ChangePayrollStatus(ChangePayrollStatusDto dto)
        {
            _logger.LogInformation("ChangePayrollStatus request received for payroll ID: {PayrollId}", dto.payrollRequestId);

            try
            {
                var result = await _service.ChangePayrollStatus(dto);
                _logger.LogInformation("Successfully changed payroll status for payroll ID: {PayrollId}", dto.payrollRequestId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ChangePayrollStatus for payroll ID: {PayrollId}", dto.payrollRequestId);
                throw;
            }
        }
    }
}
