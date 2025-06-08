using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skypoint.Domain.Entities;

namespace Skypoint.Infrastructure.Seed
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var user1 = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "alice@skypoint.com",
                    UserName = "alice",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123")
                };

                var user2 = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "bob@skypoint.com",
                    UserName = "bob",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123")
                };

                var user3 = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "charlie@skypoint.com",
                    UserName = "charlie",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123")
                };

                context.Users.AddRange(user1, user2, user3);

                var post1 = new Post
                {
                    Id = Guid.NewGuid(),
                    Content = "Excited to be on Skypoint!",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-10),
                    AuthorId = user1.Id
                };

                var post2 = new Post
                {
                    Id = Guid.NewGuid(),
                    Content = "Anyone up for a tech talk tomorrow?",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-20),
                    AuthorId = user2.Id
                };

                var post3 = new Post
                {
                    Id = Guid.NewGuid(),
                    Content = "Loving this platform already.",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-5),
                    AuthorId = user1.Id
                };

                var post4 = new Post
                {
                    Id = Guid.NewGuid(),
                    Content = "Working remotely today 👩‍💻",
                    CreatedAt = DateTime.UtcNow.AddMinutes(-30),
                    AuthorId = user3.Id
                };

                context.Posts.AddRange(post1, post2, post3, post4);

                context.Votes.AddRange(
                    new Vote { PostId = post1.Id, UserId = user2.Id, Value = 1 },
                    new Vote { PostId = post1.Id, UserId = user3.Id, Value = -1 },
                    new Vote { PostId = post2.Id, UserId = user1.Id, Value = 1 },
                    new Vote { PostId = post3.Id, UserId = user2.Id, Value = 1 }
                );

                context.UserFollows.AddRange(
                    new UserFollow { FollowerId = user1.Id, FolloweeId = user2.Id },
                    new UserFollow { FollowerId = user1.Id, FolloweeId = user3.Id },
                    new UserFollow { FollowerId = user2.Id, FolloweeId = user3.Id }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}