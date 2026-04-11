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

    }
}
