using Lms_project.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lms_project.Infra.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Enrollment> Enrollments=> Set<Enrollment>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
         
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSeeding((context, _) => {
                if (!context.Set<Instructor>().Any())
                {
                    var muhammad = new Instructor { FullName = "Muhammad Hadidi" };
                    var ahmad = new Instructor { FullName = "Ahmad Al-zoubi" };
                    context.Set<Course>().AddRange(
                        new Course() { Title = "Asp.Net Course", Price = 290, InstructorNav = muhammad },
                        new Course() { Title = "AI", Price = 490, InstructorNav = ahmad }

                        );
                }
                if (!context.Set<Student>().Any())
                {
                    context.Set<Student>().AddRange(
                        new Student() { FullName = "Muhmmad Al Hadidi", Email = "muhammadalhdidii@gmail.com", Phone = "0777777777" },
                        new Student() { FullName = "Muhammad Nase Alhadidi", Email = "mhmdalhdidii@gmail.com", Phone = "0797777777" },
                        new Student() { FullName = "Omae Naser", Email = "omaenaser151550@gmail.com", Phone = "0787777777" }

                        );
                }
                
                context.SaveChanges();


            });
            
        }
    }
}
