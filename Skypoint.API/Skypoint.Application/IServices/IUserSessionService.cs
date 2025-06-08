using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Application.IServices
{
    public interface IUserSessionService
    {
        Task<Guid> RecordLoginAsync(Guid userId);
        Task<TimeSpan?> RecordLogoutAsync(Guid userId);
    }
}