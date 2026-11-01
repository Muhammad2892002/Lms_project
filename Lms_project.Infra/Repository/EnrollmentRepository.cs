using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Lms_project.Core.Exceptions;
using Lms_project.Core.Repository;
using Lms_project.Infra.Data;
using Lms_project.Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Repository
{
    public class EnrollmentRepository(AppDbContext _context) : IEnrollmentRepository
    {
        public async Task<Enrollment> AddNewEnrollment(Enrollment enrollment)
        {
            await _context.AddAsync(enrollment);
            await _context.SaveChangesAsync();
            await _context.Entry(enrollment).Reference(obj => obj.CourseNav).LoadAsync();
            await _context.Entry(enrollment).Reference(obj => obj.StudentNav).LoadAsync();
            return enrollment;
        }

        public async Task<string> DeleteEnrollment(int Id)
        {
            var EnrollmentObj = await _context.Enrollments.FirstOrDefaultAsync(obj => obj.Id == Id);
            if (EnrollmentObj == null)
                return string.Empty;
            _context.Remove(EnrollmentObj);
            await _context.SaveChangesAsync();
            return $"The enrollment that have this Id :{Id} is Deleted";
        }

        public async  Task<List<ShowEnrollmentDto>> GetAllEnrollments()
        {
            var allEnrollments=await _context.Enrollments.Select(obj=> new ShowEnrollmentDto(obj.Id,obj.CourseNav.Title,obj.StudentNav.FullName,obj.EnrolledAt)).ToListAsync();
            return allEnrollments;
        }

        public async Task<Enrollment> GetEnrollmentById(int Id)
        {
            var enrollmentObj= await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == Id);
            if (enrollmentObj == null) {
                return enrollmentObj;
            }
            await _context.Entry(enrollmentObj).Reference(obj => obj.CourseNav).LoadAsync();
            await _context.Entry(enrollmentObj).Reference(obj => obj.StudentNav).LoadAsync();
            return enrollmentObj;
        }

        public async Task<string> UpdateEnrollment(int Id, int courseId)
        {
            var enrollmentObj =await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == Id);
            enrollmentObj.CourseId = courseId;
            enrollmentObj.EnrolledAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return "Enrollment updated successfully";
        }
        public async Task<bool> CheckIfStudentEnrolled(int studentId, int courseId)
        {
            var enrollment = await _context.Enrollments.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            return enrollment;
        }

        public async Task<bool> CheckIfStudentEnrolledInCourse(int Id,int studentId,int courseId) { 
            var allEnrollmentsForStd=await _context.Enrollments.Where(e => e.StudentId == studentId && e.CourseId==courseId).AnyAsync();
            return allEnrollmentsForStd;

        }

        public async Task<List<GetAllEnrolledCourses>> GetAllEnrolledCoursesForStd(int stdId) {
            var allEnrolledCourses = await _context.Enrollments.Where(obj => obj.StudentId == stdId)
                    .Select(obj => new GetAllEnrolledCourses(obj.CourseNav.Title,obj.CourseNav.Level,obj.CourseNav.Price, obj.StudentNav.FullName,obj.StudentNav.Email, obj.EnrolledAt)).ToListAsync();
            return allEnrolledCourses;

        }
    }
}
