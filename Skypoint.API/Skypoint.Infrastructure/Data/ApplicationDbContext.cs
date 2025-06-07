using Microsoft.EntityFrameworkCore;
using Skypoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skypoint.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFollow>()
                .HasKey(x => new { x.FollowerId, x.FollowedId });

            modelBuilder.Entity<UserFollow>()
                .HasOne(x => x.Follower)
                .WithMany(x => x.Following)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollow>()
                .HasOne(x => x.Followed)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId);

            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.UserId, v.PostId })
                .IsUnique();

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Post)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PostId);
        }

    }
}
