using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.DTOs
{
    public record CreateCourseDto(string Title, decimal Price, int InstructorId);
    public record UpdateCourseDto(string Title, decimal Price, int InstructorId);
    public record CourseResponseDto(int Id,string Title, decimal Price,int Level, string ? InstructorName);

}
