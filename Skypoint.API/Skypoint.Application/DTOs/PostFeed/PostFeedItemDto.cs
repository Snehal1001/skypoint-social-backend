using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Application.DTOs.PostFeed
{
    public class PostFeedItemDto
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Score { get; set; }
        public string TimeAgo { get; set; }
        public int? UserVote { get; set; }
        public bool IsFollowing { get; set; }
    }
}