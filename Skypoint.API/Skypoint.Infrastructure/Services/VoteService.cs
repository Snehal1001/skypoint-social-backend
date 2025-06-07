using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skypoint.Application.DTOs.Vote;
using Skypoint.Application.IServices;

namespace Skypoint.Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly ApplicationDbContext _context;

        public VoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task VoteAsync(Guid userId, VoteDTO request)
        {
            if (request.Value != 1 && request.Value != -1)
                throw new Exception("Invalid vote value. Must be 1 or -1.");

            var post = await _context.Posts.FindAsync(request.PostId);
            if (post == null)
                throw new Exception("Post not found.");

            var existingVote = await _context.Votes
                .FirstOrDefaultAsync(v => v.UserId == userId && v.PostId == request.PostId);

            if (existingVote == null)
            {
                _context.Votes.Add(new Domain.Entities.Vote
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    PostId = request.PostId,
                    Value = request.Value
                });
            }
            else
            {
                existingVote.Value = request.Value; // allow changing vote
                _context.Votes.Update(existingVote);
            }

            await _context.SaveChangesAsync();
        }
    }
}