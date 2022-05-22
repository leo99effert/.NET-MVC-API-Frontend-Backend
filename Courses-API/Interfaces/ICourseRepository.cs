using Courses_API.Models;
using Courses_API.ViewModels;
using Courses_API.ViewModels.Course;

namespace Courses_API.Interfaces
{
  public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> ListAllCoursesAsync();
        public Task<CourseViewModel?> GetCourseAsync(int id);
        public Task<CourseSmallViewModel?> GetSmallCourseAsync(int id);
        public Task<CourseViewModel?> GetCourseAsync(string title);
        // public Task<List<CourseViewModel>> GetCourseByCategoryAsync(string category);       
        public Task AddCourseAsync(PostCourseViewModel model);
        public Task DeleteCourseAsync(int id);
        public Task UpdateCourseAsync(int id, PostCourseViewModel model);
        public Task UpdateCourseAsync(int id, PatchCourseViewModel model);
        public Task<bool> SaveAllAsync();
    }
}