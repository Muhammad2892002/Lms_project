using FluentValidation;
using Lms_project.API_2_.Mapping;
using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Exceptions;
using Lms_project.Core.Repository;
using Lms_project.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Service
{
    public class CourseService(ICourseRepository courseRepo,IValidator<CreateCourseDto>_validator,
        IValidator<UpdateCourseDto>_editValidator) : ICourseService
    {
      
       
   

        public async Task<CourseResponseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            
                await _validator.ValidateAndThrowAsync(dto);
                CourseResponseDto courseObj = null;
             
                    var course = dto.ToEntity();
                    var entityCourse = await courseRepo.CreateCourseAsync(course);
                    courseObj = entityCourse.ToDto();
                

                return courseObj;
            
           

        }

        public async Task<Course> DeleteCourseAsync(int Id)
        {
            var checkIfCourseIsExist = await courseRepo.GetCourseByIdAsync(Id);
            if (checkIfCourseIsExist == null) {
                throw new NotFoundException($"The course who has this Id: {Id} does not exist ");
            }
            var course =await  courseRepo.DeleteCourseAsync(Id);
           
            return course;
        }

        public async Task<Course> EditCourseAsync(int Id, UpdateCourseDto course)
        {
            await _editValidator.ValidateAndThrowAsync(course);
            var courseEntity = course.ToEntity();
            var courseFindingResult =await  courseRepo.EditCourseAsync(Id, courseEntity);
            return courseFindingResult;
        }

        public async  Task<List<CourseResponseDto>> GetAllCoursesAsync()
        {
            var courses=await  courseRepo.GetAllCoursesAsync();
            return courses.Select(c => c.ToDto()).ToList();
        }
        public async Task<PageResult<CourseResponseDto>> GetpageAsync(FilterByPriceDto filter, int page, int pageSize) { 
            return await courseRepo.GetpageAsync(filter, page, pageSize);

        }

        public async Task<CourseResponseDto?> GetCourseByIdAsync(int Id)
        {
            var course = await courseRepo.GetCourseByIdAsync(Id)?? throw new NotFoundException($"This Course :{Id} doesnot exist");
            return course?.ToDto();
        }
        public async Task<int> Count() {
            return await courseRepo.Count();

        }


    }
}
