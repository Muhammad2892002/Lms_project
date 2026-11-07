using Lms_project.Core.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Security
{
    public class BcryptPaswordHasher : IPasswordHash
    {
        public string HashPassword(string password)=> BCrypt.Net.BCrypt.HashPassword(password);


        public bool VerifyPassword(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);



    }
}
