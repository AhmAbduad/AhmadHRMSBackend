using AhmadHRMSBackend.dto.GetMarkAttendance;
using AhmadHRMSBackend.dto.SaveAttendance;
using AhmadHRMSBackend.Services.EmployeeList;
using AhmadHRMSBackend.Services.MarkAttendance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MarkAttendanceController : ControllerBase
    {
        public readonly MarkAttendanceService _service;

        public MarkAttendanceController(MarkAttendanceService service)
        {
            _service = service;
        }


        [HttpGet("GetMarkAttendanceRecord")]
        public async Task<IActionResult> GetMarkAttendanceRecord([FromQuery] GetMarkAttendanceDto dto)
        {
            var markattendancerecord = await _service.GetMarkAttendanceRecord(dto);

            return Ok(markattendancerecord);
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _service.GetDepartments();
            return Ok(departments);
        }

        [HttpPost("SaveMarkAttendance")]
        public async Task<IActionResult> SaveMarkAttendance([FromBody] SaveAttendanceDto dto)
        {
            var savemarkattendance = await _service.SaveMarkAttendance(dto);

            return Ok(savemarkattendance);
        }

    }
}
