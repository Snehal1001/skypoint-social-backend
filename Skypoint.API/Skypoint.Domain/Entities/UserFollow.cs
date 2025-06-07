using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Domain.Entities
{
    public class UserFollow
    {
        public Guid FollowerId { get; set; }
        public User? Follower { get; set; }

        public Guid FollowedId { get; set; }
        public User? Followed { get; set; }
    }
}