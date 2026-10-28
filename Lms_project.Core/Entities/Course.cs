using System.ComponentModel.DataAnnotations.Schema;

namespace Lms_project.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }

       
        public int InstructorId { get; set; }

        public int Level { get; set; } = 1;

        public Instructor ? InstructorNav { get; set; }
        public List<Enrollment> Enrollments { get; set; } = [];
    }
}
