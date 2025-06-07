using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skypoint.Application.DTOs.Vote;

namespace Skypoint.Application.IServices
{
    public interface IVoteService
    {
            Task VoteAsync(Guid userId, VoteDTO request);

    }
}