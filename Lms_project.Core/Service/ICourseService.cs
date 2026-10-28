using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Service
{
    public interface ICourseService
    {
        public Task<List<CourseResponseDto>> GetAllCoursesAsync();
        public Task<CourseResponseDto?> GetCourseByIdAsync(int Id);

        public Task<CourseResponseDto> CreateCourseAsync(CreateCourseDto dto);

        public Task<Course> EditCourseAsync(int Id, Course course);

        public Task<Course> DeleteCourseAsync(int Id);
    }
}
