using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skypoint.Application.DTOs.PostFeed;
using Skypoint.Application.IServices;

namespace Skypoint.Infrastructure.Services
{
    public class PostFeedService : IPostFeedService
    {
        private readonly ApplicationDbContext _context;

        public PostFeedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PostFeedItemDto>> GetFeedAsync(Guid userId)
        {
            var followedUserIds = await _context.UserFollows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FolloweeId)
                .ToListAsync();

            var posts = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Votes)
                .OrderByDescending(p =>
                    followedUserIds.Contains(p.AuthorId) ? 1 : 0) // Priority 1 for followed authors
                .ThenByDescending(p => p.Votes.Sum(v => v.Value)) // Post score
                .ThenByDescending(p => p.CreatedAt) // Most recent
                .Select(p => new PostFeedItemDto
                {
                    PostId = p.Id,
                    Content = p.Content,
                    AuthorName = p.Author.Email,
                    Score = p.Votes.Sum(v => v.Value),
                    TimeAgo = GetTimeAgo(p.CreatedAt),
                    UserVote = p.Votes
                        .Where(v => v.UserId == userId)
                        .Select(v => (int?)v.Value)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return posts;
        }

        private string GetTimeAgo(DateTime createdAt)
        {
            var timespan = DateTime.UtcNow - createdAt;
            if (timespan.TotalMinutes < 1)
                return "just now";
            if (timespan.TotalMinutes < 60)
                return $"{(int)timespan.TotalMinutes}m ago";
            if (timespan.TotalHours < 24)
                return $"{(int)timespan.TotalHours}h ago";
            return $"{(int)timespan.TotalDays}d ago";
        }
    }
}