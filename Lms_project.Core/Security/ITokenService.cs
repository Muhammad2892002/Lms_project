using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Security
{
    public interface ITokenService
    {
        public (string Token, DateTime ExpiresAt) CreateToken(User user);
    }
}
