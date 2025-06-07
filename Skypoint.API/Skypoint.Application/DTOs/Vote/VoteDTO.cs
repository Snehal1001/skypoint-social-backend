using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skypoint.Application.DTOs.Vote
{
    public class VoteDTO
    {
        public Guid PostId { get; set; }
        public int Value { get; set; } // 1 or -1
    }
}