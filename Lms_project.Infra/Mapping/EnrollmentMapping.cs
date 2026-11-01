using Lms_project.Core.DTOs;
using Lms_project.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Lms_project.Infra.Mapping
{
    public static class EnrollmentMapping
    {
        public static Enrollment ToEntity(this CreateEnrollment dto) { 
           
            return new Enrollment
            {
                CourseId = dto.CourseId,
                StudentId = dto.StudentId,
                EnrolledAt = DateTime.UtcNow
            };



            



        }
        public static ShowEnrollmentDto ToDto(this Enrollment entity)
        {
            return new ShowEnrollmentDto(entity.Id, entity.CourseNav.Title, entity.StudentNav.FullName, entity.EnrolledAt);
        }
    }
}
