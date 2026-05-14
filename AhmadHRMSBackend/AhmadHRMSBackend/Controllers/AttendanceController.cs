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
        private readonly ILogger<AttendanceController> _logger;


        public AttendanceController(AttendanceService service, ILogger<AttendanceController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet("GetAttendanceInfo")]
        public async Task<IActionResult> GetAttendanceInfo()
        {
            _logger.LogInformation("GetAttendanceInfo request received");

            try
            {
                var attendanceinfo = await _service.GetAttendanceInfo();
                _logger.LogInformation("Successfully retrieved attendance info");

                return Ok(attendanceinfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAttendanceInfo");
                throw;
            }   
        }


        [HttpPost("GetAttendanceRecord")]
        public async Task<IActionResult> GetAttendanceRecord([FromBody] AttendanceRecordMonthDto dto)
        {

            _logger.LogInformation("GetAttendanceRecord request received for month:");
            try
            {
                var attendancerecord = await _service.GetAttendanceRecord(dto);

                _logger.LogInformation("Successfully retrieved attendance record");

                return Ok(attendancerecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAttendanceRecord for month");
                throw;
            }
        }


        [HttpGet("GetAttendanceSummary/{id}")]
        public async Task<IActionResult> GetAttendanceSummary(int id)
        {
            _logger.LogInformation("GetAttendanceSummary request received for employee ID: {Id}", id);


            try
            {
                var attendancesummary = await _service.GetAttendanceSummary(id);
                _logger.LogInformation("Successfully retrieved attendance summary for employee ID: {Id}", id);
                return Ok(attendancesummary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAttendanceSummary for employee ID: {Id}", id);
                throw;
            }
        }

        [HttpGet("GetDepartmentForAttendance")]
        public async Task<IActionResult> GetDepartmentForAttendance()
        {
            _logger.LogInformation("GetDepartmentForAttendance request received");

            try
            {
                var result = await _service.GetDepartmentForAttendance();
                _logger.LogInformation("Successfully retrieved departments for attendance");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDepartmentForAttendance");
                throw;
            }
        }

    }
}
