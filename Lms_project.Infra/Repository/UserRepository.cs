using Lms_project.Core.Entities;
using Lms_project.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Repository
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email) =>  await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);





    }
}
