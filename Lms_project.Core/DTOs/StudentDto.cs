using Lms_project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.DTOs
{
    //public int Id { get; set; }
    //public string FullName { get; set; }

    //public string Email { get; set; }
    //public string? Phone { get; set; }
    //public List<Enrollment> Enrollments { get; set; } = [];
    public class StudentDto
    {
        public record CreateStudent(string FullName,string Email,string Phone);
        public record EditStudent( string Email, string Phone);

        public record StudentResponseDto(int Id,string FullName,string Email,string Phone);

    }
}
