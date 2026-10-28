using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.DTOs
{
    public record CreateCourseDto(string Title, decimal Price, int InstructorId);
    public record UpdateCourse(string Title, decimal Price);
    public record CourseResponseDto(int Id,string Title, decimal Price,int Level, string ? InstructorName);
    //public class Course
    //{

    //    public string? Title { get; init; }
    //    public decimal Price { get; init; }


    //    public int InstructorId { get; init; }


    //}
}
