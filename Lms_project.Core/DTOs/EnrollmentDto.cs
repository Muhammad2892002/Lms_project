using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.DTOs
{
    public record ShowEnrollmentDto(int Id,string courseName,string studentName,DateTime enrollmentDate);
    public record CreateEnrollment(int CourseId, int StudentId);
    public record GetAllEnrolledCourses(string CourseName,int Level,decimal Price,
        string StudentName,string stdEmail,
        DateTime enrollmentDate);



}
