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

    }
}
