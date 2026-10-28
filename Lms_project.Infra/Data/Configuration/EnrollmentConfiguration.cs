using Lms_project.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Data.Configuration
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(en => en.Id);
            builder.HasIndex(obj=>new { obj.StudentId,obj.CourseId}).IsUnique();
            builder.HasOne(enroll => enroll.CourseNav).WithMany(course => course.Enrollments).HasForeignKey(enrollObj=> enrollObj.CourseId);
            builder.HasOne(enroll => enroll.StudentNav).WithMany(std => std.Enrollments).HasForeignKey(enroll => enroll.StudentId);
            builder.Property(e => e.EnrolledAt).HasDefaultValueSql("now()");

        }
    }
}
