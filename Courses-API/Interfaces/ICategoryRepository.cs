using Courses_API.ViewModels.Category;

namespace Courses_API.Interfaces
{
  public interface ICategoryRepository
    {
        public Task<List<CategoryViewModel>> ListCategoryAsync();
        public Task<CategoryViewModel?> GetCategoryAsync(int id);
        public Task<List<CategoryWithCoursesViewModel>> ListCategoriesCoursesAsync();
        public Task<CategoryWithCoursesViewModel?> ListCategoriesCoursesAsync(int id);
        public Task AddCategoryAsync(PostCategoryViewModel model);
        public Task UpdateCategoryAsync(int id, PostCategoryViewModel model);
        public Task DeleteCategoryAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}