using AhmadHRMSBackend.Services.Leave;
using AhmadHRMSBackend.Services.MarkAttendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
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
    }
}
