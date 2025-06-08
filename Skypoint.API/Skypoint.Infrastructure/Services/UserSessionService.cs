using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skypoint.Application.IServices;
using Skypoint.Domain.Entities;

namespace Skypoint.Infrastructure.Services
{
    public class UserSessionService : IUserSessionService
    {

        private readonly ApplicationDbContext _context;

        public UserSessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> RecordLoginAsync(Guid userId)
        {
            var session = new UserSession
            {
                UserId = userId,
                LoginTime = DateTime.UtcNow
            };

            _context.UserSessions.Add(session);
            await _context.SaveChangesAsync();

            return session.Id;
        }

        public async Task<TimeSpan?> RecordLogoutAsync(Guid userId)
        {
            var session = await _context.UserSessions
                .Where(s => s.UserId == userId && s.LogoutTime == null)
                .OrderByDescending(s => s.LoginTime)
                .FirstOrDefaultAsync();

            if (session == null) return null;

            session.LogoutTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return session.LogoutTime - session.LoginTime;
        }
    }
}