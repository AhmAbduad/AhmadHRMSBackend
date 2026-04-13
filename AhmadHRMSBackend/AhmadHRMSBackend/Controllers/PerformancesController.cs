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

        //[HttpGet("GetPerfromancePeriod")]
        //public async Task<IActionResult> GetPerfromancePeriod()
        //{

        //}
    }
}
