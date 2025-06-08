using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Domain.Entities
{
    public class UserFollow
    {
        public Guid Id { get; set; }

        public Guid FollowerId { get; set; }
        public User Follower { get; set; }

        public Guid FolloweeId { get; set; }
        public User Followee { get; set; }

        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
    }
}