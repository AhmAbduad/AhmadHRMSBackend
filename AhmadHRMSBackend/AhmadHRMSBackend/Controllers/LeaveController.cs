using AhmadHRMSBackend.dto.ChangeStatus;
using AhmadHRMSBackend.dto.SaveAttendance;
using AhmadHRMSBackend.dto.SubmitLeaveRequest;
using AhmadHRMSBackend.Services.Leave;
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
    public class LeaveController : ControllerBase
    {
        public readonly LeaveService _service;

        private readonly ILogger<LeaveController> _logger;

        public LeaveController(LeaveService service, ILogger<LeaveController> logger)
        {
            _service = service;
            _logger = logger;
        }



        [HttpGet("GetDepartmentsForLeave")]
        public async Task<IActionResult> GetDepartmentsForLeave()
        {
            _logger.LogInformation("GetDepartmentsForLeave request received");

            try
            {
                var departments = await _service.GetDepartmentsForLeave();
                _logger.LogInformation("Successfully retrieved departments for leave");
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDepartmentsForLeave");
                throw;
            }
        }

        [HttpGet("GetLeaveRequest")]
        public async Task<IActionResult> GetLeaveRequest()
        {
            _logger.LogInformation("GetLeaveRequest request received");

            try
            {
                var leaverequest = await _service.GetLeaveRequest();
                _logger.LogInformation("Successfully retrieved leave requests");
                return Ok(leaverequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetLeaveRequest");
                throw;
            }
        }

        [HttpGet("GetStatusForLeave")]
        public async Task<IActionResult> GetStatusForLeave()
        {
            _logger.LogInformation("GetStatusForLeave request received");

            try
            {
                var status = await _service.GetStatusForLeave();
                _logger.LogInformation("Successfully retrieved leave status");
                return Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetStatusForLeave");
                throw;
            }
        }

        [HttpGet("GetLeaveStats")]
        public async Task<IActionResult> GetLeaveStats()
        {
            _logger.LogInformation("GetLeaveStats request received");

            try
            {
                var leavestats = await _service.GetLeaveStats();
                _logger.LogInformation("Successfully retrieved leave stats");
                return Ok(leavestats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetLeaveStats");
                throw;
            }
        }

        [HttpGet("GetLeaveTypes")]
        public async Task<IActionResult> GetLeaveTypes()
        {
            _logger.LogInformation("GetLeaveTypes request received");

            try
            {
                var leavetypes = await _service.GetLeaveTypes();
                _logger.LogInformation("Successfully retrieved leave types");
                return Ok(leavetypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetLeaveTypes");
                throw;
            }
        }

        [HttpPost("SubmitLeaveRequest")]
        public async Task<IActionResult> SubmitLeaveRequest([FromBody] SubmitLeaveRequestDto dto)
        {
            _logger.LogInformation("SubmitLeaveRequest request received for employee ID: {EmployeeId}", dto.employeeId);

            try
            {
                var result = await _service.SubmitLeaveRequest(dto);
                _logger.LogInformation("Successfully submitted leave request for employee ID: {EmployeeId}", dto.employeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SubmitLeaveRequest for employee ID: {EmployeeId}", dto.employeeId);
                throw;
            }
        }


        [HttpGet("GetEmployeesForLeave")]
        public async Task<IActionResult> GetEmployeesForLeave()
        {
            _logger.LogInformation("GetEmployeesForLeave request received");

            try
            {
                var result = await _service.GetEmployeesForLeave();
                _logger.LogInformation("Successfully retrieved employees for leave");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetEmployeesForLeave");
                throw;
            }
        }

        [HttpPost("ChangeLeaveRequestStatus")]
        public async Task<IActionResult> ChangeLeaveRequestStatus([FromBody] ChangeStatusDto dto)
        {
            _logger.LogInformation("ChangeLeaveRequestStatus request received for request ID");

            try
            {
                var changestatus = await _service.ChangeLeaveRequestStatus(dto);
                _logger.LogInformation("Successfully changed leave request status for request ID");
                return Ok(changestatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ChangeLeaveRequestStatus for request ID:");
                throw;
            }
        }
    }
}
