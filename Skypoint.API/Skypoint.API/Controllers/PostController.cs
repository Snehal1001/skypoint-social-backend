using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skypoint.Application.DTOs.Post;
using Skypoint.Application.IServices;

namespace Skypoint.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("post")]
        public async Task<IActionResult> Create(CreatePostDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var post = await _postService.CreatePostAsync(Guid.Parse(userId), request);
            return Ok(post);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }
    }
}