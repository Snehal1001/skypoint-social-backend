using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skypoint.Application.DTOs.UserFollow;
using Skypoint.Application.IServices;

namespace Skypoint.Infrastructure.Services
{
    public class UserFollowService : IUserFollowService
{
    private readonly ApplicationDbContext _context;

    public UserFollowService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task FollowAsync(Guid followerId, FollowRequestDTO request)
    {
        if (followerId == request.TargetUserId)
            throw new Exception("You cannot follow yourself.");

        var alreadyFollowing = await _context.UserFollows
            .AnyAsync(f => f.FollowerId == followerId && f.FolloweeId == request.TargetUserId);

        if (alreadyFollowing)
            throw new Exception("Already following this user.");

        _context.UserFollows.Add(new Domain.Entities.UserFollow
        {
            Id = Guid.NewGuid(),
            FollowerId = followerId,
            FolloweeId = request.TargetUserId
        });

        await _context.SaveChangesAsync();
    }

    public async Task UnfollowAsync(Guid followerId, FollowRequestDTO request)
    {
        var follow = await _context.UserFollows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == request.TargetUserId);

        if (follow == null)
            throw new Exception("Not following this user.");

        _context.UserFollows.Remove(follow);
        await _context.SaveChangesAsync();
    }
}
}