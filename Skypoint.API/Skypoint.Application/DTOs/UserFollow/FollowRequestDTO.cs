using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Application.DTOs.UserFollow
{
    public class FollowRequestDTO
    {
        public Guid TargetUserId { get; set; }
    }
}