using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Lms_project.Core.DTOs.StudentDto;

namespace Lms_project.Core.Repository
{
    public interface IEnrollmentRepository
    {
        public Task<List<ShowEnrollmentDto>> GetAllEnrollments();
        public Task<Enrollment> GetEnrollmentById(int Id);
        public Task<Enrollment> AddNewEnrollment(Enrollment enrollment);
        public Task<string> UpdateEnrollment(int Id, int courseId);
        public Task<string> DeleteEnrollment(int Id);
        public Task<bool> CheckIfStudentEnrolled(int studentId, int courseId);
        public Task<bool> CheckIfStudentEnrolledInCourse(int Id, int studentId, int courseId);
        public Task<List<GetAllEnrolledCourses>> GetAllEnrolledCoursesForStd(int stdId);
    }
}
