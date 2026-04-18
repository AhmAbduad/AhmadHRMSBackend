using AhmadHRMSBackend.dto.ChangeStatus;
using AhmadHRMSBackend.dto.SaveAttendance;
using AhmadHRMSBackend.dto.SubmitLeaveRequest;
using AhmadHRMSBackend.Services.Leave;
using AhmadHRMSBackend.Services.MarkAttendance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        public readonly LeaveService _service;

        public LeaveController(LeaveService service)
        {
            _service = service;
        }



        [HttpGet("GetDepartmentsForLeave")]
        public async Task<IActionResult> GetDepartmentsForLeave()
        {
            var departments = await _service.GetDepartmentsForLeave();
            return Ok(departments);
        }

        [HttpGet("GetLeaveRequest")]
        public async Task<IActionResult> GetLeaveRequest()
        {
            var leaverequest = await _service.GetLeaveRequest();
            return Ok(leaverequest);
        }

        [HttpGet("GetStatusForLeave")]
        public async Task<IActionResult> GetStatusForLeave()
        {
            var status = await _service.GetStatusForLeave();
            return Ok(status);
        }

        [HttpGet("GetLeaveStats")]
        public async Task<IActionResult> GetLeaveStats()
        {
            var leavestats = await _service.GetLeaveStats();
            return Ok(leavestats);
        }

        [HttpGet("GetLeaveTypes")]
        public async Task<IActionResult> GetLeaveTypes()
        {
            var leavetypes = await _service.GetLeaveTypes();
            return Ok(leavetypes);
        }

        [HttpPost("SubmitLeaveRequest")]
        public async Task<IActionResult> SubmitLeaveRequest([FromBody] SubmitLeaveRequestDto dto)
        {
            var result = await _service.SubmitLeaveRequest(dto);
            return Ok(result);
        }


        [HttpGet("GetEmployeesForLeave")]
        public async Task<IActionResult> GetEmployeesForLeave()
        {
            var result = await _service.GetEmployeesForLeave();
            return Ok(result);
        }

        [HttpPost("ChangeLeaveRequestStatus")]
        public async Task<IActionResult> ChangeLeaveRequestStatus([FromBody] ChangeStatusDto dto)
        {
            var changestatus = await _service.ChangeLeaveRequestStatus(dto);
            return Ok(changestatus);
        }
    }
}
