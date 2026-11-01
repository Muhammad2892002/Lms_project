using Lms_project.Core.DTOs;
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

        public async Task<PageResult<StudentResponseDto>> GetAllStudentsAsync(int page, int pageSize)
        {
            var allItemsCount = await _dbContext.Students.CountAsync();
            var allStds = await _dbContext.Students.OrderBy(std => std.Id).Skip((page-1)*pageSize).Take(pageSize)
                .Select(std=>new StudentResponseDto(
                  std.Id,
                  std.FullName,
                  std.Email,
                  std.Phone
                )).ToListAsync();
            return new PageResult<StudentResponseDto>(allStds,page,pageSize, allItemsCount);
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
        public async Task<bool?> CheckIfEmailReapeted(int Id, Student std) {
            bool? checkEmail=null;
            if (Id == 0)
            {
                 checkEmail =await _dbContext.Students.AnyAsync(stdObj => stdObj.Email == std.Email || stdObj.Phone==std.Phone);
            }
            else if (Id>0) {
                checkEmail =await _dbContext.Students.AnyAsync(stdObj => (stdObj.Id != Id) && (stdObj.Email == std.Email || stdObj.Phone == std.Phone));


            }
            return checkEmail;
        
        
        
        }
    }
}
