using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course? CourseNav { get; set; }

        public int StudentId { get; set; }
        public Student? StudentNav { get; set; }
        public DateTime EnrolledAt { get; set; }


    }
}
