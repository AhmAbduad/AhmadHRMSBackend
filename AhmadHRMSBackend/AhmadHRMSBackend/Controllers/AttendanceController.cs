using AhmadHRMSBackend.Services.Attendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        public readonly AttendanceService _service;

        public AttendanceController(AttendanceService service)
        {
            _service = service;
        }


        [HttpGet("GetAttendanceInfo")]
        public async Task<IActionResult> GetAttendanceInfo()
        {
            var employees = await _service.GetAttendanceInfo();

            return Ok(employees);
        }
    }
}
