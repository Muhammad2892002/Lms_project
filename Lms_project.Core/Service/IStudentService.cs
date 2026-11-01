using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Core.Service
{
    public interface IStudentService
    {
        public Task<PageResult<StudentResponseDto>> GetAllStudentsAsync(int page, int pageSize);
        public Task<StudentResponseDto> GetStdByIdAsync(int Id);
        public Task<StudentResponseDto> CreateStdAsync(CreateStudentDto std);
        public Task<StudentResponseDto> UpdateStdAsync(int Id, EditStudentDto std);
     
        public Task<bool> DeleteStdAsync(int Id);
        public Task<List<GetAllEnrolledCourses>> GetAllEnrolledCoursesForStd(int stdId);





    }
}
