using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Application.DTOs.Post
{
    public class PostResponseDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Score { get; set; }
    }
}