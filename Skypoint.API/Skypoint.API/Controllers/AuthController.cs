using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Skypoint.Application.DTOs;
using Skypoint.Application.IServices;

namespace Skypoint.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] AuthRequestDTO dto)
        {
            var result = await _authService.SignupAsync(dto);
            return result is null ? Conflict("Email already exists") : Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            return result is null ? Unauthorized("Invalid credentials") : Ok(result);
        }
    }
}
