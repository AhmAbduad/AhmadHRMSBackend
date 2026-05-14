using AhmadHRMSBackend.dto.Login;
using AhmadHRMSBackend.Services.Dashboard;
using AhmadHRMSBackend.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace AhmadHRMSBackend.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly LoginService _service;
        private readonly ILogger<LoginController> _logger;

        public LoginController(LoginService service, ILogger<LoginController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("CheckUser")]
        public async Task<IActionResult> CheckUser([FromBody] CheckUserDto dto)
        {
            _logger.LogInformation("CheckUser login attempt for email: {Email}", dto.email);

            try
            {
                var token = await _service.CheckUser(dto);

                if (token == null)
                {
                    _logger.LogWarning("Login failed - invalid credentials for email: {Email}", dto.email);
                    return Unauthorized("Invalid credentials");
                }

                _logger.LogInformation("Login successful for email: {Email}", dto.email);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckUser for email: {Email}", dto.email);
                throw;
            }
        }
    }
}
