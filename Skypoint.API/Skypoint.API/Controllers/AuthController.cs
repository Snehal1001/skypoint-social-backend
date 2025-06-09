using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Signup([FromBody] AuthRequestDTO dto)
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

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var duration = await _authService.LogoutAsync(Guid.Parse(userId));

            if (duration == null)
                return NotFound("No active session found.");

            return Ok(new
            {
                SessionDuration = $"{duration.Value.TotalMinutes:F1} minutes"
            });
        }

    }
}
