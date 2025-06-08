using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Skypoint.Application.DTOs;
using Skypoint.Application.IServices;
using Skypoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Skypoint.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IUserSessionService _userSessionService;

        public AuthService(ApplicationDbContext context, IConfiguration config, IUserSessionService userSessionService)
        {
            _context = context;
            _config = config;
            _userSessionService = userSessionService;
        }

        public async Task<AuthResponseDTO?> SignupAsync(AuthRequestDTO dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return null;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User { Email = dto.Email, PasswordHash = passwordHash, UserName = dto.Email.Split('@')[0] };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var sessionId = await _userSessionService.RecordLoginAsync(user.Id);

            return new AuthResponseDTO { Email = user.Email, Token = GenerateToken(user) };
        }

        public async Task<AuthResponseDTO?> LoginAsync(AuthRequestDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            var sessionId = await _userSessionService.RecordLoginAsync(user.Id);

            return new AuthResponseDTO { Email = user.Email, Token = GenerateToken(user) };
        }

        public async Task<TimeSpan?> LogoutAsync(Guid userId)
        {
            var duration = await _userSessionService.RecordLogoutAsync(userId);
            return duration;
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
