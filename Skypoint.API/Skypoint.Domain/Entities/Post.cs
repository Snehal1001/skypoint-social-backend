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

    public int UpVotes { get; set; }
    public int DownVotes { get; set; }

    public int Score => UpVotes - DownVotes;
}
}