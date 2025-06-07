using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skypoint.Application.DTOs.Post;
using Skypoint.Application.IServices;

namespace Skypoint.Infrastructure.Services
{
   public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;

    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PostResponseDTO> CreatePostAsync(Guid userId, CreatePostDTO request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) throw new Exception("User not found");

        var post = new Domain.Entities.Post
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            AuthorId = userId
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return new PostResponseDTO
        {
            Id = post.Id,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            AuthorEmail = user.Email,
            Score = post.Score
        };
    }

    public async Task<List<PostResponseDTO>> GetAllPostsAsync()
    {
        return await _context.Posts
            .Include(p => p.Author)
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new PostResponseDTO
            {
                Id = p.Id,
                Content = p.Content,
                AuthorEmail = p.Author.Email,
                CreatedAt = p.CreatedAt,
                Score = p.Score
            })
            .ToListAsync();
    }
}
}