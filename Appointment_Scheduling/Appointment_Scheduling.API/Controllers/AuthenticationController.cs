using Appointment_Scheduling.Application.Services.Interfaces;
using Appointment_Scheduling.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduling.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service) => _service = service;


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request)
        {
            var result = await _service.AuthenticationService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _service.AuthenticationService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _service.AuthenticationService.LogoutAsync();
            return Ok(response);
        }
    }
}
