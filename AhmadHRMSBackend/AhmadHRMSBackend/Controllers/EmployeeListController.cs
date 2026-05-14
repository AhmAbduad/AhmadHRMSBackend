using AhmadHRMSBackend.dto.CreateEmployee;
using AhmadHRMSBackend.dto.EmployeeList;
using AhmadHRMSBackend.dto.UpdateEmployee;
using AhmadHRMSBackend.Services.EmployeeList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AhmadHRMSBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeListController : ControllerBase
    {

        public readonly EmployeeListService _service;
        private readonly ILogger<EmployeeListController> _logger;


        public EmployeeListController(EmployeeListService service, ILogger<EmployeeListController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            _logger.LogInformation("GetAllEmployees request received");

            try
            {
                var employees = await _service.GetAllEmployees();
                _logger.LogInformation("Successfully retrieved all employees");
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllEmployees");
                throw;
            }
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            _logger.LogInformation("GetAllDepartments request received");

            try
            {
                var departments = await _service.GetAllDepartments();
                _logger.LogInformation("Successfully retrieved all departments");
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllDepartments");
                throw;
            }
        }

        [HttpGet("GetAllPosition")]
        public async Task<IActionResult> GetAllPosition()
        {
            _logger.LogInformation("GetAllPosition request received");

            try
            {
                var position = await _service.GetAllPosition();
                _logger.LogInformation("Successfully retrieved all positions");
                return Ok(position);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllPosition");
                throw;
            }
        }

        [HttpGet("GetAllStatus")]
        public async Task<IActionResult> GetAllStatus()
        {
            _logger.LogInformation("GetAllStatus request received");

            try
            {
                var status = await _service.GetAllStatus();
                _logger.LogInformation("Successfully retrieved all status");
                return Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllStatus");
                throw;
            }
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            _logger.LogInformation("CreateEmployee request received for email: {Email}", dto.Email);

            try
            {
                var result = await _service.CreateEmployee(dto);
                if (result == null)
                {
                    _logger.LogWarning("CreateEmployee failed - result is null for email: {Email}", dto.Email);
                    return NotFound();
                }
                _logger.LogInformation("Successfully created employee with ID");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateEmployee for email: {Email}", dto.Email);
                throw;
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto dto)
        {
            _logger.LogInformation("UpdateEmployee request received for employee ID: {Id}", dto.Id);

            try
            {
                var result = await _service.UpdateEmployee(dto);
                if (result == null)
                {
                    _logger.LogWarning("UpdateEmployee failed - result is null for employee ID: {Id}", dto.Id);
                    return NotFound();
                }
                _logger.LogInformation("Successfully updated employee ID: {Id}", dto.Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateEmployee for employee ID: {Id}", dto.Id);
                throw;
            }
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogInformation("DeleteEmployee request received for employee ID: {Id}", id);

            try
            {
                var result = await _service.DeleteEmployee(id);
                _logger.LogInformation("Successfully deleted employee ID: {Id}", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteEmployee for employee ID: {Id}", id);
                throw;
            }
        }

    }
}
