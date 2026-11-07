using Lms_project.Core.Entities;
using Lms_project.Infra.Security;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.Auth;

namespace Lms_project.Infra.Mapping
{
    public static class RegisterMapping
    {
        public static Student ToEntity(this RegisterStdDto dto)
        {
            var user = new User {Email=dto.Email,PasswordHash=new BcryptPaswordHasher().HashPassword(dto.Password),Role=Roles.Student };
            var student = new Student {FullName=dto.FullName,Phone=dto.Phone,UserId=user.Id,UserNavigation=user,Email=dto.Email };
          
            return student;
        }

    }
}
