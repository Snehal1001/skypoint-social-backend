using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skypoint.Application.DTOs.UserFollow;

namespace Skypoint.Application.IServices
{
    public interface IUserFollowService
    {
        Task FollowAsync(Guid followerId, FollowRequestDTO request);
        Task UnfollowAsync(Guid followerId, FollowRequestDTO request);
    }
}