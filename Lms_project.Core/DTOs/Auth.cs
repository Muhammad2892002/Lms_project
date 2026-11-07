using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Core.DTOs
{
    public class Auth
    {
        public record LoginRequestDto(string Email, string Password);
        public record LoginResponseDto(string Token, DateTime ExpiresAt);
        public record RegisterStdDto(string FullName,string Email,string Phone,string Password);
       
    }
}
