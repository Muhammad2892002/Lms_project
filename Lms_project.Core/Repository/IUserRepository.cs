using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Repository
{
    public interface IUserRepository
    {
        public Task<User?> GetByEmailAsync(string email);
    }
}
