using Courses_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses_API.Data
{
  // The DbContext handles all comunication with the database
  public class CourseContext : DbContext // Step 1. Inherit from EntityFrameworkCore
  {
    public DbSet<Course> Courses => Set<Course>(); // Step 2. Map memory representation of course to the database
                                                   //         Introduce course to the database 
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<TeacherSkill> TeacherSkills => Set<TeacherSkill>();
    public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();
    public DbSet<Skill> Skills => Set<Skill>();
    public CourseContext(DbContextOptions options) : base(options) { } // Step 3. Create constructor to handle 
                                                                       //         conection configuration for database

    // Configure many to many relationship
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TeacherSkill

            modelBuilder.Entity<TeacherSkill>()
                .HasKey(ts => new {ts.TeacherId, ts.SkillId});

            modelBuilder.Entity<TeacherSkill>()
                .HasOne(ts => ts.Teacher)
                .WithMany(t => t.TeacherSkills)
                .HasForeignKey(ts => ts.TeacherId);

            modelBuilder.Entity<TeacherSkill>()
                .HasOne(ts => ts.Skill)
                .WithMany(s => s.TeacherSkills)
                .HasForeignKey(ts => ts.SkillId);

                // StudentCourse

                modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

                modelBuilder.Entity<StudentCourse>()
                    .HasOne(sc => sc.Student)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.StudentId);

                modelBuilder.Entity<StudentCourse>()
                    .HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentCourses)
                    .HasForeignKey(sc => sc.CourseId);
        }
  }

}