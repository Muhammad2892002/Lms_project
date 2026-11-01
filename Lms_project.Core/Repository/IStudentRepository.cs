using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Core.Repository
{
    public interface IStudentRepository
    {
        public Task<PageResult<StudentResponseDto>> GetAllStudentsAsync(int page, int pageSize);
        public Task<Student> GetStdByIdAsync(int Id);
        public Task<Student> CreateStdAsync(Student std);
        public Task<Student> UpdateStdAsync(int Id, Student std);

        public Task<bool> DeleteStdAsync(int Id);
        public Task<bool?> CheckIfEmailReapeted(int Id, Student std);

    }
}
