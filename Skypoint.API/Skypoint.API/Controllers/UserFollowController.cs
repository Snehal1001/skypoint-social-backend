using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skypoint.Application.DTOs.UserFollow;
using Skypoint.Application.IServices;

namespace Skypoint.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserFollowController : ControllerBase
    {
        private readonly IUserFollowService _followService;

        public UserFollowController(IUserFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> Follow(FollowRequestDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _followService.FollowAsync(Guid.Parse(userId), request);
            return Ok(new { message = "Followed successfully" });
        }

        [HttpPost("unfollow")]
        public async Task<IActionResult> Unfollow(FollowRequestDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _followService.UnfollowAsync(Guid.Parse(userId), request);
            return Ok(new { message = "Unfollowed successfully" });
        }
    }
}