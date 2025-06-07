using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skypoint.Application.DTOs.Post;

namespace Skypoint.Application.IServices
{
    public interface IPostService
    {
        Task<PostResponseDTO> CreatePostAsync(Guid userId, CreatePostDTO request);
        Task<List<PostResponseDTO>> GetAllPostsAsync();
    }
}