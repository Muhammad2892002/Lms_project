using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Core.Service
{
    public interface IStudentService
    {
        public Task<List<StudentResponseDto>> GetAllStudentsAsync();
        public Task<StudentResponseDto> GetStdByIdAsync(int Id);
        public Task<StudentResponseDto> CreateStdAsync(CreateStudent std);
        public Task<StudentResponseDto> UpdateStdAsync(int Id, EditStudent std);
        public Task<bool> DeleteStdAsync(int Id);
    }
}
