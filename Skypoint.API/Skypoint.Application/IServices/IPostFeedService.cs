using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skypoint.Application.DTOs.PostFeed;

namespace Skypoint.Application.IServices
{
    public interface IPostFeedService
    {
        Task<List<PostFeedItemDto>> GetFeedAsync(Guid userId);
    }
}