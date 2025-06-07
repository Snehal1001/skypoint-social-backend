using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skypoint.Application.DTOs;

namespace Skypoint.Application.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> SignupAsync(AuthRequestDTO dto);
        Task<AuthResponseDTO?> LoginAsync(AuthRequestDTO dto);
    }
}