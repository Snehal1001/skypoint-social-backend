using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skypoint.Application.IServices;

namespace Skypoint.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostFeedController : ControllerBase
    {
        private readonly IPostFeedService _feedService;

        public PostFeedController(IPostFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var feed = await _feedService.GetFeedAsync(Guid.Parse(userId));
            return Ok(feed);
        }
    }
}