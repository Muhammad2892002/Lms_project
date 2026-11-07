using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Security
{
    public interface IPasswordHash
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
