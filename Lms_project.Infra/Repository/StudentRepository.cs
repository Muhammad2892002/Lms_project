using Lms_project.Core.Entities;
using Lms_project.Core.Repository;
using Lms_project.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Infra.Repository
{
    public class StudentRepository(AppDbContext _dbContext) : IStudentRepository
    {
        public async Task<Student> CreateStdAsync(Student std)
        {
           
            var result=await _dbContext.Students.AddAsync(std);
           await _dbContext.SaveChangesAsync();
            return std;
        }

        public async Task<bool> DeleteStdAsync(int Id)
        {
            var std = await _dbContext.Students.FirstOrDefaultAsync(stdObj => stdObj.Id == Id);
           var result= _dbContext.Students.Remove(std);
            _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var allStds = await _dbContext.Students.ToListAsync();
            return allStds;
        }

        public Task<Student> GetStdByIdAsync(int Id)
        {
            var std = _dbContext.Students.FirstOrDefaultAsync(stdObj => stdObj.Id == Id);
            return std;
        }

        public async Task<Student> UpdateStdAsync(int Id, Student std)
        {
            var EditStd = await _dbContext.Students.FirstOrDefaultAsync(c => c.Id == Id);
          
            EditStd.Email = std.Email;
            EditStd.Phone=std.Phone;
            await _dbContext.SaveChangesAsync();
            return EditStd;
            
            
        }
    }
}
