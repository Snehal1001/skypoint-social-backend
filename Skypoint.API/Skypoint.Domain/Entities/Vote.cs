using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Domain.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }

        // User
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Post
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        //Vote value
        public int Value { get; set; }
    }
}