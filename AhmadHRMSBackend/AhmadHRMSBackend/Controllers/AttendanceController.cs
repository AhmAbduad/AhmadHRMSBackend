using AhmadHRMSBackend.dto.GetAttendanceRecord;
using AhmadHRMSBackend.Services.Attendance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
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
            var attendanceinfo = await _service.GetAttendanceInfo();

            return Ok(attendanceinfo);
        }


        [HttpPost("GetAttendanceRecord")]
        public async Task<IActionResult> GetAttendanceRecord([FromBody] AttendanceRecordMonthDto dto)
        {
            var attendancerecord = await _service.GetAttendanceRecord(dto);

            return Ok(attendancerecord);
        }


        [HttpGet("GetAttendanceSummary/{id}")]
        public async Task<IActionResult> GetAttendanceSummary(int id)
        {
            var attendancesummary = await _service.GetAttendanceSummary(id);
            return Ok(attendancesummary);   
        }

        [HttpGet("GetDepartmentForAttendance")]
        public async Task<IActionResult> GetDepartmentForAttendance()
        {
            var result = await _service.GetDepartmentForAttendance();
            return Ok(result);
        }

    }
}
