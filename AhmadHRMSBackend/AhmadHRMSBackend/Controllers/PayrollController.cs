using AhmadHRMSBackend.dto.Payroll;
using AhmadHRMSBackend.Services.Payroll;
using AhmadHRMSBackend.Services.TimeSheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        public readonly PayrollService _service;

        public PayrollController(PayrollService service)
        {
            _service = service;
        }

        [HttpPost("GetPayrollRequest")]
        public async Task<IActionResult> GetPayrollRequest([FromBody] GetPayrollRequestDto dto)
        {
            var result = await _service.GetPayrollRequest(dto);
            return Ok(result);
        }

        [HttpGet("GetPayrollStatus")]
        public async Task<IActionResult> GetPayrollStatus()
        {
            var result = await _service.GetPayrollStatus();
            return Ok(result);
        }

        [HttpGet("GetEmployeeForPayroll")]
        public async Task<IActionResult> GetEmployeeForPayroll()
        {
            var result = await _service.GetEmployeeForPayroll();
            return Ok(result);
        }

        [HttpPost("SubmitPayrollRequest")]
        public async Task<IActionResult> SubmitPayrollRequest([FromBody] SubmitPayrollRequestDto dto)
        {
            var result = await _service.SubmitPayrollRequest(dto);
            return Ok(result);
        }

        [HttpPost("ChangePayrollStatus")]
        public async Task<IActionResult> ChangePayrollStatus(ChangePayrollStatusDto dto)
        {
            var result = await _service.ChangePayrollStatus(dto);
            return Ok(result);
        }
    }
}
