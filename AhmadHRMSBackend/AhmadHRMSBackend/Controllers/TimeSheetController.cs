using AhmadHRMSBackend.dto.TimeSheetDetails;
using AhmadHRMSBackend.Services.Leave;
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
    public class TimeSheetController : ControllerBase
    {

        public readonly TimeSheetService _service;
        private readonly ILogger<TimeSheetController> _logger;

        public TimeSheetController(TimeSheetService service, ILogger<TimeSheetController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpPost("GetTimeSheetDetail")]
        public async Task<IActionResult> GetTimeSheetDetail([FromBody] GetTimesheetDto dto)
        {
            _logger.LogInformation("GetTimeSheetDetail request received for employee ID: {EmployeeId}", dto.EmployeeId);

            try
            {
                var result = await _service.GetTimeSheetDetail(dto);
                _logger.LogInformation("Successfully retrieved timesheet detail for employee ID: {EmployeeId}", dto.EmployeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetTimeSheetDetail for employee ID: {EmployeeId}", dto.EmployeeId);
                throw;
            }
        }


        [HttpGet("GetEmployeesForTimeSheet")]
        public async Task<IActionResult> GetEmployeesForTimeSheet()
        {
            _logger.LogInformation("GetEmployeesForTimeSheet request received");

            try
            {
                var result = await _service.GetEmployeesForTimeSheet();
                _logger.LogInformation("Successfully retrieved employees for timesheet");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployeesForTimeSheet");
                throw;
            }
        }

        [HttpPost("SaveTimeSheet")]
        public async Task<IActionResult> SaveTimeSheet([FromBody] SaveTimeSheetDto dto)
        {
            _logger.LogInformation("SaveTimeSheet request received for employee ID: {EmployeeId}", dto.EmployeeId);

            try
            {
                var result = await _service.SaveTimeSheet(dto);
                _logger.LogInformation("Successfully saved timesheet for employee ID: {EmployeeId}", dto.EmployeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveTimeSheet for employee ID: {EmployeeId}", dto.EmployeeId);
                throw;
            }
        }
    }
}
