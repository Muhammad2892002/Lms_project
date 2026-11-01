using Lms_project.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Service
{
    public interface IEnrollmentService
    {
        public Task<List<ShowEnrollmentDto>> GetAllEnrollments();
        public Task<ShowEnrollmentDto> GetEnrollmentById(int Id);
        public Task<ShowEnrollmentDto> AddNewEnrollment(CreateEnrollment enrollmentDto);
        public Task<string> UpdateEnrollment(int Id, int CourseId);
        public Task<string> DeleteEnrollment(int Id);
       
    }
}
