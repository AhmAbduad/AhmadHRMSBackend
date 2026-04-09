using AhmadHRMSBackend.dto.TimeSheetDetails;
using AhmadHRMSBackend.Services.Leave;
using AhmadHRMSBackend.Services.TimeSheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {

        public readonly TimeSheetService _service;

        public TimeSheetController(TimeSheetService service)
        {
            _service = service;
        }


        [HttpPost("GetTimeSheetDetail")]
        public async Task<IActionResult> GetTimeSheetDetail([FromBody] GetTimesheetDto dto)
        {
            var result = await _service.GetTimeSheetDetail(dto);

            return Ok(result);
        }


        [HttpGet("GetEmployeesForTimeSheet")]
        public async Task<IActionResult> GetEmployeesForTimeSheet()
        {
            var result = await _service.GetEmployeesForTimeSheet();
            return Ok(result);
        }

        //[HttpPost("SaveTimeSheet")]
        //public async Task<IActionResult> SaveTimeSheet([FromBody] SaveTimeSheetDto dto)
        //{

        //}
    }
}
