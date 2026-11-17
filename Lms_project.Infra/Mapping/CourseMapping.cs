using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;

namespace Lms_project.API_2_.Mapping
{
    public static class CourseMapping
    {
        public static CourseResponseDto ToDto(this Course course)
        {


            return new CourseResponseDto(course.Id, course.Title, course.Price, course.Level, course.InstructorNav?.FullName,course.ImageUrl);
        }
        public static Course ToEntity(this CreateCourseDto dto)
        {


            return new Course { 
            Title=dto.Title,
            Price=dto.Price,
            InstructorId=dto.InstructorId,
            ImageUrl=dto.ImgUrl
            };
        }
        public static Course ToEntity(this UpdateCourseDto dto)
        {


            return new Course
            {
                Title = dto.Title,
                Price = dto.Price,
                InstructorId = dto.InstructorId,
                ImageUrl=dto.ImgUrl
                
            };
        }
    }
}
