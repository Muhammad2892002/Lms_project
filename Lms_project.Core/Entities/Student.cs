using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public string? Phone { get; set; }
        public List<Enrollment> Enrollments { get; set; } = [];


    }
}
