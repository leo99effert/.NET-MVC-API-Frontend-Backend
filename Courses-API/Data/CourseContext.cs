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
    public DbSet<Skill> Skills => Set<Skill>();
    public CourseContext(DbContextOptions options) : base(options) { } // Step 3. Create constructor to handle 
                                                                       //         conection configuration for database

  }

}