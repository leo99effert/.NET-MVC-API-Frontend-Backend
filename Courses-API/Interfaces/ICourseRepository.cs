using Courses_API.Models;
using Courses_API.ViewModels;

namespace Courses_API.Interfaces
{
  public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> ListAllCoursesAsync();
        public Task<CourseViewModel?> GetCourseAsync(int id);
        public Task<CourseViewModel?> GetCourseAsync(string title);
        public Task AddCourseAsync(Course model);
        public void DeleteCourse(int id);
        public void UpdateCourse(int id, Course model);
        public Task<bool> SaveAllAsync();
    }
}