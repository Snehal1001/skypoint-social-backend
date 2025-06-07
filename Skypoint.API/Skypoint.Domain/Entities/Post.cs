using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<Vote> Votes { get; set; } = new List<Vote>();

        public int UpVotes => Votes.Count(v => v.Value == 1);
        public int DownVotes => Votes.Count(v => v.Value == -1);
        public int Score => UpVotes - DownVotes;
    }
}