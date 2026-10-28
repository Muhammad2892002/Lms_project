using Lms_project.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Data.Configuration
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c=>c.Id);
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            builder.HasIndex(c => c.Title).IsUnique();
            builder.Property(c => c.Price).HasPrecision(10, 2);
            builder.HasOne(c => c.InstructorNav).WithMany(i => i.Courses).HasForeignKey(c => c.InstructorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
