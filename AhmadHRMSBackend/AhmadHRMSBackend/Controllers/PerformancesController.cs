using AhmadHRMSBackend.dto.Performance;
using AhmadHRMSBackend.Services.Payroll;
using AhmadHRMSBackend.Services.Performances;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        public readonly PerformancesService _service;

        public PerformancesController(PerformancesService service)
        {
            _service = service;
        }

        [HttpGet("GetPerfromancePeriod")]
        public async Task<IActionResult> GetPerfromancePeriod()
        {
            var result = await _service.GetPerfromancePeriod();
            return Ok(result);
        }

        [HttpGet("GetDepartmentForPerformance")]
        public async Task<IActionResult> GetDepartmentForPerformance()
        {
            var result = await _service.GetDepartmentForPerformance();
            return Ok(result);
        }

        [HttpPost("GetPerformanceData")]
        public async Task<IActionResult> GetPerformanceData([FromBody] PeriodnameDto dto )
        {
            var result = await _service.GetPerformanceData(dto);
            return Ok(result);
        }

    }
}
