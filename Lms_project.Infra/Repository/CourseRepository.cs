using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Repository;
using Lms_project.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Repository
{
    public class CourseRepository(AppDbContext _context): ICourseRepository
    {
        private static readonly List<Course> _courses = [];
        public async Task<Course> CreateCourseAsync(Course course)
        {
            try
            {
                await _context.AddAsync(course);
                _context.SaveChangesAsync();
                await _context.Entry(course).Reference(c => c.InstructorNav).LoadAsync();

                return course;
            }
            catch (DbUpdateConcurrencyException ex) {
                throw new ArgumentException("A course with this title already exists.");
            
            
            
            }

        }

        public async Task<Course> DeleteCourseAsync(int Id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(obj => obj.Id == Id);
            _courses.Remove(course);
            return course;
        }

        public async Task<Course> EditCourseAsync(int Id, Course course)
        {
            var courseFindingResult =await _context.Courses.FirstOrDefaultAsync(obj => obj.Id == Id);
            courseFindingResult.Title = course.Title;
            courseFindingResult.Price = course.Price;
            courseFindingResult.Level = course.Level;
            _context.SaveChangesAsync();
            return courseFindingResult;
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await  _context.Courses.Include(c=>c.InstructorNav).ToListAsync();
        }
        public async Task<PageResult<CourseResponseDto>> GetpageAsync(FilterByPriceDto filter, int page,int pageSize )
        {
            var count = await _context.Courses.CountAsync();
            var item= await _context.Courses.OrderBy(c=>c.Id).Where(c => c.Price >= filter.MinPrice && c.Price <= filter.MaxPrice).Skip((page-1)*pageSize).Take(pageSize).Select(obj => new CourseResponseDto(
            obj.Id,
            obj.Title,
            obj.Price,
            obj.Level,
            obj.InstructorNav.FullName
            )).ToListAsync();
            return new PageResult<CourseResponseDto>(item, page, pageSize, count);
        }

        public async Task<Course> GetCourseByIdAsync(int Id)
        {
            var course = await _context.Courses.Include(c=>c.InstructorNav).FirstOrDefaultAsync(obj => obj.Id == Id);
            return course;
        }

        public bool isCourseValid(Course course) {
            var isCourseTitleUnique = !(_context.Courses.Any(obj=>obj.Title==course.Title));
            return isCourseTitleUnique;



        }
        public async Task<int> Count() { 
            return await _context.Courses.CountAsync();

        }


    }
}
