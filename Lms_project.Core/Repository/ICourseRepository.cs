using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Repository
{
    public interface ICourseRepository
    {
        public Task<List<Course>> GetAllCoursesAsync();
        public Task<Course> GetCourseByIdAsync(int Id);

        public Task<Course> CreateCourseAsync(Course course);

        public Task<Course> EditCourseAsync(int Id, Course course);

        public Task<Course> DeleteCourseAsync(int Id);
        public bool isCourseValid(Course course);
        public Task<PageResult<CourseResponseDto>> GetpageAsync(FilterByPriceDto filter, int page, int pageSize);
        public Task<int> Count();
    }
}
