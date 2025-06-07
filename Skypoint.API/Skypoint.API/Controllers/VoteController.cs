using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skypoint.Application.DTOs.Vote;
using Skypoint.Application.IServices;

namespace Skypoint.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpPost]
        public async Task<IActionResult> Vote(VoteDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _voteService.VoteAsync(Guid.Parse(userId), request);
            return Ok(new { message = "Vote recorded successfully." });
        }
    }
}