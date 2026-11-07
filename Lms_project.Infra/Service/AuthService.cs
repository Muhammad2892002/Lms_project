using Lms_project.Core.DTOs;
using Lms_project.Core.Exceptions;
using Lms_project.Core.Repository;
using Lms_project.Core.Security;
using Lms_project.Core.Service;
using Lms_project.Infra.Mapping;
using Lms_project.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.Auth;

namespace Lms_project.Infra.Service
{
    public class AuthService(IUserRepository userrepo,IPasswordHash passwordHash,ITokenService tokenService,IStudentRepository _stdRepo) : IAuthService
    {
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user=await userrepo.GetByEmailAsync(dto.Email);
            if (user is null || !passwordHash.VerifyPassword(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password");
            var (token,expiresAt)= tokenService.CreateToken(user);
            return new LoginResponseDto(token, expiresAt);

        }

        public async Task<(string Token,DateTime )> RegisterAsStdAsync(RegisterStdDto dto)

        {
            var student = dto.ToEntity();
            var existingUser = await _stdRepo.CheckIfEmailReapeted(0,student);
            if (existingUser==true) {
                throw new ConflictException("Email is already registered");

            
            }
            var result= await _stdRepo.CreateStdAsync(student);
            if (result != null) {
                var (token,expiresAt) = tokenService.CreateToken(result.UserNavigation);
                return (token,expiresAt);
            
            }
            return (null, DateTime.MinValue);
        }
    }
}
