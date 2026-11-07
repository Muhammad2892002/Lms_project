using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
     
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string Role { get; set; }=Roles.Student;
    }
    public static class Roles { 
        public const string Admin = "Admin";
        public const string Instructor = "Instructor";
        public const string Student = "Student";

    }
}
