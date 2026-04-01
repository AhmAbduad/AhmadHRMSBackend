using AhmadHRMSBackend.dto.CreateEmployee;
using AhmadHRMSBackend.dto.EmployeeList;
using AhmadHRMSBackend.dto.UpdateEmployee;
using AhmadHRMSBackend.Services.EmployeeList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeListController : ControllerBase
    {

        public readonly EmployeeListService _service;

        public EmployeeListController(EmployeeListService service)
        {
            _service = service;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _service.GetAllEmployees();

            return Ok(employees);
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _service.GetAllDepartments();
            return Ok(departments);
        }

        [HttpGet("GetAllPosition")]
        public async Task<IActionResult> GetAllPosition()
        {
            var position = await _service.GetAllPosition();
            return Ok(position);
        }

        [HttpGet("GetAllStatus")]
        public async Task<IActionResult> GetAllStatus()
        {
            var status = await _service.GetAllStatus();
            return Ok(status);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            var result = await _service.CreateEmployee(dto);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto dto)
        {
            var result = await _service.UpdateEmployee(dto);
            if(result==null)
            {
                return NotFound();
            }
            return Ok(result);  
        }

    }
}
