using AhmadHRMSBackend.dto.GetMarkAttendance;
using AhmadHRMSBackend.dto.SaveAttendance;
using AhmadHRMSBackend.Services.EmployeeList;
using AhmadHRMSBackend.Services.MarkAttendance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MarkAttendanceController : ControllerBase
    {
        public readonly MarkAttendanceService _service;
        private readonly ILogger<MarkAttendanceController> _logger;


        public MarkAttendanceController(MarkAttendanceService service, ILogger<MarkAttendanceController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet("GetMarkAttendanceRecord")]
        public async Task<IActionResult> GetMarkAttendanceRecord([FromQuery] GetMarkAttendanceDto dto)
        {
            _logger.LogInformation("GetMarkAttendanceRecord request received for date: {Date}", dto.date);

            try
            {
                var markattendancerecord = await _service.GetMarkAttendanceRecord(dto);
                _logger.LogInformation("Successfully retrieved mark attendance record");
                return Ok(markattendancerecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetMarkAttendanceRecord for date: {Date}", dto.date);
                throw;
            }
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            _logger.LogInformation("GetDepartments request received");

            try
            {
                var departments = await _service.GetDepartments();
                _logger.LogInformation("Successfully retrieved departments");
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDepartments");
                throw;
            }
        }

        [HttpPost("SaveMarkAttendance")]
        public async Task<IActionResult> SaveMarkAttendance([FromBody] SaveAttendanceDto dto)
        {
            _logger.LogInformation("SaveMarkAttendance request received for date: {Date}", dto.Date);

            try
            {
                var savemarkattendance = await _service.SaveMarkAttendance(dto);
                _logger.LogInformation("Successfully saved mark attendance for date: {Date}", dto.Date);
                return Ok(savemarkattendance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveMarkAttendance for date: {Date}", dto.Date);
                throw;
            }
        }

    }
}
