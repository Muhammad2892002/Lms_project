using Lms_project.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Data.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
           
            builder.HasIndex(std => std.Email).IsUnique();
            builder.Property(std => std.FullName).IsRequired();
            
           
        }
    }
}
