using Lms_project.API_2_.Mapping;
using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Repository;
using Lms_project.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Service
{
    public class CourseService(ICourseRepository courseRepo) : ICourseService
    {
      
       
   

        public async Task<CourseResponseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            CourseResponseDto courseObj = null;
            //var isCourseValid = courseRepo.isCourseValid(course);
            if (true)
            {
                var course = dto.ToEntity();
                 var entityCourse = await courseRepo.CreateCourseAsync(course);
                courseObj = entityCourse.ToDto();
            }
           
            return courseObj;

        }

        public async Task<Course> DeleteCourseAsync(int Id)
        {
            var course =await  courseRepo.DeleteCourseAsync(Id);
           
            return course;
        }

        public async Task<Course> EditCourseAsync(int Id, Course course)
        {
            var courseFindingResult =await  courseRepo.EditCourseAsync(Id,course);
            return courseFindingResult;
        }

        public async  Task<List<CourseResponseDto>> GetAllCoursesAsync()
        {
            var courses=await  courseRepo.GetAllCoursesAsync();
            return courses.Select(c => c.ToDto()).ToList();
        }

        public async Task<CourseResponseDto?> GetCourseByIdAsync(int Id)
        {
            var course = await courseRepo.GetCourseByIdAsync(Id);
            return course?.ToDto();
        }
    }
}
