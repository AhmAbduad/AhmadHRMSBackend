using AhmadHRMSBackend.dto.Login;
using AhmadHRMSBackend.Services.Dashboard;
using AhmadHRMSBackend.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadHRMSBackend.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly LoginService _service;

        public LoginController(LoginService service)
        {
            _service = service;
        }

        [HttpPost("CheckUser")]
        public async Task<IActionResult> CheckUser([FromBody] CheckUserDto dto)
        {
            var token = await _service.CheckUser(dto);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }
    }
}
