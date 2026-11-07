using System;
using System.Collections.Generic;
using System.Text;
using Lms_project.Core.DTOs;
using static Lms_project.Core.DTOs.Auth;

namespace Lms_project.Core.Service
{
    public interface IAuthService
    {
        public Task<LoginResponseDto>LoginAsync(LoginRequestDto dto);
        public Task<(string Token, DateTime)> RegisterAsStdAsync(RegisterStdDto dto);
    }
}
